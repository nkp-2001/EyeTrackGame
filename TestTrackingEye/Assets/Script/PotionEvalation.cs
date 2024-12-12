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
        List<RecipeStep> recipeInfo = mangment.recipeSteps;

      
        print(playerPerformanceSteps.Count);


        for (int i = 0; i < mangment.playerPerformanceSteps.Count ; i++)
        {
            float value = playerPerformanceSteps[i].EvaluateCompareStep(RecipeSteps[i]) *100; 
            string desicptionResult = recipeInfo[i].ResultScreenText;
            text.text +=  desicptionResult + ": " + value + "%" + "\n";
        }

       


    }

}
