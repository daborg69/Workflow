using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sheakley.ICS
{
    /// <summary>
    /// Interface for a Sheakley WorkFlow Engine
    /// </summary>
    public interface IWorkflow {
        /// <summary>
        /// Adds a step into the workflow
        /// </summary>
        /// <param name="step"></param>
        public void AddStep (WorkflowStep step);
        
        /// <summary>
        /// Removes a step from the workflow
        /// </summary>
        /// <param name="stepNumber"></param>
        public void RemoveStep (int stepNumber);

        /// <summary>
        /// Overrides an existing step in the workflow
        /// </summary>
        /// <param name="step"></param>
        public void OverrideStep (WorkflowStep step);

        /// <summary>
        /// Runs the step associated with the step.
        /// </summary>
        /// <param name="status">The status </param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool RunStep (int status, object data);

    }
}
