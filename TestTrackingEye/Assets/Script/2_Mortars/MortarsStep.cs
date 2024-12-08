using System.Linq;
using UnityEngine;

public class MortarsStep : StepGameHandler
{
    RecipeStep step;
    bool[] lookedATPostions = new bool[3] {false,false,false};
    [SerializeField] float maxTime;
    [SerializeField] float time;
    [SerializeField] GameObject TimeIndiactor;
    [SerializeField] MortalShuffle mortalShuffle;

    [SerializeField] int stepsToDO = 3;
    [SerializeField] bool selectFirstField = true;

    bool blocked = false;

    [SerializeField] AudioClip succesSound;
    [SerializeField] AudioClip failSound;

    [SerializeField] ChangeMaterial markerPitfull; 
    [SerializeField] ChangeMaterial markerBowl;

    private void Start()
    {
        time = maxTime;
        RecipeMangment recipeMangment = FindAnyObjectByType<RecipeMangment>();
        if(recipeMangment != null)
        {
            stepsToDO = recipeMangment.GetCurrentStepData().Value;
        }
        else
        {
           stepsToDO = 3; // FallBack
        }
      


    }
  

    private void OnEnable()
    {
        CodeEventHandler.BasicSelection += GetSelection;
        CodeEventHandler.SelectionSetBlock += SetBlock;
    }
    private void OnDisable()
    {
        CodeEventHandler.BasicSelection -= GetSelection;
        CodeEventHandler.SelectionSetBlock -= SetBlock;
    }
    override public void EndStep()
    {
        step = new RecipeStep(true, score + stepsToDO*2); // Secound Argument Errors and *2 of missed Times
        CodeEventHandler.Trigger_NextStepInRecipe(step);
    }

    override public void GetSelection(int i)
    {
        if (blocked) // 1 = First to selected // 2 = Secound to Selcted // Rest = wrong
        {         
            if (selectFirstField)
            {
                if (i == 1)
                {
                    markerPitfull.HightLighting();
                    selectFirstField = false;
                    AudioSource.PlayClipAtPoint(succesSound,transform.position,0.5f);
                }
                else
                {
                    score++; // FAIL Score Increase
                    AudioSource.PlayClipAtPoint(failSound, transform.position, 0.5f);
                }
            }
            else
            {
                if (i == 2)
                {
     
                    blocked = true;
                    stepsToDO--;
                    AudioSource.PlayClipAtPoint(succesSound, transform.position,1);
                    if (stepsToDO <= 0)
                    {
                        EndStep();
                    }
                    else
                    {
                        markerPitfull.UnHightLighting();
                        mortalShuffle.Shuffle();
                        selectFirstField = true;
                    }
                }
                else if (i != 1)
                {
                    score++;
                    AudioSource.PlayClipAtPoint(failSound, transform.position);// FAIL Score Increase

                }
            }
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
    public void SetBlock(bool on)
    {
        blocked = on;
    }


}
