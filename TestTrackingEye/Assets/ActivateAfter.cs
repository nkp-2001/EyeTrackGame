using System.Collections;
using UnityEngine;

public class ActivateAfter : MonoBehaviour
{

    [SerializeField] float time;
    void Start()
    {
        StartCoroutine(ShowAfter());
    }
    IEnumerator ShowAfter()
    {
        yield return new WaitForSeconds(time);
        SelectOnTime selectOnTime = GetComponent<SelectOnTime>();
        if (selectOnTime != null)
        {
            selectOnTime.TotalBlock = false;
        }

        
        HightLight hightLight = GetComponent<HightLight>();
        if (hightLight != null)
        {
            hightLight.TotalBlock = false;
        }

        HightLightParticle particleIndicator = GetComponent<HightLightParticle>();
        if (particleIndicator != null)
        {
            particleIndicator.TotalBlock = false;
        }

    }
}
