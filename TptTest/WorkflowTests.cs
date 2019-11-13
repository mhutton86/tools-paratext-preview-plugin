using System;
using System.Windows.Forms;
using AddInSideViews;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TptMain.Models;
using TptMain.Workflow;

namespace TptTest
{
    [TestClass]
    public class WorkflowTests
    {
        private const string TEST_PROJECT_NAME = "testProjectName";
        private readonly ProjectDetails TEST_PROJECT_DETAILS = new ProjectDetails
        {
            ProjectName = TEST_PROJECT_NAME,
            ProjectUpdated = DateTime.MinValue
        };

        [TestMethod]
        public void TestMissingProject()
        {
            // setup
            var mockHost = new Mock<IHost>(MockBehavior.Strict);
            var mockWorkflow = new Mock<TypesettingPreviewWorkflow>(MockBehavior.Strict);

            mockWorkflow.Setup(workflowItem =>
            workflowItem.CheckProjectName(It.IsAny<String>()))
                .Returns((ProjectDetails)null);
            mockWorkflow.Setup(workflowItem =>
            workflowItem.Run(It.IsAny<IHost>(), It.IsAny<string>()))
                .CallBase();
            int messageBoxes = 0;
            mockWorkflow.Setup(workflowItem =>
            workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()))
                .Callback<string, MessageBoxButtons, MessageBoxIcon>((messsage, buttons, icon) => { messageBoxes++; })
                .Returns(DialogResult.OK);

            // execute
            mockWorkflow.Object.Run(mockHost.Object, TEST_PROJECT_NAME);

            // assert
            mockWorkflow.Verify(workflowItem =>
            workflowItem.CheckProjectName(TEST_PROJECT_NAME), Times.Once);
            mockWorkflow.Verify(workflowItem =>
            workflowItem.Run(mockHost.Object, TEST_PROJECT_NAME), Times.Once);
            mockWorkflow.Verify(workflowItem =>
            workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()), Times.Once);

            mockHost.VerifyNoOtherCalls();
            mockWorkflow.VerifyNoOtherCalls();
        }
    }
}
