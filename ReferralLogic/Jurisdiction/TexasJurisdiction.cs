using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sheakley.ICS;

namespace ReferralLogic
{
    public class TexasJurisdiction : DefaultJurisdiction, IWorkflow
    {
        public TexasJurisdiction (Dictionary<string, IWorkflow> dictionaryOfJurisdictions) : base("TX", dictionaryOfJurisdictions)
        {
            // We override Step B
            WorkflowStep workflowStep = new WorkflowStep("TexasStepB",2000, 20, 0, TexasStepB);
            OverrideStep(workflowStep);
        }



        // Override StepB from Default
        protected bool TexasStepB(object data, WorkflowStep current, WorkflowStep nextStep)
        {
            Console.WriteLine("Texas Step # [ " + current.StepNumber + "] - " + data.ToString());
            return true;
        }

    }
}
