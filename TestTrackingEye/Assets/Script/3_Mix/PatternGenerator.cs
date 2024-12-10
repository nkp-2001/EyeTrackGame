using System.Collections.Generic;
using UnityEngine;

public class PatternGenerator : MonoBehaviour
{
    public GameObject[] gridFields; // Alle Felder in deinem 2x4-Grid
    public int gridWidth = 3; // Anzahl der Felder pro Zeile
    public List<int> pattern = new List<int>(); // Das generierte Muster
    public int patternLength = 4; // L�nge des gew�nschten Musters

    public void GenerateConnectedPattern()
    {
        pattern.Clear();

        // Starte mit einem zuf�lligen Feld
        int currentField = Random.Range(0, gridFields.Length);
        pattern.Add(currentField);

        // Generiere das Muster
        for (int i = 1; i < patternLength; i++)
        {
            int nextField = GetNextNeighbor(currentField);

            if (nextField == -1)
            {
                Debug.LogWarning("Keine g�ltigen Nachbarn mehr verf�gbar!");
                break;
            }

            pattern.Add(nextField);
            currentField = nextField; // Aktualisiere das aktuelle Feld
        }

        Debug.Log("Generiertes Muster: " + string.Join(", ", pattern));
    }

    private int GetNextNeighbor(int currentIndex)
    {
        List<int> neighbors = GetValidNeighbors(currentIndex);

        if (neighbors.Count == 0)
            return -1; // Keine Nachbarn verf�gbar

        return neighbors[Random.Range(0, neighbors.Count)]; // W�hle zuf�lligen Nachbarn
    }

    private List<int> GetValidNeighbors(int index)
    {
        List<int> neighbors = new List<int>();

        int row = index / gridWidth;
        int col = index % gridWidth;

        // Oben (gleiche Spalte, eine Zeile dr�ber)
        if (row > 0)
        {
            int above = index - gridWidth;
            if (!pattern.Contains(above)) neighbors.Add(above);
        }

        // Unten (gleiche Spalte, eine Zeile drunter)
        if (row < 1) // Es gibt nur zwei Zeilen (0 und 1)
        {
            int below = index + gridWidth;
            if (!pattern.Contains(below)) neighbors.Add(below);
        }

        // Links (gleiche Zeile, eine Spalte links)
        if (col > 0)
        {
            int left = index - 1;
            if (!pattern.Contains(left) && (left / gridWidth == row)) // Gleiche Zeile pr�fen
                neighbors.Add(left);
        }

        // Rechts (gleiche Zeile, eine Spalte rechts)
        if (col < gridWidth - 1)
        {
            int right = index + 1;
            if (!pattern.Contains(right) && (right / gridWidth == row)) // Gleiche Zeile pr�fen
                neighbors.Add(right);
        }

        return neighbors;
    }
}
