using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternVisualizer : MonoBehaviour
{
    public GameObject[] gridFields; // Array mit allen Grid-Feldern
    public List<int> pattern = new List<int>(); // Das Muster
    public float highlightDuration = 0.5f; // Dauer des Highlights
    public Material highlightMaterial; // Material fürs Hervorheben
    public Material defaultMaterial; // Standard-Material
    public bool isShowingPattern = false; // Flag, ob das Muster gerade angezeigt wird

    public void ShowPattern()
    {
        StartCoroutine(ShowPatternCoroutine());
    }

    private IEnumerator ShowPatternCoroutine()
    {
        isShowingPattern = true; // Musteranzeige starten

        // Arbeite mit einer Kopie der Liste
        List<int> patternCopy = new List<int>(pattern);

        foreach (int index in patternCopy)
        {
            HighlightField(index);
            yield return new WaitForSeconds(highlightDuration);
        }

        yield return new WaitForSeconds(highlightDuration);
        //ResetAllFields(); // Setze das Material zurück
        isShowingPattern = false; // Musteranzeige beenden
    }


    private void HighlightField(int index)
    {
        // Ändere das Material des MeshRenderers
        MeshRenderer renderer = gridFields[index].GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = highlightMaterial;
        }
    }

    private void ResetAllFields()
    {
        foreach (GameObject field in gridFields)
        {
            if (field == null) continue; // Überspringe null-Felder
            MeshRenderer renderer = field.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material = defaultMaterial;
            }
        }
    }
}
