using System.Linq;
using UnityEngine;

public class MortarsStep : StepGameHandler
{
    RecipeStep step;
    bool[] lookedATPostions = new bool[3] {false,false,false};
    [SerializeField] float maxTime;
    [SerializeField] float time;
    [SerializeField] GameObject TimeIndiactor;

    private void Start()
    {
        time = maxTime;
    }

    private void OnEnable()
    {
        CodeEventHandler.BasicSelection += GetSelection;
    }
    private void OnDisable()
    {
        CodeEventHandler.BasicSelection -= GetSelection;
    }
    override public void EndStep()
    {
        step = new RecipeStep(score);
        CodeEventHandler.Trigger_NextStepInRecipe(step);
    }
    override public void GetSelection(int i)
    {
        lookedATPostions[i] = true;
        if (lookedATPostions.All(entry => entry)) // One = going one direction and back
        {
            lookedATPostions = new bool[3] { false, false, false };
            score++;
        }

    }
    private void Update()
    {
        time -= Time.deltaTime;
        TimeIndiactor.transform.localScale = new Vector3(time / maxTime, TimeIndiactor.transform.localScale.y, TimeIndiactor.transform.localScale.z);

        if (time < 0)
        {
           EndStep();
        }
    }


}
