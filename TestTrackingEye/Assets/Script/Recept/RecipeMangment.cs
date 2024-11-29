using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class RecipeMangment : MonoBehaviour
{
    [SerializeField] Recipe recipe;

    public List<RecipeStep> recipeSteps;
    public List<RecipeStep> playerPerformanceSteps;
    [SerializeField] int step = 0;

  

    private void OnEnable()
    {
        CodeEventHandler.StartBrewing += StartSystem;
        CodeEventHandler.NextStepInRecipe += RecordAndNextStep;
    }
    void Awake()
    {
        RecipeMangment[] objs = FindObjectsByType<RecipeMangment>(FindObjectsSortMode.None);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        GetSteps(recipe);
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        playerPerformanceSteps = new List<RecipeStep>();
    }

    public void GetSteps(Recipe recipe)
    {

        recipeSteps = recipe.GetRecipeSteps();
    }
    public List<string> GetStepsInstrcution()
    {
        List<string> instruction = new List<string>(); ;
        foreach(RecipeStep step in recipeSteps)
            instruction.Add(step.Instruction);
        return instruction;
    }

    public void NextStep()
    {
        step++;
        if(step >= recipeSteps.Count)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(recipeSteps[step].SceneIndex);
        }
    }
    public void RecordAndNextStep(RecipeStep step)
    {
        playerPerformanceSteps.Add(step);
        print(playerPerformanceSteps[0]);
        NextStep();
    }
    public void StartSystem() // for steo
    {
        step = 0;
        SceneManager.LoadScene(recipeSteps[step].SceneIndex);
    }
    private void OnDisable()
    {
        CodeEventHandler.StartBrewing -= StartSystem;
        CodeEventHandler.NextStepInRecipe -= RecordAndNextStep;
    }


}
