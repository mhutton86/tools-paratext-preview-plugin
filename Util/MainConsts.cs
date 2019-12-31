namespace TptMain.Util
{
    /// <summary>
    /// Utility constants.
    /// </summary>
    public static class MainConsts
    {
        /// <summary>
        /// JSON MIME type (this is .NET core, but not framework).
        /// </summary>
        public const string APPLICATION_JSON_MIME_TYPE = "application/json";

        /// <summary>
        /// Default server URI. Needs to be configurable, in future.
        /// </summary>
        public const string DEFAULT_SERVER_URI = "http://172.31.10.90:9875/api";

        /// <summary>
        /// Default web request timeout in milliseconds.
        /// </summary>
        public const int DEFAULT_REQUEST_TIMEOUT_IN_MS = 10000;

        /// <summary>
        /// Progress form update frequency in x/sec.
        /// </summary>
        public const int PROGRESS_FORM_UPDATE_RATE_IN_FPS = 10;

        /// <summary>
        /// Preview job update interval in sec.
        /// </summary>
        public const int PREVIEW_JOB_UPDATE_INTERVAL_IN_SEC = 5;

        /// <summary>
        /// Default output file name format.
        /// </summary>
        public const string DEFAULT_OUTPUT_FILE_NAME_FORMAT = "yyyyMMdd'T'HHmmss'Z'";

        /// <summary>
        /// Target preview job time.
        ///
        /// This is used to animate preview progress bar, as there's no server-side incremental progress at this time
        /// (generally accurate, as enqueued but not executing jobs are distinguishable and indicated differently).
        /// </summary>
        public const int TARGET_PREVIEW_JOB_TIME_IN_SEC = (60 * 10);
    }
}