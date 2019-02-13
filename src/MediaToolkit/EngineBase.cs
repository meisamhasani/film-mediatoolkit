using MediaToolkit.Resources;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace MediaToolkit
{
    public class EngineBase : IDisposable
    {
        private bool isDisposed;

        /// <summary>   Used for locking the FFmpeg process to one thread. </summary>
        private const string LockName = "MediaToolkit.Engine.LockName";

        protected EngineBase()
           : this(null)
        {
        }

        /// <summary>
        ///     <para> Initializes FFmpeg.exe; Ensuring that there is a copy</para>
        ///     <para> in the clients temp folder &amp; isn't in use by another process.</para>
        /// </summary>
        protected EngineBase(string ffMpegPath)
        {
            this.Mutex = new Mutex(false, LockName);
            this.isDisposed = false;

            this.FFmpegFilePath = ffMpegPath;

            //this.EnsureDirectoryExists();
            this.EnsureFFmpegFileExists();
            this.EnsureFFmpegIsNotUsed();
        }

        protected Mutex Mutex { get; }
        protected string FFmpegFilePath { get; }
        protected Process FFmpegProcess { get; set; }

        private void EnsureFFmpegIsNotUsed()
        {
            try
            {
                this.Mutex.WaitOne();
                foreach (var process in Process.GetProcessesByName(Strings.FFmpegProcessName))
                {
                    process.Kill();
                    process.WaitForExit();
                }
            }
            catch
            {

            }
            finally
            {
                this.Mutex.ReleaseMutex();
            }
        }

        private void EnsureFFmpegFileExists()
        {
            if (!File.Exists(this.FFmpegFilePath))
            {
                throw new InvalidOperationException($"FFMPEG was not found at {this.FFmpegProcess}");
            }
        }

        public virtual void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing || this.isDisposed)
            {
                return;
            }

            if (FFmpegProcess != null)
            {
                try
                {
                    this.FFmpegProcess.Kill();
                    this.FFmpegProcess.Dispose();
                }
                catch
                {
                }
            }

            this.FFmpegProcess = null;
            this.isDisposed = true;
        }
    }
}
