using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sheakley.ICS;

namespace ReferralLogic
{
    /// <summary>
    /// Represents the MDOS Default Workflow logic.  All state specific workflows inherit from this workflow and either override, remove or add steps as necessary.
    /// </summary>
    public class DefaultJurisdiction : JurisdictionWorkFlow, IWorkflow
    {
        /// <summary>
        /// Creates the Jurisdiction work flow
        /// </summary>
        /// <param name="dictionaryOfJurisdictions"></param>
        public DefaultJurisdiction(Dictionary<string, IWorkflow> dictionaryOfJurisdictions) : this ("DF",dictionaryOfJurisdictions) { }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jurisdictionCode"></param>
        /// <param name="dictionaryOfJurisdictions"></param>
        public DefaultJurisdiction (string jurisdictionCode, Dictionary<string,IWorkflow> dictionaryOfJurisdictions) : base (jurisdictionCode)
        {
            // Add some steps
            WorkflowStep workflowStep = new WorkflowStep("StepA",1000, 10, 0, StepA);
            AddStep(workflowStep);

            workflowStep = new WorkflowStep("StepB",2000, 20, 0, StepB);
            AddStep(workflowStep);

            workflowStep = new WorkflowStep("StepC", 6000, 99, 0, StepC);
            AddStep(workflowStep);


            dictionaryOfJurisdictions.Add(jurisdictionCode, this);
        }


        protected bool StepA(object data, WorkflowStep current, WorkflowStep nextStep)
        {
            Console.WriteLine("Default Step # [ " + current.StepNumber + "] - " + data.ToString());
            return true;
        }


        protected bool StepB(object data, WorkflowStep current, WorkflowStep nextStep)
        {
            Console.WriteLine("Default Step # [ " + current.StepNumber + "] - " + data.ToString());
            return true;
        }


        protected bool StepC(object data, WorkflowStep current, WorkflowStep nextStep)
        {
            Console.WriteLine("Default Step # [ " + current.StepNumber + "] - " + data.ToString());
            return true;
        }

    }
}
