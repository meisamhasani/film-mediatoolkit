namespace MediaToolkit
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>   Values that represent fmpeg tasks. </summary>
    public enum FFmpegTask
    {
        Convert,

        /// <summary>
        /// Get media metadata
        /// </summary>
        GetMetaData,

        /// <summary>
        /// Generate thumbnail
        /// </summary>
        GetThumbnail,

        /// <summary>
        /// 
        /// </summary>
        GenerateHLS
    }
}