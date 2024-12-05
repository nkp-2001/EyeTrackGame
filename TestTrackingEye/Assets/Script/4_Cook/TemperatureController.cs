using UnityEngine;
using UnityEngine.UI;

public class TemperatureController : MonoBehaviour
{
    [Header("UI Components")]
    public Slider temperatureSlider; // Der UI-Slider, der die aktuelle Temperatur anzeigt
    public Image sliderFill; // Das Bild, das die Füllfarbe des Sliders anzeigt
    public RectTransform optimalMinIndicator; // Indikator für den unteren Bereich
    public RectTransform optimalMaxIndicator; // Indikator für den oberen Bereich

    [Header("Temperature Settings")]
    public float minTemperature = 0f; // Minimale Temperatur
    public float maxTemperature = 100f; // Maximale Temperatur
    public float optimalMin = 40f; // Untere Grenze des optimalen Bereichs
    public float optimalMax = 60f; // Obere Grenze des optimalen Bereichs
    public float temperatureChangeSpeed = 20f; // Geschwindigkeit, mit der sich die Temperatur ändert
    public float temperatureAutoChangeSpeed = 0.005f; // Geschwindigkeit, mit der sich die Temperatur automatisch ändert


    public float currentTemperature; // Aktuelle Temperatur
    private bool isIncreasing = false; // Gibt an, ob die Temperatur gerade steigt
    private bool isDecreasing = false; // Gibt an, ob die Temperatur gerade sinkt

    void Start()
    {
        // Initialisiere die Temperatur auf 0 und setze die Slider-Werte
        currentTemperature = 50f; // Temperatur beginnt bei der hälfte
        temperatureSlider.minValue = minTemperature;
        temperatureSlider.maxValue = maxTemperature;
        UpdateSlider(); // Aktualisiere die UI-Anzeige
        UpdateIndicators(); // Positioniere die Indikatoren
    }

    void Update()
    {
        currentTemperature -= temperatureAutoChangeSpeed * Time.deltaTime;

        // Temperatur basierend auf den Eingaben erhöhen oder verringern
        if (isIncreasing)
        {
            currentTemperature += temperatureChangeSpeed * Time.deltaTime;
        }
        else if (isDecreasing)
        {
            currentTemperature -= temperatureChangeSpeed * Time.deltaTime;
        }

        // Temperatur innerhalb der festgelegten Grenzen halten
        currentTemperature = Mathf.Clamp(currentTemperature, minTemperature, maxTemperature);

        // Aktualisiere die UI-Anzeige des Sliders
        UpdateSlider();

        // Ändere die Farbe des Sliders basierend darauf, ob die Temperatur im optimalen Bereich liegt
        if (currentTemperature >= optimalMin && currentTemperature <= optimalMax)
        {
            sliderFill.color = Color.green; // Grün, wenn die Temperatur optimal ist
        }
        else
        {
            sliderFill.color = Color.red; // Rot, wenn die Temperatur außerhalb des optimalen Bereichs ist
        }
    }

    // Methode, um das Erhöhen der Temperatur zu starten oder zu stoppen
    public void IncreaseTemperature(bool state)
    {
        isIncreasing = state;
    }

    // Methode, um das Verringern der Temperatur zu starten oder zu stoppen
    public void DecreaseTemperature(bool state)
    {
        isDecreasing = state;
    }

    // Aktualisiert die Position des Sliders basierend auf der aktuellen Temperatur
    private void UpdateSlider()
    {
        temperatureSlider.value = currentTemperature;
    }

    // Aktualisiere die Position der Indikatoren
    private void UpdateIndicators()
    {
        float sliderHeight = temperatureSlider.GetComponent<RectTransform>().sizeDelta.y;

        if (optimalMinIndicator != null)
        {
            // Berechne die Y-Position für optimalMin in Prozent relativ zur Slider-Höhe
            float minPercent = (optimalMin - minTemperature) / (maxTemperature - minTemperature);
            optimalMinIndicator.anchoredPosition = new Vector2(optimalMinIndicator.anchoredPosition.x, sliderHeight * minPercent);
        }

        if (optimalMaxIndicator != null)
        {
            // Berechne die Y-Position für optimalMax in Prozent relativ zur Slider-Höhe
            float maxPercent = (optimalMax - minTemperature) / (maxTemperature - minTemperature);
            optimalMaxIndicator.anchoredPosition = new Vector2(optimalMaxIndicator.anchoredPosition.x, sliderHeight * maxPercent);
        }
    }
}
 