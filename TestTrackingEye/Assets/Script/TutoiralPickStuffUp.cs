using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TutoiralPickStuffUp : MonoBehaviour
{
    [SerializeField]
    private bool[] fields = new bool[] { false, false,false,
                                         false,false,false };

    [SerializeField] GameObject[] Brewobject;

    [SerializeField] AudioClip succesSound;

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
            AudioSource.PlayClipAtPoint(succesSound, transform.position, 0.5f);
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
