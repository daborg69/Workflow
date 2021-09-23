using System;
using System.Collections.Generic;
using Sheakley.ICS;

namespace ReferralLogic
{
    class Program
    {
        private static Dictionary<string, IWorkflow> _jurisdictions = new Dictionary<string, IWorkflow>();

        static void Main(string[] args)
        {

            DefaultJurisdiction df = new DefaultJurisdiction(_jurisdictions);
            ColoradoJurisdictionLogic colorado = new ColoradoJurisdictionLogic(_jurisdictions);
            TexasJurisdiction texas = new TexasJurisdiction(_jurisdictions);


            Console.WriteLine("Hello World!");

            // Get the Work Flow for Colorado
            IWorkflow workflow = GetWorkFlow("CO");
            workflow.RunStep(15, "Nice Mountains");

            // Do Texas
            workflow = GetWorkFlow("TX");
            workflow.RunStep(20, "LoneStar");

            // KY - which will be default
            workflow = GetWorkFlow("KY");
            workflow.RunStep(99, "Horses");

        }


        private static IWorkflow GetWorkFlow (string jurisdictionCode) {
            IWorkflow workflow;
            if (! _jurisdictions.TryGetValue(jurisdictionCode, out workflow))
                if ( !_jurisdictions.TryGetValue("DF", out workflow) )
                    throw new ArgumentException("Cannot find requested jurisdiction, nor the default jurisdiction");

            return workflow;
        }
    }
}
