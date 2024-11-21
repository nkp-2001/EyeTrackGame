using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Bilder;

    [SerializeField] 
    private string Scenename;

    public bool nextScene = true;

    private int introIndex = -1;

    void Start()
    {
        NextFrame();
    }
    
    void Update()
    {
        if (Input.anyKeyDown && introIndex < Bilder.Length)
        {
             NextFrame();
        }
    }
    
    public void NextFrame()
    {
        introIndex++;
        
        if (introIndex < Bilder.Length)
        {
            Bilder[introIndex].SetActive(true);
            
            Bilder[introIndex].GetComponent<FadeOnClick>().FadeIn();
            
            Bilder[introIndex].GetComponent<FadeOnClick>().AfterFadeIn();
        }
        
        
        if (introIndex >= Bilder.Length && nextScene == true)
        {
            SceneManager.LoadScene(Scenename);
            return;
        }
        if(introIndex >= Bilder.Length && nextScene == false)
        {
            Application.Quit();
            Debug.Log("Game Over");
        }
    }
}