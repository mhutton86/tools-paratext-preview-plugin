using System;

namespace TptMain.Workflow
{
    /// <summary>
    /// Routine usage exceptions (e.g., invalid user inputs, render failures).
    /// </summary>
    public class WorkflowException : ApplicationException
    {
        /// <inheritdoc />
        public WorkflowException()
        {
        }

        /// <inheritdoc />
        public WorkflowException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        public WorkflowException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
