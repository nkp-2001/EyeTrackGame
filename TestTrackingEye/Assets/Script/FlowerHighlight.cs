using UnityEngine;
using System.Collections;

public class FlowerHighlight : HightLight
{
    [SerializeField] int times;
    [SerializeField] float betweenTimes;

    FlowerNumber flowerNumber;

    bool triggered;

    private void OnEnable()
    {
        CodeEventHandler.HightLightFlower += StartHighlight;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flowerNumber = GetComponent<FlowerNumber>();
        SetUpMaterialRefrence();
  
    }
    private void Update()
    {
        // override so Update from Base is not called
    }

    protected override void SetUpMaterialRefrence()
    {
        meshRenderers = transform.GetChild(0).gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer MS in meshRenderers)
        {
            defaultMaterialList.Add(MS.materials);

        }
    }


    public void StartHighlight(int i)
    {
        
        if (flowerNumber.Flower == (Flowers)i)
        {
            if (!triggered)
            {
                StartCoroutine(MarkBlink());
                triggered = true;
            }
          
        }
        
        
    }
    IEnumerator MarkBlink() 
    {
        print("fff");
        for (int i = 0; i < times; i++)
        {     
            HightLighting();
            yield return new WaitForSeconds(betweenTimes);
            UnHightLighting();
            yield return new WaitForSeconds(betweenTimes/2);
        }
       
    }
    private void OnDisable()
    {
        CodeEventHandler.HightLightFlower -= StartHighlight;
    }



}
