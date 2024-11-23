using UnityEngine;

public class PickFlowerStep : StepGameHandler
{
    SelectRightStep step;

    private void OnEnable()
    {
        CodeEventHandler.BasicSelection += GetSelection;
    }

    override public void EndStep()
    {
        print("Import note"+ step.Value);
        CodeEventHandler.Trigger_NextStepInRecipe(step);
    }
    public void GetSelection(int i)
    {
        step = new SelectRightStep(i);
        EndStep();
    }
    private void OnDisable()
    {
        CodeEventHandler.BasicSelection -= GetSelection;
    }

}
