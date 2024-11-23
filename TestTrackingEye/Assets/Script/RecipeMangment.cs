using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RecipeMangment : MonoBehaviour
{
    [SerializeField] Recipe recipe;

    [SerializeField] private List<RecipeStep> RecipeSteps;
    [SerializeField] private List<RecipeStep> playerPerformanceSteps;
    [SerializeField] int step = 0;

    private void OnEnable()
    {
        CodeEventHandler.StartBrewing += StartSystem;
    }
    void Awake()
    {
        GetSteps(recipe);

        DontDestroyOnLoad(this.gameObject);
    }


    public void GetSteps(Recipe recipe)
    {
        RecipeSteps = recipe.GetRecipeSteps();
    }
    public List<string> GetStepsInstrcution()
    {
        List<string> instruction = new List<string>(); ;
        foreach(RecipeStep step in RecipeSteps)
            instruction.Add(step.Instruction);
        return instruction;
    }

    public void NextStep()
    {
        step++;
        if(step >= RecipeSteps.Count)
        {
            // End of Recipe Load Result Scene Here 
        }
        else
        {
            SceneManager.LoadScene(RecipeSteps[step].SceneIndex);
        }
    }
    public void RecordAndNextStep(RecipeStep step)
    {
        playerPerformanceSteps.Add(step);
        NextStep();
    }
    public void StartSystem() // for steo
    {
        step = 0;
        SceneManager.LoadScene(RecipeSteps[step].SceneIndex);
    }
    private void OnDisable()
    {
        CodeEventHandler.StartBrewing -= StartSystem;
    }


}
