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

        // Alle Marker zurücksetzen
        ResetMarkers();

        // Arbeite mit einer Kopie der Liste
        List<int> patternCopy = new List<int>(pattern);

        for (int i = 0; i < patternCopy.Count; i++)
        {
            int index = patternCopy[i];

            // Aktiviert den entsprechenden Marker (z. B. 1., 2., ...)
 
            ActivateMarker(index, i);

            yield return new WaitForSeconds(highlightDuration);
        }

        yield return new WaitForSeconds(highlightDuration);
        isShowingPattern = false; // Musteranzeige beenden
    }

    

    private void ActivateMarker(int gridIndex, int markerIndex)
    {
        // Hole das Grid-Feld
        GameObject gridField = gridFields[gridIndex];

        // Prüfe, ob das Grid-Feld 4 Child-Objekte hat
        if (gridField.transform.childCount >= markerIndex + 1)
        {
            // Aktiviere das entsprechende Child-Objekt
            Transform marker = gridField.transform.GetChild(markerIndex);
            marker.gameObject.SetActive(true);
        }
    }

    private void ResetMarkers()
    {
        // Setze alle Marker in allen Grid-Feldern zurück
        foreach (GameObject field in gridFields)
        {
            for (int i = 0; i < field.transform.childCount; i++)
            {
                field.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
