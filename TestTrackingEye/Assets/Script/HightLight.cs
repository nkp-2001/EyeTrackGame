using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HightLight : ChangeMaterial
{
    EyeTrackTest eyeTrackTest;

    public bool totalBlock;

    public bool TotalBlock { get => totalBlock; set => totalBlock = value; }

    private void Start()
    {
        SetUpMaterialRefrence();
       
    }

    protected override void SetUpMaterialRefrence()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer MS in meshRenderers)
        {
            defaultMaterialList.Add(MS.materials);

        }
        eyeTrackTest = FindAnyObjectByType<EyeTrackTest>();
    }

    private void Update()
    {
        if (eyeTrackTest != null)
        {
            if (!totalBlock)
            {
                Ray gazeRay = eyeTrackTest.GetRayCast();
                RaycastHit hit;

                if (Physics.Raycast(gazeRay, out hit))
                {
                    CheckRaycast(hit);

                }
                else
                {
                    if (hightlight)
                    {
                        UnHightLighting();
                    }
                }
            }
            
        }
        else
        {
            eyeTrackTest = FindAnyObjectByType<EyeTrackTest>();
        }
    }

    public void CheckRaycast(RaycastHit hit)
    {
        if (hit.collider.transform == transform & !hightlight)
        {
         
            HightLighting();
        }
        if (!(hit.collider.transform == transform) & hightlight)
        {
            UnHightLighting();
        }
    }
}
