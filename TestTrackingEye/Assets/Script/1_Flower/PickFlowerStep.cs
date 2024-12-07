using UnityEngine;
using System.Collections;


public class PickFlowerStep : StepGameHandler
{
    SelectRightStep step;
    [SerializeField] float markedAfter = 8;

    private void OnEnable()
    {
        CodeEventHandler.BasicSelection += GetSelection;
    }
    private void Start()
    {
        StartCoroutine(MarkAfterSecounds(markedAfter));
    }
    IEnumerator MarkAfterSecounds(float time)
    {
        yield return new WaitForSeconds(markedAfter);

        RecipeMangment recipeMangment = FindAnyObjectByType<RecipeMangment>();
        if (recipeMangment != null) 
        {
            CodeEventHandler.Trigger_HightLightFlower(((SelectRightStep)recipeMangment.GetCurrentStepData()).Value);
           
        }
        else
        {
            CodeEventHandler.Trigger_HightLightFlower(1);
        }

       
    }

    override public void EndStep()
    {
        print("Import note"+ step.Value);
        CodeEventHandler.Trigger_NextStepInRecipe(step);
    }
    override public void GetSelection(int i)
    {
        step = new SelectRightStep(i);
        EndStep();
    }


    private void OnDisable()
    {
        CodeEventHandler.BasicSelection -= GetSelection;
    }

}
