using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


    public class IntroFade : MonoBehaviour
    {
        public GameObject Bild2;
        public GameObject Bild3;
        public GameObject Bild4;
        public GameObject Bild5; 
        public GameObject Bild6;
        
       
        
        //Startet die Coroutine
        void Start()
        {
            StartCoroutine(IntroCoroutine());
        }
        
        //wartet die Zahl in den Klammern als Sekunden ab
        //bevor immer ein weiteres bild aktivert wird.
        IEnumerator IntroCoroutine()
        {
            yield return new WaitForSeconds(5);
            Bild2.SetActive(true);
            yield return new WaitForSeconds(5);
            Bild3.SetActive(true);
            yield return new WaitForSeconds(5);
            Bild4.SetActive(true);
            yield return new WaitForSeconds(5);
            Bild5.SetActive(true);
            yield return new WaitForSeconds(5);
            Bild6.SetActive(true);
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene("Memory_Basti_Selfmade");



        }
    }
