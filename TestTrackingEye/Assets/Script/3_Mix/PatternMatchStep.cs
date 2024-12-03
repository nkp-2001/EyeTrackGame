using UnityEngine;
using System.Collections.Generic;

public class PatternMatchStep : StepGameHandler
{
    public PatternVisualizer patternVisualizer; // Referenz für die Musteranzeige
    public PatternGenerator patternGenerator;   // Referenz für die Musterlogik
    public List<int> currentPattern;
    public int currentIndex = 0;
    public int stepsToDO;

    private void Start()
    {
        RecipeMangment recipeMangment = FindAnyObjectByType<RecipeMangment>();
        if (recipeMangment != null)
        {
            stepsToDO = recipeMangment.GetCurrentStepData().Value;
        }
        else
        {
            stepsToDO = 1; // FallBack
        }
    }
    private void OnEnable()
    {
        // Registriere das Event
        CodeEventHandler.BasicSelection += HandleSelection;

        // Generiere und zeige das erste Muster
        GenerateNewPattern();
    }

    private void OnDisable()
    {
        // Entferne das Event
        CodeEventHandler.BasicSelection -= HandleSelection;
    }

    private void GenerateNewPattern()
    {
        // Generiere neues Muster und zeige es an
        patternGenerator.GenerateConnectedPattern();
        currentPattern = new List<int>(patternGenerator.pattern);
        currentIndex = 0;

        if (patternVisualizer != null)
        {
            patternVisualizer.pattern = currentPattern;
            patternVisualizer.ShowPattern();
        }
    }

    private void HandleSelection(int selectedValue)
    {
        Debug.Log($"Selected: {selectedValue}, Expected: {currentPattern[currentIndex]}");

        if (selectedValue == currentPattern[currentIndex])
        {
            currentIndex++;
            Debug.Log($"Correct! CurrentIndex: {currentIndex}");

            if (currentIndex >= currentPattern.Count)
            {
                stepsToDO--;

                Debug.Log("Pattern successfully matched!");
                
                if (stepsToDO <= 0)
                {
                    EndStep();
                }
                else
                {
                    GenerateNewPattern();
                }
            }
        }
        else
        {
            Debug.Log("Incorrect selection. Restarting pattern.");
            currentIndex = 0;

            if (patternVisualizer != null)
            {
                score++;
                patternVisualizer.ShowPattern();
            }
        }
    }

    override public void EndStep()
    {
        Debug.Log("Minispiel abgeschlossen!");
        CodeEventHandler.Trigger_NextStepInRecipe(new RecipeStep(true, score)); // Passe die Logik an
    }
}
