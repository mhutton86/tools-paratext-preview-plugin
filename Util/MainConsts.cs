using TptMain.Models;

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
        public const string DEFAULT_SERVER_URI = "https://tpt-server.biblica.com/api";

        /// <summary>
        /// Default web request timeout in milliseconds.
        /// </summary>
        public const int DEFAULT_REQUEST_TIMEOUT_IN_MS = 30000;

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

        /// <summary>
        /// Default preview book format.
        /// </summary>
        public const BookFormat DEFAULT_BOOK_FORMAT = BookFormat.cav;

        /// <summary>
        /// Settings for preview font size in points (min/max/default).
        /// </summary>
        public static readonly PreviewSetting FontSizeSettings
            = new PreviewSetting(1f, 100f, 8f);

        /// <summary>
        /// Settings for preview font leading in points (min/max/default).
        /// </summary>
        public static readonly PreviewSetting FontLeadingSettings
            = new PreviewSetting(1f, 100f, 9f);

        /// <summary>
        /// Settings for preview page width in points. (min/max/default).
        /// </summary>
        public static readonly PreviewSetting PageWidthSettings
            = new PreviewSetting(1f, 1000f, 396f);

        /// <summary>
        /// Settings for preview page height in points (min/max/default).
        /// </summary>
        public static readonly PreviewSetting PageHeightSettings
            = new PreviewSetting(1f, 2000f, 612f);

        /// <summary>
        /// Settings for preview page header in points (min/max/default).
        /// </summary>
        public static readonly PreviewSetting PageHeaderSettings
            = new PreviewSetting(1f, 100f, 18f);

        /// <summary>
        /// Holder class for preview related settings (min, max, and default values).
        /// </summary>
        public class PreviewSetting
        {
            /// <summary>
            /// Min value.
            /// </summary>
            public float MinValue { get; private set; }

            /// <summary>
            /// Max value.
            /// </summary>
            public float MaxValue { get; private set; }

            /// <summary>
            /// Default value.
            /// </summary>
            public float DefaultValue { get; private set; }

            /// <summary>
            /// Basic ctor.
            /// </summary>
            /// <param name="minValue">Min value.</param>
            /// <param name="maxValue">Max value.</param>
            /// <param name="defaultValue">Default value.</param>
            public PreviewSetting(float minValue, float maxValue, float defaultValue)
            {
                MinValue = minValue;
                MaxValue = maxValue;
                DefaultValue = defaultValue;
            }
        }
    }
}