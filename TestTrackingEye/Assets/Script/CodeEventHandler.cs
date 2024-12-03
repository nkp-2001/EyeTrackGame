using System;
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

    // Neue Events für Temperatursteuerung
    public static event Action IncreaseTemperature;
    public static void Trigger_IncreaseTemperature() { IncreaseTemperature?.Invoke(); }

    public static event Action DecreaseTemperature;
    public static void Trigger_DecreaseTemperature() { DecreaseTemperature?.Invoke(); }

    public static event Action StopTemperatureChange;
    public static void Trigger_StopTemperatureChange() { StopTemperatureChange?.Invoke(); }
}
