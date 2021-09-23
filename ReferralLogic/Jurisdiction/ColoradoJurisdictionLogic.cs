using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sheakley.ICS;
namespace ReferralLogic
{
    /// <summary>
    /// Colorado specific workflow engine with modifications
    /// </summary>
    public class ColoradoJurisdictionLogic : DefaultJurisdiction, IWorkflow
    {
        public ColoradoJurisdictionLogic(Dictionary<string, IWorkflow> dictionaryOfJurisdictions) : base("CO", dictionaryOfJurisdictions) {

            // We add a step 1500
            WorkflowStep workflowStep = new WorkflowStep("ColoradoStep1500",1500, 15, 0, ColoradoStep1500);
            AddStep(workflowStep);

            // We add a ending step
            workflowStep = new WorkflowStep("ColoradoStep9999",9999, 254, 0, ColoradoStep9999);
            AddStep(workflowStep);
        }


        protected bool ColoradoStep1500(object data, WorkflowStep current, WorkflowStep nextStep)
        {
            Console.WriteLine("Colorado Step # [ " + current.StepNumber + "] - " + data.ToString());
            return false;
        }


        protected bool ColoradoStep9999(object data, WorkflowStep current, WorkflowStep nextStep)
        {
            Console.WriteLine("Colorado Step # [ " + current.StepNumber + "] - " + data.ToString());
            return false;
        }

    }
}
