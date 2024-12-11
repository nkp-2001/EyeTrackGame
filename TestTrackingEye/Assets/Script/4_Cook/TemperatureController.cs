using UnityEngine;
using UnityEngine.UI;

public class TemperatureController : MonoBehaviour
{
    [Header("UI Components")]
    public Slider temperatureSlider; // Der UI-Slider, der die aktuelle Temperatur anzeigt
    public Image sliderFill; // Das Bild, das die Füllfarbe des Sliders anzeigt
    public RectTransform optimalMinIndicator; // Optional: Indikator für den unteren Bereich
    public RectTransform optimalMaxIndicator; // Optional: Indikator für den oberen Bereich

    [Header("Temperature Settings")]
    public float minTemperature = 0f; // Minimale Temperatur
    public float maxTemperature = 100f; // Maximale Temperatur
    public float optimalMin = 40f; // Untere Grenze des optimalen Bereichs
    public float optimalMax = 60f; // Obere Grenze des optimalen Bereichs
    public float temperatureChangeSpeed = 20f; // Geschwindigkeit, mit der sich die Temperatur ändert
    public float temperatureAutoChangeSpeed = 3f; // Geschwindigkeit, mit der sich die Temperatur automatisch ändert

    public float currentTemperature; // Aktuelle Temperatur
    public bool isIncreasing = false; // Gibt an, ob die Temperatur gerade steigt
    public bool isDecreasing = false; // Gibt an, ob die Temperatur gerade sinkt

    void Start()
    {
        // Initialisiere die Temperatur auf die Mitte des Bereichs und setze die Slider-Werte
        currentTemperature = 50f;
        temperatureSlider.minValue = minTemperature;
        temperatureSlider.maxValue = maxTemperature;

        UpdateSlider(); // Aktualisiere die UI-Anzeige
    }

    void Update()
    {
        // Automatische Abkühlung
        currentTemperature -= temperatureAutoChangeSpeed * Time.deltaTime;

        // Temperatur basierend auf den Eingaben erhöhen oder verringern
        if (isIncreasing)
        {
            Debug.Log("Erhöhe Temperatur Bool");
            temperatureAutoChangeSpeed = 0f;
            currentTemperature += temperatureChangeSpeed * Time.deltaTime;
        }
        else if (isDecreasing)
        {
            Debug.Log("Verringere Temperatur Bool");
            temperatureAutoChangeSpeed = 0f;
            currentTemperature -= temperatureChangeSpeed * Time.deltaTime;
        }
        else
        {
            temperatureAutoChangeSpeed = 3f;
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

   
}
