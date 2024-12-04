using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PotionEvalation : MonoBehaviour
{
    RecipeMangment mangment;
    [SerializeField] TextMeshProUGUI text;
    private void Start()
    {
        mangment = FindAnyObjectByType<RecipeMangment>();


        List<RecipeStep> RecipeSteps = mangment.recipeSteps;
        List<RecipeStep> playerPerformanceSteps = mangment.playerPerformanceSteps;


        for (int i = 0; i < mangment.playerPerformanceSteps.Count ; i++)
        {
            float value = playerPerformanceSteps[i].EvaluateCompareStep(RecipeSteps[i]);
            string[] descriptions = { "Correctly pick a flower", "Grind the ingredients", "Stir correctly", "Properly boil" };
            text.text += "\n" + descriptions[i] + ": " + value;
        }

       


    }

}
