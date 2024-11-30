using UnityEngine;
using UnityEngine.UI;

public class OptimalRangeChecker : MonoBehaviour
{
    public Slider temperatureSlider;        // Der Slider (mit minValue = 0, maxValue = 100)
    public RectTransform optimalRangeImage; // Das RectTransform f¸r den Optimalbereich
    public float minValue = 30f;            // Minimum des Optimalbereichs (im Bereich 0-100)
    public float maxValue = 70f;            // Maximum des Optimalbereichs (im Bereich 0-100)

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

   

}
