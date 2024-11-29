using UnityEngine;

[CreateAssetMenu(fileName = "SoSelectRightStep", menuName = "Scriptable Objects/SoSelectRightStep")]
public class SoSelectRightStep : SoRecipeStep
{
    [SerializeField] Flowers flower;

    public override RecipeStep CreateRecipeStepObject()
    {
        return new SelectRightStep(flower, instruction, sceneIndex);
    }

}
public enum Flowers
{
    Purple, Red, White, Three_Yellow, Blue, Dark_Purple
}
