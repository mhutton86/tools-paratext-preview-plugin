using System;
using System.IO;
using System.Windows.Forms;
using AddInSideViews;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TptMain.Form;
using TptMain.Models;
using TptMain.Workflow;

namespace TptTest
{
    /// <summary>
    /// Main workflow tests.
    /// </summary>
    [TestClass]
    public class WorkflowTests
    {
        /// <summary>
        /// Test project name.
        /// </summary>
        private const string TestProjectName = "testProjectName";

        /// <summary>
        /// Test user.
        /// </summary>
        private const string TestUser = "testUser";

        /// <summary>
        /// Test where project is missing from server.
        /// </summary>
        [ExpectedException(typeof(WorkflowException))]
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
                workflowItem.CheckProjectName(It.IsAny<string>()))
                .Throws<WorkflowException>();

            // execute
            mockWorkflow.Object.Run(mockHost.Object, TestProjectName);

            // assert
            mockWorkflow.Verify(workflowItem =>
                workflowItem.Run(mockHost.Object, TestProjectName), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CheckProjectName(TestProjectName), Times.Once);

            mockHost.VerifyNoOtherCalls();
            mockWorkflow.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Test user-cancelled setup.
        /// </summary>
        [TestMethod]
        public void TestCancelledSetup()
        {
            // setup
            var mockHost = new Mock<IHost>(MockBehavior.Strict);
            var mockWorkflow = new Mock<TypesettingPreviewWorkflow>(MockBehavior.Strict);
            var mockSetupForm = new Mock<SetupForm>() { CallBase = true };
            var testProjectDetails = CreateTestProjectDetails();

            mockHost.Setup(hostItem => hostItem.UserName)
                .Returns(TestUser);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.Run(It.IsAny<IHost>(), It.IsAny<string>()))
                .CallBase();
            mockWorkflow.Setup(workflowItem =>
                workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()))
                .Returns(DialogResult.OK);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CheckProjectName(TestProjectName))
                .Returns(testProjectDetails);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.IsFootnoteCallerSequenceDefined(TestProjectName))
                .Returns(true);
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
            mockWorkflow.Object.Run(mockHost.Object, TestProjectName);

            // assert
            mockHost.Verify(hostItem =>
                hostItem.UserName, Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.Run(mockHost.Object, TestProjectName), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CheckProjectName(TestProjectName), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()), Times.AtMostOnce);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CreateSetupForm(), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.ShowModalForm(mockSetupForm.Object), Times.Once);
            mockSetupForm.Verify(formItem =>
                formItem.SetProjectDetails(testProjectDetails), Times.Once);

            mockHost.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Test error creating preview job on server.
        /// </summary>
        [ExpectedException(typeof(WorkflowException))]
        [TestMethod]
        public void TestCreatePreviewJobError()
        {
            // setup
            var mockHost = new Mock<IHost>(MockBehavior.Strict);
            var mockWorkflow = new Mock<TypesettingPreviewWorkflow>(MockBehavior.Strict);
            var mockSetupForm = new Mock<SetupForm>() { CallBase = true };
            var mockProgressForm = new Mock<ProgressForm>() { CallBase = true };
            var testProjectDetails = CreateTestProjectDetails();
            var testPreviewJob = CreateTestPreviewJob();

            mockHost.Setup(hostItem => hostItem.UserName)
               .Returns(TestUser);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.Run(It.IsAny<IHost>(), It.IsAny<string>()))
                .CallBase();
            mockWorkflow.Setup(workflowItem =>
                workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()))
                .Returns(DialogResult.OK);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CheckProjectName(TestProjectName))
                .Returns(testProjectDetails);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CreateSetupForm())
                .Returns(mockSetupForm.Object);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.ShowModalForm(It.IsAny<Form>()));
            mockSetupForm.Setup(formItem =>
                formItem.SetProjectDetails(It.IsAny<ProjectDetails>()));
            mockSetupForm.Setup(
                formItem => formItem.IsCancelled)
                .Returns(false);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CreateProgressForm())
                .Returns(mockProgressForm.Object);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.ShowModelessForm(It.IsAny<Form>()));
            mockSetupForm.Setup(
                formItem => formItem.PreviewJob)
                .Returns(testPreviewJob);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CreatePreviewJob(testPreviewJob))
                .Throws(new IOException());
            mockWorkflow.Setup(workflowItem =>
                workflowItem.HideModelessForm(It.IsAny<Form>()));

            // execute
            mockWorkflow.Object.Run(mockHost.Object, TestProjectName);

            // assert, in workflow execution order
            mockWorkflow.Verify(workflowItem =>
                workflowItem.Run(mockHost.Object, TestProjectName), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CheckProjectName(TestProjectName), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()), Times.AtMostOnce);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CreateSetupForm(), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.ShowModalForm(mockSetupForm.Object), Times.Once);
            mockSetupForm.Verify(formItem =>
                formItem.SetProjectDetails(testProjectDetails), Times.Once);
            mockSetupForm.VerifyGet(formItem =>
                formItem.IsCancelled, Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CreateProgressForm(), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.ShowModelessForm(mockProgressForm.Object), Times.Once);
            mockSetupForm.VerifyGet(formItem =>
                formItem.PreviewJob, Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CreatePreviewJob(testPreviewJob), Times.Once);

            mockHost.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Test I/O error (client/server) finishing preview job on server.
        /// </summary>
        [ExpectedException(typeof(WorkflowException))]
        [TestMethod]
        public void TestFinishPreviewJobError1()
        {
            // setup
            var mockHost = new Mock<IHost>(MockBehavior.Strict);
            var mockWorkflow = new Mock<TypesettingPreviewWorkflow>(MockBehavior.Strict);
            var mockSetupForm = new Mock<SetupForm>() { CallBase = true };
            var mockProgressForm = new Mock<ProgressForm>() { CallBase = true };
            var testProjectDetails = CreateTestProjectDetails();
            var testPreviewJob1 = CreateTestPreviewJob();
            var testPreviewJob2 = CreateTestPreviewJob();
            testPreviewJob2.Id = Guid.NewGuid().ToString();

            mockHost.Setup(hostItem => hostItem.UserName)
               .Returns(TestUser);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.Run(It.IsAny<IHost>(), It.IsAny<string>()))
                .CallBase();
            mockWorkflow.Setup(workflowItem =>
                workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()))
                .Returns(DialogResult.OK);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CheckProjectName(TestProjectName))
                .Returns(testProjectDetails);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CreateSetupForm())
                .Returns(mockSetupForm.Object);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.ShowModalForm(It.IsAny<Form>()));
            mockSetupForm.Setup(formItem =>
                formItem.SetProjectDetails(It.IsAny<ProjectDetails>()));
            mockSetupForm.Setup(
                formItem => formItem.IsCancelled)
                .Returns(false);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CreateProgressForm())
                .Returns(mockProgressForm.Object);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.ShowModelessForm(It.IsAny<Form>()));
            mockSetupForm.Setup(
                formItem => formItem.PreviewJob)
                .Returns(testPreviewJob1);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CreatePreviewJob(testPreviewJob1))
                .Returns(() => testPreviewJob2);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.HideModelessForm(It.IsAny<Form>()));
            mockProgressForm.Setup(formItem =>
                formItem.SetStatus(testPreviewJob2))
                .Throws(new IOException());

            // execute
            mockWorkflow.Object.Run(mockHost.Object, TestProjectName);

            // assert, in workflow execution order
            mockWorkflow.Verify(workflowItem =>
                workflowItem.Run(mockHost.Object, TestProjectName), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CheckProjectName(TestProjectName), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CreateSetupForm(), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.ShowModalForm(mockSetupForm.Object), Times.Once);
            mockSetupForm.Verify(formItem =>
                formItem.SetProjectDetails(testProjectDetails), Times.Once);
            mockSetupForm.VerifyGet(formItem =>
                formItem.IsCancelled, Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CreateProgressForm(), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.ShowModelessForm(mockProgressForm.Object), Times.Once);
            mockSetupForm.VerifyGet(formItem =>
                formItem.PreviewJob, Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CreatePreviewJob(testPreviewJob1), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.HideModelessForm(mockProgressForm.Object), Times.Once);
            mockProgressForm.Verify(formItem =>
                formItem.SetStatus(testPreviewJob2), Times.Once);

            mockHost.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Test server-side error finishing preview job.
        /// </summary>
        [ExpectedException(typeof(WorkflowException))]
        [TestMethod]
        public void TestFinishPreviewJobError2()
        {
            // setup
            var mockHost = new Mock<IHost>(MockBehavior.Strict);
            var mockWorkflow = new Mock<TypesettingPreviewWorkflow>(MockBehavior.Strict);
            var mockSetupForm = new Mock<SetupForm>() { CallBase = true };
            var mockProgressForm = new Mock<ProgressForm>() { CallBase = true };
            var testProjectDetails = CreateTestProjectDetails();
            var testPreviewJob1 = CreateTestPreviewJob();
            var testPreviewJob2 = CreateTestPreviewJob();
            testPreviewJob2.Id = Guid.NewGuid().ToString();

            mockHost.Setup(hostItem => hostItem.UserName)
                .Returns(TestUser);
            var setStatusCtr = 0;
            mockWorkflow.Setup(workflowItem =>
                workflowItem.Run(It.IsAny<IHost>(), It.IsAny<string>()))
                .CallBase();
            mockWorkflow.Setup(workflowItem =>
                workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()))
                .Returns(DialogResult.OK);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CheckProjectName(TestProjectName))
                .Returns(testProjectDetails);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CreateSetupForm())
                .Returns(mockSetupForm.Object);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.ShowModalForm(It.IsAny<Form>()));
            mockSetupForm.Setup(formItem =>
                formItem.SetProjectDetails(It.IsAny<ProjectDetails>()));
            mockSetupForm.Setup(
                formItem => formItem.IsCancelled)
                .Returns(false);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CreateProgressForm())
                .Returns(mockProgressForm.Object);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.ShowModelessForm(It.IsAny<Form>()));
            mockSetupForm.Setup(
                formItem => formItem.PreviewJob)
                .Returns(testPreviewJob1);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CreatePreviewJob(testPreviewJob1))
                .Returns(() => testPreviewJob2);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.HideModelessForm(It.IsAny<Form>()));
            mockProgressForm.Setup(formItem =>
                formItem.SetStatus(testPreviewJob2))
                .Callback(() =>
                {
                    setStatusCtr++;
                    if (setStatusCtr > 1)
                    {
                        testPreviewJob2.IsError = true;
                    }
                    else if (setStatusCtr > 0)
                    {
                        testPreviewJob2.DateSubmitted = DateTime.UtcNow;
                        testPreviewJob2.DateStarted = DateTime.UtcNow;
                    }
                });

            // execute
            mockWorkflow.Object.Run(mockHost.Object, TestProjectName);

            // assert, in workflow execution order
            mockWorkflow.Verify(workflowItem =>
                workflowItem.Run(mockHost.Object, TestProjectName), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CheckProjectName(TestProjectName), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CreateSetupForm(), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.ShowModalForm(mockSetupForm.Object), Times.Once);
            mockSetupForm.Verify(formItem =>
                formItem.SetProjectDetails(testProjectDetails), Times.Once);
            mockSetupForm.VerifyGet(formItem =>
                formItem.IsCancelled, Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CreateProgressForm(), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.ShowModelessForm(mockProgressForm.Object), Times.Once);
            mockSetupForm.VerifyGet(formItem =>
                formItem.PreviewJob, Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CreatePreviewJob(testPreviewJob1), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.HideModelessForm(mockProgressForm.Object), Times.Once);
            mockProgressForm.Verify(formItem =>
                formItem.SetStatus(testPreviewJob2), Times.Exactly(2));

            mockHost.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Test error downloading preview file.
        /// </summary>
        [ExpectedException(typeof(WorkflowException))]
        [TestMethod]
        public void TestDownloadFileError()
        {
            // setup
            var mockHost = new Mock<IHost>(MockBehavior.Strict);
            var mockWorkflow = new Mock<TypesettingPreviewWorkflow>(MockBehavior.Strict);
            var mockSetupForm = new Mock<SetupForm>() { CallBase = true };
            var mockProgressForm = new Mock<ProgressForm>() { CallBase = true };
            var testProjectDetails = CreateTestProjectDetails();
            var testPreviewJob1 = CreateTestPreviewJob();
            var testPreviewJob2 = CreateTestPreviewJob();
            testPreviewJob2.Id = Guid.NewGuid().ToString();

            mockHost.Setup(hostItem => hostItem.UserName)
                .Returns(TestUser);
            var setStatusCtr = 0;
            mockWorkflow.Setup(workflowItem =>
                workflowItem.Run(It.IsAny<IHost>(), It.IsAny<string>()))
                .CallBase();
            mockWorkflow.Setup(workflowItem =>
                workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()))
                .Returns(DialogResult.OK);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CheckProjectName(TestProjectName))
                .Returns(testProjectDetails);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CreateSetupForm())
                .Returns(mockSetupForm.Object);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.ShowModalForm(It.IsAny<Form>()));
            mockSetupForm.Setup(formItem =>
                formItem.SetProjectDetails(It.IsAny<ProjectDetails>()));
            mockSetupForm.Setup(
                formItem => formItem.IsCancelled)
                .Returns(false);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CreateProgressForm())
                .Returns(mockProgressForm.Object);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.ShowModelessForm(It.IsAny<Form>()));
            mockSetupForm.Setup(
                formItem => formItem.PreviewJob)
                .Returns(testPreviewJob1);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CreatePreviewJob(testPreviewJob1))
                .Returns(() => testPreviewJob2);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.HideModelessForm(It.IsAny<Form>()));
            mockProgressForm.Setup(formItem =>
                formItem.SetStatus(testPreviewJob2))
                .Callback(() =>
                {
                    setStatusCtr++;
                    if (setStatusCtr > 1)
                    {
                        testPreviewJob2.DateCompleted = DateTime.UtcNow;
                    }
                    else if (setStatusCtr > 0)
                    {
                        testPreviewJob2.DateSubmitted = DateTime.UtcNow;
                        testPreviewJob2.DateStarted = DateTime.UtcNow;
                    }
                });
            mockWorkflow.Setup(workflowItem =>
                workflowItem.DownloadPreviewFile(testPreviewJob2, It.IsAny<bool>()))
                .Throws(new IOException());

            // execute
            mockWorkflow.Object.Run(mockHost.Object, TestProjectName);

            // assert, in workflow execution order
            mockWorkflow.Verify(workflowItem =>
                workflowItem.Run(mockHost.Object, TestProjectName), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CheckProjectName(TestProjectName), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CreateSetupForm(), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.ShowModalForm(mockSetupForm.Object), Times.Once);
            mockSetupForm.Verify(formItem =>
                formItem.SetProjectDetails(testProjectDetails), Times.Once);
            mockSetupForm.VerifyGet(formItem =>
                formItem.IsCancelled, Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CreateProgressForm(), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.ShowModelessForm(mockProgressForm.Object), Times.Once);
            mockSetupForm.VerifyGet(formItem =>
                formItem.PreviewJob, Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CreatePreviewJob(testPreviewJob1), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.HideModelessForm(mockProgressForm.Object), Times.Once);
            mockProgressForm.Verify(formItem =>
                formItem.SetStatus(testPreviewJob2), Times.Exactly(2));
            mockWorkflow.Verify(workflowItem =>
                workflowItem.DownloadPreviewFile(testPreviewJob2, It.IsAny<bool>()), Times.Once);

            mockHost.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Test complete, successful workflow.
        /// </summary>
        /// <param name="isArchive">True: test workflow when a typesetting archive is requested. False: test workflow when PDF is requested.</param>
        public void TestCompleteWorkflow(bool isArchive)
        {
            // setup
            var mockHost = new Mock<IHost>(MockBehavior.Strict);
            var mockWorkflow = new Mock<TypesettingPreviewWorkflow>(MockBehavior.Strict);
            var mockSetupForm = new Mock<SetupForm>() { CallBase = true };
            var mockProgressForm = new Mock<ProgressForm>() { CallBase = true };
            var testProjectDetails = CreateTestProjectDetails();
            var testPreviewJob1 = CreateTestPreviewJob();
            var testPreviewJob2 = CreateTestPreviewJob();
            testPreviewJob2.Id = Guid.NewGuid().ToString();
            var testPreviewFile = new FileInfo(Path.GetTempFileName());

            // ensure temp file (preview file) exists before we get started
            testPreviewFile.Refresh();
            Assert.IsTrue(testPreviewFile.Exists);

            mockHost.Setup(hostItem => hostItem.UserName)
                .Returns(TestUser);
            var setStatusCtr = 0;
            mockWorkflow.Setup(workflowItem =>
                workflowItem.Run(It.IsAny<IHost>(), It.IsAny<string>()))
                .CallBase();
            mockWorkflow.Setup(workflowItem =>
                workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()))
                .Returns(DialogResult.OK);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CheckProjectName(TestProjectName))
                .Returns(testProjectDetails);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.IsFootnoteCallerSequenceDefined(TestProjectName))
                .Returns(true);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CreateSetupForm())
                .Returns(mockSetupForm.Object);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.ShowModalForm(It.IsAny<Form>()));
            mockSetupForm.Setup(formItem =>
                formItem.SetProjectDetails(It.IsAny<ProjectDetails>()));
            mockSetupForm.Setup(
                formItem => formItem.IsCancelled)
                .Returns(false);

            // Setup form mock to address when archive is requested or otherwise.
            if (isArchive)
            {
                mockSetupForm.Setup(
                    formItem => formItem.IsArchive)
                    .Returns(true);
            }
            else
            {
                mockSetupForm.Setup(
                 formItem => formItem.IsArchive)
                 .Returns(false);
            }

            mockWorkflow.Setup(workflowItem =>
                workflowItem.CreateProgressForm())
                .Returns(mockProgressForm.Object);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.ShowModelessForm(It.IsAny<Form>()));
            mockSetupForm.Setup(
                formItem => formItem.PreviewJob)
                .Returns(testPreviewJob1);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.CreatePreviewJob(testPreviewJob1))
                .Returns(() => testPreviewJob2);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.HideModelessForm(It.IsAny<Form>()));
            mockProgressForm.Setup(formItem =>
                formItem.SetStatus(testPreviewJob2))
                .Callback(() =>
                {
                    setStatusCtr++;
                    if (setStatusCtr > 1)
                    {
                        testPreviewJob2.DateCompleted = DateTime.UtcNow;
                    }
                    else if (setStatusCtr > 0)
                    {
                        testPreviewJob2.DateSubmitted = DateTime.UtcNow;
                        testPreviewJob2.DateStarted = DateTime.UtcNow;
                    }
                });
            mockWorkflow.Setup(workflowItem =>
                workflowItem.DownloadPreviewFile(testPreviewJob2, isArchive))
                .Returns(testPreviewFile);

            // execute
            mockWorkflow.Object.Run(mockHost.Object, TestProjectName);

            // assert, in workflow execution order
            mockHost.Verify(hostItem =>
                hostItem.UserName, Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.Run(mockHost.Object, TestProjectName), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CheckProjectName(TestProjectName), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.ShowMessageBox(It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()), Times.AtMostOnce);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CreateSetupForm(), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.ShowModalForm(mockSetupForm.Object), Times.Once);
            mockSetupForm.Verify(formItem =>
                formItem.SetProjectDetails(testProjectDetails), Times.Once);
            mockSetupForm.VerifyGet(formItem =>
                formItem.IsCancelled, Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CreateProgressForm(), Times.Once);
            mockWorkflow.Setup(workflowItem =>
                workflowItem.IsFootnoteCallerSequenceDefined(TestProjectName))
                .Returns(true);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.ShowModelessForm(mockProgressForm.Object), Times.Once);
            mockSetupForm.VerifyGet(formItem =>
                formItem.PreviewJob, Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.CreatePreviewJob(testPreviewJob1), Times.Once);
            mockWorkflow.Verify(workflowItem =>
                workflowItem.HideModelessForm(mockProgressForm.Object), Times.Once);
            mockProgressForm.Verify(formItem =>
                formItem.SetStatus(testPreviewJob2), Times.Exactly(2));
            mockWorkflow.Verify(workflowItem =>
                workflowItem.DownloadPreviewFile(testPreviewJob2, isArchive), Times.Once);


            // ensure preview file is cleaned up after process complete
            testPreviewFile.Refresh();
            Assert.IsTrue(testPreviewFile.Exists);
            mockHost.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Util method for creating project details for testing.
        /// </summary>
        /// <returns>Project details object.</returns>
        private static ProjectDetails CreateTestProjectDetails()
        {
            return new ProjectDetails
            {
                ProjectName = TestProjectName,
                ProjectUpdated = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Util method for creating preview job for testing.
        /// </summary>
        /// <returns>Preview job object.</returns>
        private static PreviewJob CreateTestPreviewJob()
        {
            return new PreviewJob
            {
                ProjectName = TestProjectName,
                User = TestUser,
                BookFormat = BookFormat.cav,
                UseCustomFootnotes = true,
                FontSizeInPts = 123.4f,
                FontLeadingInPts = 234.5f,
                PageHeightInPts = 345.6f,
                PageWidthInPts = 456.7f,
                PageHeaderInPts = 567.8f
            };
        }

        /// <summary>
        /// Unit test for creating a preview with a typesetting archive download requested.
        /// </summary>
        [TestMethod]
        public void TestPreviewJobWhenIsArchiveEqualsTrue()
        {
            TestCompleteWorkflow(true);
        }

        /// <summary>
        // Unit test for creating a preview when PDF download is requested.
        /// </summary>
        [TestMethod]
        public void TestPreviewJobWhenIsArchiveEqualsFalse()
        {
            TestCompleteWorkflow(false);
        }
    }
}
