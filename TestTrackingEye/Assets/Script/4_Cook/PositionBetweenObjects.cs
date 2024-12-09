using UnityEngine;

public class PositionBetweenObjects : MonoBehaviour
{
    public Transform Upper;
    public Transform Lower;

    public TemperatureController temperatureController;

    public bool useOptimalMin;

    void Update()
    {
        if (Lower != null && Upper != null && temperatureController != null)
        {
            // Basierend auf dem bool-Wert `useOptimalMin` den richtigen Wert auswählen
            float selectedTemperature = useOptimalMin
                ? temperatureController.optimalMin
                : temperatureController.optimalMax;

            // Temperaturbereich in einen Wert zwischen 0 und 1 normalisieren
            float percentage = Mathf.InverseLerp(
                temperatureController.minTemperature,
                temperatureController.maxTemperature,
                selectedTemperature
            );

            // Position zwischen den beiden Objekten berechnen
            transform.position = Vector3.Lerp(Lower.position, Upper.position, percentage);
        }
    }
}
