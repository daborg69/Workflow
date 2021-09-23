using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sheakley.ICS
{
    public class Workflow : IWorkflow
    {
        /// <summary>
        /// The steps in dictionary with a key of the Status
        /// </summary>
        protected Dictionary<int, WorkflowStep> _logicSteps = new Dictionary<int, WorkflowStep>();

        /// <summary>
        /// The steps in a list sorted by the step number.
        /// </summary>
        //protected SortedList _stepsSorted = new SortedList();
        protected SortedList<int, WorkflowStep> _stepsSorted = new SortedList<int, WorkflowStep>();


        /// <summary>
        /// Name for this workflow
        /// </summary>
        public string Name { get; protected set; }


        /// <summary>
        /// Returns the number of steps in the workflow
        /// </summary>
        public int StepCount {
            get { return _stepsSorted.Count; }
        }


        /// <summary>
        /// The workflow steps in order
        /// </summary>
        public SortedList<int,WorkflowStep> StepsInOrder {
        //public SortedList StepsInOrder
        get { return _stepsSorted; }
        }


        public Workflow (string name) { Name = name;}


        /// <summary>
        /// Adds a new workflow step
        /// </summary>
        /// <param name="step"></param>
        public void AddStep(WorkflowStep step)
        {
            _logicSteps.Add(step.Status, step);
            _stepsSorted.Add(step.StepNumber, step);
        }


        /// <summary>
        /// Overrides a workflow step.
        /// </summary>
        /// <param name="step"></param>
        public void OverrideStep(WorkflowStep step)
        {
            // Remove Existing Step
            RemoveStep(step.StepNumber);
            AddStep(step);
        }


        /// <summary>
        /// Removes an existing workflow step
        /// </summary>
        /// <param name="stepNumber">The Step Number to remove</param>

        public void RemoveStep(int stepNumber)
        {
            // Get the step for the dictionary lookup
            if ( !_stepsSorted.TryGetValue(stepNumber, out WorkflowStep step) )
                throw new ArgumentException("A step with step number [" + stepNumber + "] does not exist.");
            _logicSteps.Remove(step.Status);
            _stepsSorted.Remove(step.StepNumber);
        }


        /// <summary>
        /// Removes the given Step object from the lists.
        /// </summary>
        /// <param name="stepToRemove"></param>
        public void RemoveStep (WorkflowStep stepToRemove) {
            RemoveStep(stepToRemove.StepNumber);
        }



        /// <summary>
        /// Runs a workflow step based upon the Status code passed in.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="data">Should contain both Status and SubStatus properties, plus other necessary data</param>
        /// <returns></returns>
        public bool RunStep(int status, object data)
        {
            // Find Status.
            if (!_logicSteps.TryGetValue(status, out WorkflowStep step)) throw new ArgumentException("The status [" + status + "] does not exist in the steps to be executed");
            if (step == null) throw new ArgumentNullException("The step for status [" + status + "] is null");

            // Get current step in sorted list, so we can get the next step
            WorkflowStep nextStep = null;

            int x = _stepsSorted.IndexOfKey(step.StepNumber) + 1;
            if ( x < _stepsSorted.Count ) nextStep = _stepsSorted.Values [x];
            
            //int currentStep = _stepsSorted [step.StepNumber].StepNumber; //_stepsSorted.IndexOfKey(step.StepNumber) + 1;
            //if (currentStep < _stepsSorted.Count)
            //    nextStep = _stepsSorted[currentStep];


            // Run the step logic
            return step.Execute(data, nextStep);
        }
    }
}
