﻿namespace MediaToolkit
{
    using MediaToolkit.HLSOptions;
    using MediaToolkit.Model;
    using MediaToolkit.Options;
    using MediaToolkit.Resources;
    using MediaToolkit.Util;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;

    public class MediaEngine : EngineBase
    {
        public MediaEngine(string ffMpegPath) : base(ffMpegPath)
        {
        }

        /// <summary>   Event queue for all listeners interested in convertProgress events. </summary>
        public event EventHandler<ConvertProgressEventArgs> ConvertProgressEvent;

        /// <summary>
        ///     <para> Converts media with conversion options</para>
        /// </summary>
        /// <param name="inputFile">Input file</param>
        /// <param name="outputFile">Output file. </param>
        /// <param name="options">Conversion options. </param>
        public void Convert(MediaFile inputFile, MediaFile outputFile, ConversionOptions options)
        {
            var engineParams = new EngineParameters
            {
                InputFile = inputFile,
                OutputFile = outputFile,
                ConversionOptions = options,
                Task = FFmpegTask.Convert
            };

            this.FFmpegEngine(engineParams);
        }

        /// <summary>
        ///     Event queue for all listeners interested in conversionComplete events.
        /// </summary>
        public event EventHandler<ConversionCompleteEventArgs> ConversionCompleteEvent;

        /// <summary>
        ///     <para> Converts media with default options</para>
        /// </summary>
        /// <param name="inputFile">    Input file. </param>
        /// <param name="outputFile">   Output file. </param>
        public void Convert(MediaFile inputFile, MediaFile outputFile)
        {
            var engineParams = new EngineParameters
            {
                InputFile = inputFile,
                OutputFile = outputFile,
                Task = FFmpegTask.Convert
            };

            this.FFmpegEngine(engineParams);
        }

        public void CustomCommand(string ffmpegCommand)
        {
            if (ffmpegCommand.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(ffmpegCommand));
            }

            var engineParameters = new EngineParameters { CustomArguments = ffmpegCommand };

            this.StartFFmpegProcess(engineParameters);
        }

        public void GenerateGIF(EngineParameters paramters)
        {
            paramters.Task = FFmpegTask.GIF;
            this.FFmpegEngine(paramters);
        }

        public void HealthCheck(MediaFile input)
        {
            this.FFmpegEngine(new EngineParameters() { InputFile = input, Task = FFmpegTask.Check });
        }

        public void GenerateHLS(EngineParameters parameters)
        {
            parameters.Task = FFmpegTask.GenerateHLS;

            this.FFmpegEngine(parameters);
        }

        /// <summary>
        ///     <para> Retrieve media metadata</para>
        /// </summary>
        /// <param name="inputFile">Retrieves the metadata for the input file. </param>
        public void GetMetadata(MediaFile inputFile)
        {
            var engineParams = new EngineParameters
            {
                InputFile = inputFile,
                Task = FFmpegTask.GetMetaData
            };

            this.FFmpegEngine(engineParams);
        }

        /// <summary>   Retrieve a thumbnail image from a video file. </summary>
        /// <param name="inputFile">    Video file. </param>
        /// <param name="outputFile">   Image file. </param>
        /// <param name="options">      Conversion options. </param>
        public void GetThumbnail(ThumbnailOptions options)
        {
            var engineParams = new EngineParameters
            {
                InputFile = new MediaFile(options.InputFile),
                ThumbnailOptions = options,
                Task = FFmpegTask.GetThumbnail
            };

            this.FFmpegEngine(engineParams);
        }

        private void FFmpegEngine(EngineParameters engineParameters)
        {
            if (!engineParameters.InputFile.Filename.StartsWith("http://") && !File.Exists(engineParameters.InputFile.Filename))
            {
                throw new FileNotFoundException(Strings.Exception_Media_Input_File_Not_Found, engineParameters.InputFile.Filename);
            }

            try
            {
                this.Mutex.WaitOne();
                this.StartFFmpegProcess(engineParameters);
            }
            finally
            {
                this.Mutex.ReleaseMutex();
            }
        }

        private ProcessStartInfo GenerateStartInfo(EngineParameters engineParameters)
        {
            var arguments = CommandBuilder.Serialize(engineParameters);

            return this.GenerateStartInfo(arguments);
        }

        private ProcessStartInfo GenerateStartInfo(string arguments)
        {
            //windows case
            if (Path.DirectorySeparatorChar == '\\')
            {
                return new ProcessStartInfo
                {
                    Arguments = "-nostdin -y -loglevel info " + arguments,
                    FileName = this.FFmpegFilePath,
                    CreateNoWindow = true,
                    RedirectStandardInput = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }
            else //linux case: -nostdin options doesn't exist at least in debian ffmpeg
            {
                return new ProcessStartInfo
                {
                    Arguments = "-y -loglevel info " + arguments,
                    FileName = this.FFmpegFilePath,
                    CreateNoWindow = true,
                    RedirectStandardInput = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }
        }

        /// <summary>   Raises the conversion complete event. </summary>
        /// <param name="e">Event information to send to registered event handlers. </param>
        private void OnConversionComplete(ConversionCompleteEventArgs e)
        {
            this.ConversionCompleteEvent?.Invoke(this, e);
        }

        /// <summary>   Raises the convert progress event. </summary>
        /// <param name="e">    Event information to send to registered event handlers. </param>
        private void OnProgressChanged(ConvertProgressEventArgs e)
        {
            this.ConvertProgressEvent?.Invoke(this, e);
        }

        /// <summary>   Starts FFmpeg process. </summary>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the requested operation is
        ///     invalid.
        /// </exception>
        /// <exception cref="Exception">
        ///     Thrown when an exception error condition
        ///     occurs.
        /// </exception>
        /// <param name="engineParameters"> The engine parameters. </param>
        private void StartFFmpegProcess(EngineParameters engineParameters)
        {
            var receivedMessagesLog = new List<string>();
#if DEBUG
            if (engineParameters.Task == FFmpegTask.GenerateHLS)
            {
                var command = CommandBuilder.GetHLS(engineParameters);
                Console.WriteLine(command);
            }
#endif
            var processStartInfo = engineParameters.HasCustomArguments
                    ? this.GenerateStartInfo(engineParameters.CustomArguments)
                    : this.GenerateStartInfo(engineParameters);

            using (this.FFmpegProcess = Process.Start(processStartInfo))
            {
                Exception caughtException = null;
                if (this.FFmpegProcess == null)
                {
                    throw new InvalidOperationException(Strings.Exceptions_FFmpeg_Process_Not_Running);
                }

                //FFMPEG outputs to "sterr" to keep "stdout" for redirecting to other apps
                this.FFmpegProcess.ErrorDataReceived += (sender, received) =>
                    HandleOutput(engineParameters,
                    received,
                    receivedMessagesLog,
                    ref caughtException);

                this.FFmpegProcess.BeginErrorReadLine();
                this.FFmpegProcess.WaitForExit();

                if (( this.FFmpegProcess.ExitCode != 0 ) || caughtException != null)
                {
                    // If we are checking the file, do not throw
                    if (engineParameters.Task == FFmpegTask.Check)
                    {
                        HandleCheckResult(engineParameters.InputFile, receivedMessagesLog);
                    }
                    else
                    {
                        throw new Exception(
                        this.FFmpegProcess.ExitCode + ": " + receivedMessagesLog[1] + receivedMessagesLog[0],
                        caughtException);
                    }
                }
            }
        }

        private void HandleOutput(EngineParameters engineParameters, DataReceivedEventArgs received, List<string> receivedMessagesLog, ref Exception caughtException)
        {
            if (received.Data == null)
            {
                return;
            }

            var totalMediaDuration = new TimeSpan();

            try
            {
                receivedMessagesLog.Insert(0, received.Data);

                SetMetadata(engineParameters, received, totalMediaDuration);

                HandleProgressResult(received, totalMediaDuration);
                HandleConversionResult(engineParameters, received, totalMediaDuration);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // catch the exception and kill the process since we're in a faulted state
                caughtException = ex;

                try
                {
                    this.FFmpegProcess.Kill();
                }
                catch
                {
                    // swallow exceptions that are thrown when killing the process, 
                    // one possible candidate is the application ending naturally before we get a chance to kill it
                }
            }
        }

        private void HandleCheckResult(MediaFile input, List<string> receivedDataList)
        {
            if (input.Metadata == null)
            {
                input.Metadata = new Metadata();
            }

            foreach (var data in receivedDataList)
            {
                if (!string.IsNullOrEmpty(data))
                {
                    input.Metadata.ErrorCollection.Add(data);
                }
            }
        }

        private void HandleProgressResult(DataReceivedEventArgs received, TimeSpan totalMediaDuration)
        {
            if (RegexEngine.IsProgressData(received.Data, out ConvertProgressEventArgs progressEvent))
            {
                progressEvent.TotalDuration = totalMediaDuration;
                this.OnProgressChanged(progressEvent);
            }
        }

        private void HandleConversionResult(EngineParameters engineParameters, DataReceivedEventArgs received, TimeSpan totalMediaDuration)
        {
            if (engineParameters.Task != FFmpegTask.Convert)
            {
                return;
            }

            if (RegexEngine.IsConvertCompleteData(received.Data, out ConversionCompleteEventArgs convertCompleteEvent))
            {
                convertCompleteEvent.TotalDuration = totalMediaDuration;
                this.OnConversionComplete(convertCompleteEvent);
            }
        }

        private static void SetMetadata(EngineParameters engineParameters, DataReceivedEventArgs received, TimeSpan totalMediaDuration)
        {
            if (engineParameters.InputFile != null)
            {
                RegexEngine.TestVideo(received.Data, engineParameters);
                RegexEngine.TestAudio(received.Data, engineParameters);

                Match matchDuration = RegexEngine.Index[RegexEngine.Find.Duration].Match(received.Data);
                if (matchDuration.Success)
                {
                    if (engineParameters.InputFile.Metadata == null)
                    {
                        engineParameters.InputFile.Metadata = new Metadata();
                    }

                    TimeSpan.TryParse(matchDuration.Groups[1].Value, out totalMediaDuration);
                    engineParameters.InputFile.Metadata.Duration = totalMediaDuration;
                }
            }
        }
    }
}