using UnityEngine;
using TMPro;

public class ShowRecipe : MonoBehaviour
{
   [SerializeField] GameObject TimeIndiactor;
   [SerializeField] TextMeshProUGUI TitelText;
   [SerializeField] TextMeshProUGUI InstructionText;
   [SerializeField] float Maxtime;
   float time;

    private void Start()
    {
      

        InstructionText.text = "";
        time = Maxtime;

        RecipeMangment recipeMangment = FindFirstObjectByType<RecipeMangment>();
        var instructionlist = recipeMangment.GetAllStepsInstrcutions();
        for (int i = 0; i < instructionlist.Count; i++)
        {
            InstructionText.text += ("#" + (i+1) + ". " + instructionlist[i]) +"\n";
        }
        InstructionText.text = InstructionText.text.Replace("_", " ");
        TitelText.text = recipeMangment.GetRecipeTitel();

    }

    private void Update()
    {
        time -= Time.deltaTime;
        TimeIndiactor.transform.localScale = new Vector3(time/Maxtime , TimeIndiactor.transform.localScale.y, TimeIndiactor.transform.localScale.z);


        if (time < 0)
        {
            CodeEventHandler.Trigger_StartBrewing();
        }

    }
}
