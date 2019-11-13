using System;
using System.Windows.Forms;
using AddInSideViews;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TptMain.Form;
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
            workflowItem.Run(It.IsAny<IHost>(), It.IsAny<string>()))
                .CallBase();
            mockWorkflow.Setup(workflowItem =>
            workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()))
                .Returns(DialogResult.OK);
            mockWorkflow.Setup(workflowItem =>
            workflowItem.CheckProjectName(It.IsAny<String>()))
                .Returns((ProjectDetails)null);

            // execute
            mockWorkflow.Object.Run(mockHost.Object, TEST_PROJECT_NAME);

            // assert
            mockWorkflow.Verify(workflowItem =>
            workflowItem.Run(mockHost.Object, TEST_PROJECT_NAME), Times.Once);
            mockWorkflow.Verify(workflowItem =>
            workflowItem.CheckProjectName(TEST_PROJECT_NAME), Times.Once);
            mockWorkflow.Verify(workflowItem =>
            workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()), Times.Once);

            mockHost.VerifyNoOtherCalls();
            mockWorkflow.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void TestCancelledSetup()
        {
            // setup
            var mockHost = new Mock<IHost>(MockBehavior.Strict);
            var mockWorkflow = new Mock<TypesettingPreviewWorkflow>(MockBehavior.Strict);
            var mockSetupForm = new Mock<SetupForm>() { CallBase = true };

            mockWorkflow.Setup(workflowItem =>
            workflowItem.Run(It.IsAny<IHost>(), It.IsAny<string>()))
                .CallBase();
            mockWorkflow.Setup(workflowItem =>
            workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()))
                .Returns(DialogResult.OK);
            mockWorkflow.Setup(workflowItem =>
            workflowItem.CheckProjectName(TEST_PROJECT_NAME))
                .Returns(TEST_PROJECT_DETAILS);
            mockWorkflow.Setup(workflowItem =>
            workflowItem.CreateSetupForm())
                .Returns(mockSetupForm.Object);
            mockWorkflow.Setup(workflowItem =>
            workflowItem.ShowModalForm(It.IsAny<Form>()));
            mockSetupForm.Setup(formItem =>
            formItem.SetProjectDetails(It.IsAny<ProjectDetails>()));
            mockSetupForm.Setup(
            formItem => formItem.IsCancelled)
                .Returns(true);

            // execute
            mockWorkflow.Object.Run(mockHost.Object, TEST_PROJECT_NAME);

            // assert
            mockWorkflow.Verify(workflowItem =>
            workflowItem.Run(mockHost.Object, TEST_PROJECT_NAME), Times.Once);
            mockWorkflow.Verify(workflowItem =>
            workflowItem.CheckProjectName(TEST_PROJECT_NAME), Times.Once);
            mockWorkflow.Verify(workflowItem =>
            workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()), Times.Once);
            mockWorkflow.Verify(workflowItem =>
            workflowItem.CreateSetupForm(), Times.Once);
            mockWorkflow.Verify(workflowItem =>
            workflowItem.ShowModalForm(mockSetupForm.Object), Times.Once);
            mockSetupForm.Verify(formItem =>
            formItem.SetProjectDetails(TEST_PROJECT_DETAILS), Times.Once);

            mockHost.VerifyNoOtherCalls();
        }
    }
}
