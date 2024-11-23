using UnityEngine;

[CreateAssetMenu(menuName = "OwnScriptableObjects/SoRecipeStep")]
public class SoRecipeStep : ScriptableObject
{
   
   [SerializeField] int value = -1;
   [SerializeField] string instruction = "Do X";
   [SerializeField] int sceneIndex = -1;

    public RecipeStep CreateRecipeStepObject()
    {
        return new RecipeStep(value, instruction, sceneIndex);
    }


}
