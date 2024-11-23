using System;
using UnityEngine;


public class RecipeStep
{
    protected int value = -1;

    protected string instruction = "Null";

    protected int nextsceneIndex = -1;

    public RecipeStep(int value, string instruction,int sceneIndex)
    {
        this.value = value;
        this.nextsceneIndex = sceneIndex;
        this.instruction = instruction;
        SetInstruction();
    }
    public RecipeStep(int value)
    {
        this.value = value;
    }

    public int Value { get => value; set => this.value = value; }
    public string Instruction { get => instruction; set => instruction = value; }
    public int SceneIndex { get => nextsceneIndex; set => nextsceneIndex = value; }

    virtual public void SetInstruction()
    {
        this.instruction = string.Format(this.instruction,value);
    }

    virtual public float EvaluateCompareStep(RecipeStep otherStep)
    {
        int diffrence = Math.Abs(otherStep.value - this.value);

        float result = (1 - (diffrence * 0.3f));
        if(result < 0.0f)
        {
            result = 0.0f;
        }

        return result;


    }
}
