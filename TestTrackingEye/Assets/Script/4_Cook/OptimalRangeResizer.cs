using UnityEngine;
using UnityEngine.UI;

public class OptimalRangeResizer : MonoBehaviour
{
    public Slider temperatureSlider;        // Der Slider (mit minValue = 0, maxValue = 100)
    public RectTransform optimalRangeImage; // Das RectTransform für den Optimalbereich
    public float minValue = 30f;            // Minimum des Optimalbereichs (im Bereich 0-100)
    public float maxValue = 70f;            // Maximum des Optimalbereichs (im Bereich 0-100)

    void Start()
    {
        AdjustOptimalRange();
    }

    private void Update()
    {
        AdjustOptimalRange();
    }

    public void AdjustOptimalRange()
    {
        if (temperatureSlider != null && optimalRangeImage != null)
        {
            // Berechne die Normalisierten Werte für minValue und maxValue (zwischen 0 und 1)
            float normalizedMin = (minValue - temperatureSlider.minValue) / (temperatureSlider.maxValue - temperatureSlider.minValue);
            float normalizedMax = (maxValue - temperatureSlider.minValue) / (temperatureSlider.maxValue - temperatureSlider.minValue);

            // Setze die Anker für das optimalRangeImage
            optimalRangeImage.anchorMin = new Vector2(normalizedMin, optimalRangeImage.anchorMin.y);
            optimalRangeImage.anchorMax = new Vector2(normalizedMax, optimalRangeImage.anchorMax.y);

            // Setze die Position des optimalRangeImage auf (0, 0) relativ zu seinen Ankern
            optimalRangeImage.anchoredPosition = Vector2.zero;
        }
    }
}
