using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PatternMatchStep : StepGameHandler
{
    public PatternVisualizer patternVisualizer; // Referenz f�r die Musteranzeige
    public PatternGenerator patternGenerator;   // Referenz f�r die Musterlogik
    public List<int> currentPattern;
    public int currentIndex = 0;
    public int stepsToDO;

    [SerializeField] AudioClip succesSound;
    [SerializeField] AudioClip failSound;


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
            AudioSource.PlayClipAtPoint(succesSound, transform.position, 0.5f);

            // Marker beim ausgew�hlten Feld deaktivieren
            DeactivateMarker(selectedValue);

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

                // Alle Marker wieder sichtbar machen
                ResetAllMarkers();
                AudioSource.PlayClipAtPoint(failSound, transform.position, 0.5f);

                patternVisualizer.ShowPattern();
            }
        }
    }

    private void DeactivateMarker(int selectedValue)
    {
        // Hole das Grid-Feld anhand der ID
        GameObject field = patternVisualizer.gridFields[selectedValue];

        // Deaktiviere alle Marker im Grid-Feld
        GameObject child = field.transform.GetChild(0).gameObject;
        StartCoroutine(FadeOutAndDeactivate(child));
        
    }

    private IEnumerator FadeOutAndDeactivate(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if(renderer != null)
        {
            Material material = renderer.material;
            Color color = material.color;
            float duration = 1f;
            float elapsed = 0f;

            while(elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
                material.color = new Color(color.r, color.g, color.b, alpha);
                yield return null;
            }

            material.color = new Color(color.r, color.g, color.b, 0f);
        }
        obj.SetActive(false);
    }

    private void ResetAllMarkers()
    {
        // Setze alle Marker in allen Grid-Feldern zur�ck
        foreach (GameObject field in patternVisualizer.gridFields)
        {
            for (int i = 0; i < field.transform.childCount; i++)
            {
                field.transform.GetChild(i).gameObject.SetActive(true);
            }
        }        
    }

    override public void EndStep()
    {
        Debug.Log("Minispiel abgeschlossen!");
        CodeEventHandler.Trigger_NextStepInRecipe(new RecipeStep(true, score)); // Passe die Logik an
    }
}
