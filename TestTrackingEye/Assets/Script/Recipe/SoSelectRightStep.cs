using UnityEngine;

[CreateAssetMenu(fileName = "SoSelectRightStep", menuName = "Scriptable Objects/SoSelectRightStep")]
public class SoSelectRightStep : SoRecipeStep
{
    [SerializeField] Flowers flower;

    public override RecipeStep CreateRecipeStepObject()
    {
        return new SelectRightStep(flower, instruction, sceneIndex,resultText);
    }

}
public enum Flowers
{
    Red_Flowers, White_Flowers, Pink_Flower, Light_Blue_Flowers, Green_Flowers, Mushroom
}
