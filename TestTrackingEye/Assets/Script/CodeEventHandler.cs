using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeEventHandler : MonoBehaviour
{
    public static event Action StartBrewing;
    public static void Trigger_StartBrewing() { StartBrewing?.Invoke(); }

    public static event Action<RecipeStep> NextStepInRecipe;

    public static void Trigger_NextStepInRecipe(RecipeStep step) { NextStepInRecipe?.Invoke(step); }

    public static event Action<int> BasicSelection;

    public static void Trigger_BasicSelection(int value) { BasicSelection?.Invoke(value); }

    public static event Action<bool> SelectionSetBlock;

    public static void Trigger_SelectionSetBlock(bool on) { SelectionSetBlock?.Invoke(on); }


}
