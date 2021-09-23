using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sheakley.ICS
{
    /// <summary>
    /// A single major step in the work flow process.
    /// </summary>
    public class WorkflowStep
    {
        /// <summary>
        /// The step number 
        /// </summary>
        public int StepNumber { get; protected set; }

        /// <summary>
        /// The major status that corresponds to this step.
        /// </summary>
        public int Status { get; protected set;  }

        /// <summary>
        /// SubStatus for this step.  This is always zero
        /// </summary>
        public int SubStatus { get; protected set; }

        /// <summary>
        /// Name of this step
        /// </summary>
        public string Name { get; protected set; }


        /// <summary>
        /// A method to run, that returns a bool.  Parameters are:
        /// <para>Object: the data if any needed by the method</para>
        /// <para>ReferalStep:  Current step we are on.</para>
        /// <para>WorkflowStep: Next step to set when this step is complete</para>
        /// </summary>
        protected Func<object, WorkflowStep,WorkflowStep,bool> _method;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Descriptive name for this step</param>
        /// <param name="stepNumber">The step number.</param>
        /// <param name="status">The major status this step corresponds to</param>
        /// <param name="subStatus">The substatus of this step</param>
        /// <param name="methodToRun">The method to be run to perform this step.</param>
        public WorkflowStep(string name,int stepNumber, int status, int subStatus, Func<object,WorkflowStep,WorkflowStep,bool> methodToRun) { 
            StepNumber = stepNumber;
            Status = status;
            SubStatus = subStatus;
            _method = methodToRun;
            Name = name;    
        }


        /// <summary>
        /// Runs the method associate with this step.
        /// </summary>
        /// <param name="data">Any Data to pass along to the method</param>
        /// <param name="nextStep">The next Major step to be set if this step is determined to be complete.  This will be null if there are no further steps after this one.</param>
        /// <returns></returns>
        public bool Execute (object data, WorkflowStep nextStep) { return _method(data,this,nextStep); }


        /// <summary>
        /// Writes Step info
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Step: " + Name + " - Step # [" + StepNumber + "] Status: [" + Status + "]  SubStatus: [" + SubStatus + "]";
        }
    }
}
