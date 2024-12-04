using System.Collections;
using UnityEngine;

public class ActivateAfter : MonoBehaviour
{

    [SerializeField] float time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ShowAfter());
    }
    IEnumerator ShowAfter()
    {
        yield return new WaitForSeconds(time);
        GetComponent<SelectOnTime>().TotalBlock = false;
    }
}
