using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Sheakley.ICS;

namespace TestWorkflow
{
    public class Tests {
        private const int RUNRESULT = 1999;
        private int _runResult = 0;
        private string _runNextStep = "";
        private string _workflowName = "testWF";

        [SetUp]
        public void Setup()
        {
        }


        private bool Step1 (Object data, WorkflowStep current, WorkflowStep next) {
            _runResult = RUNRESULT;
            _runNextStep = next.Name;
            return true;
        }


        [Test]
        public void WorkflowAddSingleStep()
        {
            Workflow workflow = new Workflow(_workflowName);

            int stepNumber = 1;
            int status = 10;
            int subStatus = 0;

            WorkflowStep step = new WorkflowStep("Step 1", stepNumber, status, subStatus, Step1);
            workflow.AddStep(step);


            Assert.AreEqual(1,workflow.StepCount,"A10:");
            Assert.AreEqual(10,workflow.StepsInOrder[1].Status,"A20:");
        }


        [Test]
        public void WorkflowAddMultipleSteps()
        {
            Workflow workflow = new Workflow(_workflowName);

            int stepNumber = 1;
            int status = 10;
            int subStatus = 0;

            WorkflowStep step = new WorkflowStep("Step 1", stepNumber++, status++, subStatus, Step1);
            workflow.AddStep(step);
            step = new WorkflowStep("Step 2", stepNumber++, status++, subStatus, Step1);
            workflow.AddStep(step);
            step = new WorkflowStep("Step 3", stepNumber++, status++, subStatus, Step1);
            workflow.AddStep(step);
            step = new WorkflowStep("Step 4", stepNumber, status, subStatus, Step1);
            workflow.AddStep(step);

            Assert.AreEqual(stepNumber, workflow.StepCount, "A10:");

            // Ensure they are in order:
            int start = 0;
            status = 0;
            foreach ( KeyValuePair<int, WorkflowStep> workflowStep in workflow.StepsInOrder ) {
                status = 10 + start;
                start++;
                Assert.AreEqual(start,workflowStep.Value.StepNumber,"A100: loopCount: " + start);
                Assert.AreEqual("Step " + start, workflowStep.Value.Name, "A110:loopCount: " + start);
                Assert.AreEqual(status, workflowStep.Value.Status, "A120: loopCount: " + start);
            }
        }



        [Test]
        public void WorkflowOverrideStep()
        {
            // Setup
            Workflow workflow = new Workflow(_workflowName);

            int stepNumber = 1;
            int status = 10;
            int subStatus = 0;

            WorkflowStep step = new WorkflowStep("Step 1", stepNumber++, status++, subStatus, Step1);
            workflow.AddStep(step);
            step = new WorkflowStep("Step 2", stepNumber++, status++, subStatus, Step1);
            workflow.AddStep(step);
            step = new WorkflowStep("Step 3", stepNumber++, status++, subStatus, Step1);
            workflow.AddStep(step);
            step = new WorkflowStep("Step 4", stepNumber, status, subStatus, Step1);
            workflow.AddStep(step);

            Assert.AreEqual(stepNumber, workflow.StepCount, "A10:");

            // Ensure they are in order:
            int start = 0;
            foreach (KeyValuePair<int, WorkflowStep> workflowStep in workflow.StepsInOrder)
            {
                start++;
                Assert.AreEqual(start, workflowStep.Value.StepNumber, "A100:");
                Assert.AreEqual("Step " + start, workflowStep.Value.Name, "A110:");
            }


            // Test
            step = new WorkflowStep("Step 3b", 3, 12, subStatus, Step1);
            workflow.OverrideStep(step);

            // Validate
            start = 0;
            foreach (KeyValuePair<int, WorkflowStep> workflowStep in workflow.StepsInOrder)
            {
                start++;
                Assert.AreEqual(start, workflowStep.Value.StepNumber, "A200:");
                if (start == 3)
                {
                    Assert.AreEqual("Step " + start + "b", workflowStep.Value.Name, "A300:");
                }
                else
                {
                    Assert.AreEqual("Step " + start, workflowStep.Value.Name, "400:");
                }
            }

        }





