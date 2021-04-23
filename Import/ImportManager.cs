using AddInSideViews;
using Paratext.Data;
using SIL.Scripture;
using System;
using System.Linq;
using TptMain.Util;

namespace TptMain.Import
{
    /// <summary>
    /// Manages importing (reading) verse text from a project.
    /// </summary>
    public class ImportManager
    {
        /// <summary>
        /// Paratext host interface.
        /// </summary>
        private readonly IHost _host;

        /// <summary>
        /// Active project name.
        /// </summary>
        private readonly string _projectName;

        /// <summary>
        /// Scripture extractor for the project.
        /// </summary>
        private readonly ScrText _projectScrText;

        /// <summary>
        /// Scripture parser for the project.
        /// </summary>
        private readonly ScrParser _projectScrParser;

        public ScrText ProjectScrText
        {
            get
            {
                return _projectScrText;
            }
        }

        /// <summary>
        /// Static initializer that ensures ParatextData is initialized.
        /// </summary>
        static ImportManager()
        {
            HostUtil.Instance.InitParatextData(true);
        }

        /// <summary>
        /// Basic ctor, taking minimum args and creating major support objects.
        /// 
        /// Note: Will initialize ParatextData, which may block.
        /// </summary>
        /// <param name="host">Paratext host interface (required).</param>
        /// <param name="projectName">Active project name (required).</param>
        public ImportManager(IHost host, string projectName)
        : this(host, projectName, ScrTextCollection.Get(projectName))
        { }

        /// <summary>
        /// Basic ctor, taking all args including support objects.
        ///
        /// Notes:
        /// - Expected to be used for testing.
        /// - Will initialize ParatextData, which may block.
        /// </summary>
        /// <param name="host">Paratext host interface (required).</param>
        /// <param name="projectName">Active project name (required).</param>
        /// <param name="projectScrText">ParatextData project proxy (required).</param>
        public ImportManager(IHost host, string projectName,
            ScrText projectScrText)
        {
            _host = host ?? throw new ArgumentNullException(nameof(host));
            _projectName = projectName ?? throw new ArgumentNullException(nameof(projectName));
            _projectScrText = projectScrText ?? throw new ArgumentNullException(nameof(projectScrText));

            _projectScrParser = _projectScrText.Parser;
        }

    }
}
