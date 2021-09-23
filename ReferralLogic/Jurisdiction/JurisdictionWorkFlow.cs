using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sheakley.ICS;

namespace ReferralLogic
{
    public abstract class JurisdictionWorkFlow : Workflow,IWorkflow
    {

        /// <summary>
        /// The Jurisdiction code 
        /// </summary>
        public string JurisdictionCode { get; protected set; }


        public JurisdictionWorkFlow (string jurisdiction) : base(jurisdiction) { JurisdictionCode = jurisdiction; }



    }
}
