using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class RecipeMangment : MonoBehaviour
{
    [SerializeField] Recipe recipe;

    public List<RecipeStep> recipeSteps;
    public List<RecipeStep> playerPerformanceSteps;
    [SerializeField] int step = 0;
    Animator animator;
    bool Loading = false;
    bool onFirstScene = true;

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

        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if(onFirstScene)
            {
                StartSystem();
            }
            else
            {
                RecordAndNextStep(new RecipeStep(true));          
            }        
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GetSteps(Recipe recipe)
    {
        recipeSteps = recipe.GetRecipeSteps();
    }
    public List<string> GetAllStepsInstrcutions()
    {
        List<string> instruction = new List<string>(); ;
        foreach(RecipeStep step in recipeSteps)
            instruction.Add(step.Instruction);
        return instruction;
    }

    public void NextStep()
    {      
            Loading = true;
            step++;
            if (step >= recipeSteps.Count)
            {
                StartCoroutine(LoadingSceneWithTransition(3));
            }
            else
            {
                StartCoroutine(LoadingSceneWithTransition(recipeSteps[step].SceneIndex));
            }     
    }
    public void RecordAndNextStep(RecipeStep step)
    {
        if (!Loading)
        {
            Loading = true;
            playerPerformanceSteps.Add(step);
            print(playerPerformanceSteps[0]);
            NextStep();
        }
      
    }
    public void StartSystem() // for steo
    {
        ResetRecord();
        step = 0;
        onFirstScene = false;
        if (!Loading)
        {
            Loading = true;
            StartCoroutine(LoadingSceneWithTransition(recipeSteps[step].SceneIndex));
        }
      
    }
    private void OnDisable()
    {
        CodeEventHandler.StartBrewing -= StartSystem;
        CodeEventHandler.NextStepInRecipe -= RecordAndNextStep;
    }
    public RecipeStep GetCurrentStepData()
    {
        return recipeSteps[step];
    }
    public String GetRecipeTitel()
    {
        return recipe.GetTitel();
    }
    IEnumerator LoadingSceneWithTransition(int i)
    { 
            if (animator != null)
            {
                print("FadeOutComeing");
                animator.SetTrigger("FadeOut");
                yield return new WaitForSeconds(0.20f);
            }
            SceneManager.LoadScene(i);
            Loading = false;
            animator.SetTrigger("FadeIn");
    }
    public void ResetRecord()
    {
        playerPerformanceSteps = new List<RecipeStep>();
    }


}
