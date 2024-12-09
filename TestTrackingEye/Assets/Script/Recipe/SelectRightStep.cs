using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectRightStep : RecipeStep
{
    public SelectRightStep(Flowers flower, string instruction, int sceneIndex,string resultText) : base((int)flower, instruction, sceneIndex, resultText)
    {
        
    }
    public SelectRightStep(int value) : base(value) {}

    override public void SetInstruction()
    {
        this.instruction = string.Format(this.instruction, ((Flowers)value).ToString());
    }
  

    override public float EvaluateCompareStep(RecipeStep otherStep)
    {
        if(otherStep.Value == this.value)
        {
            return 1;
        }
        return 0;
    }
}
