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

        // Hole das erste Child-Objekt des Grid-Feldes, das als MarkerGroup dient
        Transform markerGroup = gridField.transform.GetChild(0);

        // Prüfe, ob MarkerGroup genügend Marker hat
        if (markerGroup != null && markerGroup.childCount > markerIndex)
        {
            Transform marker = markerGroup.GetChild(markerIndex);
            marker.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"MarkerGroup oder Marker {markerIndex} nicht gefunden in {gridField.name}");
        }
    }

    private void ResetMarkers()
    {
        // Setze alle Marker in allen Grid-Feldern zurück
        foreach (GameObject field in gridFields)
        {
            // Hole das erste Child-Objekt als MarkerGroup
            Transform markerGroup = field.transform.GetChild(0);

            // Überprüfen, ob MarkerGroup existiert und die Marker zurücksetzen
            if (markerGroup != null)
            {
                for (int i = 0; i < markerGroup.childCount; i++)
                {
                    markerGroup.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }



}
