/*
Copyright © 2021 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System.Collections.Generic;
using TptMain.Models;

namespace TptMain.Util
{
    /// <summary>
    /// Utility constants.
    /// </summary>
    public static class MainConsts
    {
        /// <summary>
        /// Default separator for a sequence of book names.
        /// </summary>
        public const string DEFAULT_REFERENCE_BOOK_SEQUENCE_SEPARATOR = ";";

        /// <summary>
        /// Default separator for a sequence of chapter numbers.
        /// </summary>
        public const string DEFAULT_REFERENCE_CHAPTER_SEQUENCE_SEPARATOR = ";";

        /// <summary>
        /// Default separator for a range of chapter numbers.
        /// </summary>
        public const string DEFAULT_REFERENCE_BOOK_OR_CHAPTER_RANGE_SEPARATOR = "–";

        /// <summary>
        /// Default separator for chapters and verses.
        /// </summary>
        public const string DEFAULT_REFERENCE_CHAPTER_AND_VERSE_SEPARATOR = ":";

        /// <summary>
        /// Default separator for verse sequences.
        /// </summary>
        public const string DEFAULT_REFERENCE_VERSE_SEQUENCE_SEPARATOR = ",";

        /// <summary>
        /// Default separator for verse ranges.
        /// </summary>
        public const string DEFAULT_REFERENCE_VERSE_RANGE_SEPARATOR = "-";

        /// <summary>
        /// Default prefix or suffix for references.
        /// </summary>
        public const string DEFAULT_REFERENCE_PREFIX_OR_SUFFIX = "";

        /// <summary>
        /// Default final punctuation for references.
        /// </summary>
        public const string DEFAULT_REFERENCE_FINAL_PUNCTUATION = "";

        /// <summary>
        /// Multiplier for book numbers in BCV-style references.
        /// </summary>
        public const int BookRefMultiplier = 1000000;

        /// <summary>
        /// Multiplier for chapter numbers in BCV-style references.
        /// </summary>
        public const int ChapRefMultiplier = 1000;

        /// <summary>
        /// Range ref parts (i.e., chapters, verses).
        /// </summary>
        public const int RefPartRange = 1000;

        /// <summary>
        /// JSON MIME type (this is .NET core, but not framework).
        /// </summary>
        public const string APPLICATION_JSON_MIME_TYPE = "application/json";

        /// <summary>
        /// Paratext project settings filename.
        /// </summary>
        public const string SETTINGS_FILE_PATH = "Settings.xml";

        /// <summary>
        /// Progress form update frequency in x/sec.
        /// </summary>
        public const int PROGRESS_FORM_UPDATE_RATE_IN_FPS = 10;

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
        public const int TARGET_PREVIEW_JOB_TIME_IN_SEC = (60 * 60);

        /// <summary>
        /// Default preview book format.
        /// </summary>
        public const BookFormat DEFAULT_BOOK_FORMAT = BookFormat.cav;

        // from: https://docs.google.com/spreadsheets/d/1cHdeBGfUV_HSWV5bkfrr4sbKClvM9B7fl_HFcvDlkZs/edit#gid=0

        /// <summary>
        /// Settings for preview font size in points (min/max/default).
        /// </summary>
        public static readonly PreviewSetting FontSizeSettings
            = new PreviewSetting(6f, 24f, 8f);

        /// <summary>
        /// Settings for preview font leading in points (min/max/default).
        /// </summary>
        public static readonly PreviewSetting FontLeadingSettings
            = new PreviewSetting(6f, 28f, 9f);

        /// <summary>
        /// Settings for preview page width in points. (min/max/default).
        /// </summary>
        public static readonly PreviewSetting PageWidthSettings
            = new PreviewSetting(180f, 612f, 396f);

        /// <summary>
        /// Settings for preview page height in points (min/max/default).
        /// </summary>
        public static readonly PreviewSetting PageHeightSettings
            = new PreviewSetting(216f, 1008f, 612f);

        /// <summary>
        /// Settings for preview page header in points (min/max/default).
        /// </summary>
        public static readonly PreviewSetting PageHeaderSettings
            = new PreviewSetting(0f, 50f, 18f);

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

        /// <summary>
        /// The copyright for this plugin.
        /// </summary>
        public const string COPYRIGHT = "© 2020-2021 Biblica, Inc.";

        // from: https://docs.google.com/spreadsheets/d/1wXMY_M8Dts8ATNt_autcU4MrtMl9LIAPOKvzA3w8eAI/edit?skip_itp2_check=true#gid=0

        // Tool tip strings
        public const string LAYOUT_TOOLTIP = "Changes the template based on desired format.";
        public const string LAYOUT_CAV_TOOLTIP = "Chapter and Verse";
        public const string LAYOUT_BTOTB_TOOLTIP = "The Books of the Bible (no chapter/verse markings)";

        public const string BOOK_RANGE = "Changes the Bible books to be previewed";
        public const string BOOK_RANGE_FULL = "Preview the full Bible";
        public const string BOOK_RANGE_NT = "Preview the New Testament";
        public const string BOOK_RANGE_CUSTOM = "Preview a custom book list, using the canonical book IDs (e.g., GEN, DEU or GEN, LEV, 2KI)";
        public const string BOOK_RANGE_ANCILLARY = "Include ancillary books(e.g, FRT, GLO, XXA, etc.)";

        public const string TEXT_OPTS = "Changes the previewed text. Use the default options or specify custom ones.";
        public const string TEXT_FONT = "Font Size: Min 6; Max 24";
        public const string TEXT_LEAD = "Leading: Min 6; Max 28";

        public const string PAGE_OPTS = "Changes the previewed page. Use the default options or specify custom ones.";
        public const string PAGE_WIDTH = "Width: Min 180; Max 612";
        public const string PAGE_HEIGHT = "Height: Min 216; Max 1008";
        public const string PAGE_HEADER = "Header Size: Min 0; Max 50";

        public const string HYPHENATE = "Changes whether the previewed text uses hyphenation; Break and hyphenate long words. Must have hyphenation data in project.";
        public const string LOCALIZE_FOOTNOTES = "Footnotes should be localized. Must specify footnote caller sequence.";

        public const string INCLUSIONS = "Changes whether to include specific elements in the preview.";
        public const string INCLUDE_INTRO = "Include the Introductory text";
        public const string INCLUDE_HEADINGS = "Include section headings";
        public const string INCLUDE_FOOTNOTES = "Include footnotes";
        public const string INCLUDE_CHAPTER_NUMS = "Include chapter numbers";
        public const string INCLUDE_VERSE_NUMS = "Include verse numbers";
        public const string INCLUDE_PARALLEL = "Include parallel passage references";
        public const string INCLUDE_ACROSTIC = "Include acrostic poetry Hebrew letters";

        public const string DOWNLOAD_TYPESETTING = "Download all typesetting files in addition to the preview.";
        public const string USE_PROJECT_FONTS = "Use the font specified in a project's Language Settings for the entire document.";

        /// <summary>
        /// The new testament book IDs
        /// </summary>
        public static readonly List<string> NEW_TESTAMENT_BOOKS = new List<string>()
        {
            "MAT",
            "MRK",
            "LUK",
            "JHN",
            "ACT",
            "ROM",
            "1CO",
            "2CO",
            "GAL",
            "EPH",
            "PHP",
            "COL",
            "1TH",
            "2TH",
            "1TI",
            "2TI",
            "TIT",
            "PHM",
            "HEB",
            "JAS",
            "1PE",
            "2PE",
            "1JN",
            "2JN",
            "3JN",
            "JUD",
            "REV"
        };

        /// <summary>
        /// The max size for displaying book names
        /// </summary>
        public const int MAX_BOOK_NAME_DISPLAY_LENGTH = 20;

        /// <summary>
        /// This is the URL to get support for the plugin.
        /// </summary>
        public const string SUPPORT_URL = "https://translationtools.biblica.com/en/support/home";
    }
}