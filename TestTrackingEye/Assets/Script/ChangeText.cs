using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    public Text textComponent; // Drag your text component here in the Inspector
    public string newText = "Neuer Text"; // Define the new text here

    void Start()
    {
        if (textComponent != null)
        {
            textComponent.text = newText; // Change the text to the new string
        }
        else
        {
            Debug.LogError("Text Component not assigned.");
        }
    }
}
