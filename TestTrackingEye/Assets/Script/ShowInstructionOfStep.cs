using UnityEngine;
using TMPro;

public class ShowInstructionOfStep : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] TextMeshProUGUI instructionText;
    void Start()
    {
        RecipeMangment recipeMangment = FindAnyObjectByType<RecipeMangment>();
        if(recipeMangment != null)
        {
            instructionText.text = recipeMangment.GetCurrentStepData().Instruction;
        }
       
    }
}
