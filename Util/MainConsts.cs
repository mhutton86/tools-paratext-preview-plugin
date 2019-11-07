using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tools_paratext_preview_plugin.Util
{
    public static class MainConsts
    {
        public const string DEFAULT_SERVER_URI = "http://10.20.2.4:9875/api";
        public const int PROGRESS_FORM_UPDATE_RATE_IN_FPS = 10;
        public const int PREVIEW_JOB_UPDATE_INTERVAL_IN_SEC = 5;
        public const int TARGET_PREVIEW_JOB_TIME_IN_SEC = 90;
    }
}
