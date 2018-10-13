using System.IO;

namespace MediaToolkit.Util
{
    using System;

    public class Document
    {
        internal static bool IsLocked(string filePath)
        {
            if (filePath.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("filePath");
            }

            FileInfo file = new FileInfo(filePath);
            FileStream fileStream = null;

            try
            {
                fileStream = file.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }

            return false;
        }


    }
}