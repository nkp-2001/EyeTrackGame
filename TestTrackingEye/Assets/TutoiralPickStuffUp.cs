using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TutoiralPickStuffUp : MonoBehaviour
{
    [SerializeField]
    private bool[] fields = new bool[] { false, false,false,
                                         false,false,false };

    [SerializeField] GameObject[] Brewobject;
    private void OnEnable()
    {
        CodeEventHandler.BasicSelection += GetSelection;
    }

    public void GetSelection(int i)
    {
        if (!fields[i])
        {
            fields[i] = true;
            Brewobject[i].GetComponent<BasicMoveAway>().Active();
        }
       
        if (fields.All(x => x))
        {
            SceneManager.LoadScene(2);


        }
    }
    private void OnDisable()
    {
        CodeEventHandler.BasicSelection -= GetSelection;
    }
}
