using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "OwnScriptableObjects/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] string Titel;
    [SerializeField] private List<SoRecipeStep> recipeSteps;

    public List<SoRecipeStep> GetRecipeStepsRaw() { return recipeSteps; }

    public List<RecipeStep> GetRecipeSteps() {
        List <RecipeStep> recipeStepsObj = new List<RecipeStep>();
        
        foreach (SoRecipeStep step in recipeSteps)
        {
            recipeStepsObj.Add(step.CreateRecipeStepObject());
        }
        return recipeStepsObj;


       
    }
    public string GetTitel()
    {
        return Titel;
    }


}
