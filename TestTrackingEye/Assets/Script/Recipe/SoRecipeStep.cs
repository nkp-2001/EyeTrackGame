using UnityEngine;

[CreateAssetMenu(menuName = "OwnScriptableObjects/SoRecipeStep")]
public class SoRecipeStep : ScriptableObject
{

   public int value = -1;
   public string instruction = "Do X";
   public int sceneIndex = -1;
   public bool errorBased = false;
   

    virtual public RecipeStep CreateRecipeStepObject()
    {
        return new RecipeStep(value, instruction, sceneIndex, errorBased);
    }


}