        [Test]
        public void WorkflowRemoveStep()
        {
            // Setup
            Workflow workflow = new Workflow(_workflowName);

            int stepNumber = 10;
            int status = 10;
            int subStatus = 0;
            int steps = 1;

            // Add 4 steps
            WorkflowStep step = new WorkflowStep("Step 1", stepNumber, status++, subStatus, Step1);
            workflow.AddStep(step);

            stepNumber += 10;
            step = new WorkflowStep("Step 2", stepNumber, status++, subStatus, Step1);
            steps++;
            workflow.AddStep(step);

            stepNumber += 10;
            step = new WorkflowStep("Step 3", stepNumber, status++, subStatus, Step1);
            steps++;
            workflow.AddStep(step);

            stepNumber += 10;
            step = new WorkflowStep("Step 4", stepNumber, status, subStatus, Step1);
            steps++;
            workflow.AddStep(step);

            stepNumber = stepNumber / 10;
            Assert.AreEqual(stepNumber, workflow.StepCount, "A10:");

            int start = 0;
            int stepCnt = 0;
            foreach (KeyValuePair<int, WorkflowStep> workflowStep in workflow.StepsInOrder)
            {
                start++;
                stepCnt = start * 10;
                Assert.AreEqual(stepCnt, workflowStep.Value.StepNumber, "A100:");
                Assert.AreEqual("Step " + start, workflowStep.Value.Name, "A110:");
            }

            // Test
            workflow.RemoveStep(30);

            // Validate
            start = 0;
            foreach (KeyValuePair<int, WorkflowStep> workflowStep in workflow.StepsInOrder)
            {
                start++;
                stepCnt = start * 10;
                if ( start == 3 ) {
                    stepCnt = (start + 1) * 10;
                    Assert.AreEqual(stepCnt, workflowStep.Value.StepNumber, "A200:");
                    Assert.AreEqual("Step " + (start + 1), workflowStep.Value.Name, "A210:");
                }
                else {
                    Assert.AreEqual(stepCnt, workflowStep.Value.StepNumber, "A100:");
                    Assert.AreEqual("Step " + start, workflowStep.Value.Name, "A110:");
                }
            }

        }


        [Test]
        public void WorkflowRunStep()
        {
            // Setup
            Workflow workflow = new Workflow(_workflowName);

            int stepNumber = 100;
            int stepCnt = 0;
            int status = 10;
            int subStatus = 0;

            WorkflowStep step = new WorkflowStep("Step 1", stepNumber++, status++, subStatus, Step1);
            stepCnt++;
            workflow.AddStep(step);

            step = new WorkflowStep("Step 2", stepNumber++, status++, subStatus, Step1);
            stepCnt++;
            workflow.AddStep(step);

            step = new WorkflowStep("Step 3", stepNumber++, status++, subStatus, Step1);
            stepCnt++;
            workflow.AddStep(step);
            
            step = new WorkflowStep("Step 4", stepNumber, status, subStatus, Step1);
            stepCnt++;
            workflow.AddStep(step);

            Assert.AreEqual(stepCnt, workflow.StepCount, "A10:");

            // Test - Run Step 2
            workflow.RunStep(11, null);

            // Validate
            Assert.AreEqual(RUNRESULT,_runResult,"A20:");
            Assert.AreEqual("Step 3",_runNextStep,"A30:");
        }


        [Test]
        public void RunStepThrows () {
            // Setup
            Workflow workflow = new Workflow(_workflowName);

            // Test - Run Step 2
            Assert.Throws<ArgumentException>(() => workflow.RunStep(10, null), "A10:");
        }


        [Test]
        public void WorkFlowStep_Success () {
            string name = "step 1";
            int status = 2;
            int subStatus = 5;
            int stepNumber = 100;

            WorkflowStep step = new WorkflowStep(name,stepNumber,status,subStatus,Step1);

            // Validate
            Assert.AreEqual(name,step.Name,"A10:");
            Assert.AreEqual(status, step.Status, "A20:");
            Assert.AreEqual(subStatus, step.SubStatus, "A30:");
            Assert.AreEqual(stepNumber, step.StepNumber, "A40:");
        }


        [Test]
        public void WorkFlowStep_ToString () {
            string name = "step 1";
            int status = 2;
            int subStatus = 5;
            int stepNumber = 100;

            WorkflowStep step = new WorkflowStep(name, stepNumber, status, subStatus, Step1);

            Assert.IsNotEmpty(step.ToString(),"A10:");
        }
    }

}