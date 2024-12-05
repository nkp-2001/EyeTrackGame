using UnityEngine;
using UnityEngine.UI;

public class OptimalRangeChecker : MonoBehaviour
{
    public Slider temperatureSlider;        // Der Slider (mit minValue = 0, maxValue = 100)
    public RectTransform optimalRangeImage; // Das RectTransform f¸r den Optimalbereich
    public RectTransform minIndicator;      // Indikator f¸r den minimalen Optimalbereich
    public RectTransform maxIndicator;      // Indikator f¸r den maximalen Optimalbereich
    public float minValue = 30f;            // Minimum des Optimalbereichs (im Bereich 0-100)
    public float maxValue = 70f;            // Maximum des Optimalbereichs (im Bereich 0-100)

    void Start()
    {
        UpdateIndicatorPositions();
    }

    void Update()
    {
        if (temperatureSlider != null && optimalRangeImage != null)
        {
            // Pr¸fe, ob der Sliderwert im Bereich liegt
            float sliderValue = temperatureSlider.value;

            if (sliderValue >= minValue && sliderValue <= maxValue)
            {
                // Sliderwert liegt im Optimalbereich - Farbe ‰ndern
                optimalRangeImage.GetComponent<Image>().color = Color.green;
            }
            else
            {
                // Sliderwert liegt auﬂerhalb des Bereichs - andere Farbe
                optimalRangeImage.GetComponent<Image>().color = Color.red;
            }
        }
    }

    void UpdateIndicatorPositions()
    {
        if (temperatureSlider != null && minIndicator != null && maxIndicator != null)
        {
            // Position des minimalen Indikators
            float normalizedMin = Mathf.InverseLerp(temperatureSlider.minValue, temperatureSlider.maxValue, minValue);
            Vector3 minPos = new Vector3(normalizedMin * temperatureSlider.GetComponent<RectTransform>().sizeDelta.x, 0, 0);
            minIndicator.anchoredPosition = minPos;

            // Position des maximalen Indikators
            float normalizedMax = Mathf.InverseLerp(temperatureSlider.minValue, temperatureSlider.maxValue, maxValue);
            Vector3 maxPos = new Vector3(normalizedMax * temperatureSlider.GetComponent<RectTransform>().sizeDelta.x, 0, 0);
            maxIndicator.anchoredPosition = maxPos;
        }
    }
}
