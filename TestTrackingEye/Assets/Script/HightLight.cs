using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HightLight : MonoBehaviour
{
    [SerializeField] public Material highlightMaterial = null;

    private List<Material[]> defaultMaterialList = new List<Material[]>();
    private MeshRenderer[] meshRenderers;
    [SerializeField] bool onlyparent = false;

    bool hightlight;

    EyeTrackTest eyeTrackTest;

    private void Start()
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
        else
        {
            eyeTrackTest = FindAnyObjectByType<EyeTrackTest>();
        }
    }

    public void CheckRaycast(RaycastHit hit)
    {
        if (hit.collider.transform == transform & !hightlight)
        {
            print("--- HightLighting");
            HightLighting();
        }
        if (!(hit.collider.transform == transform) & hightlight)
        {
            UnHightLighting();
        }
    }

    private void HightLighting()
    {
        if (highlightMaterial != null)
        {
            if (onlyparent)
            {
                gameObject.GetComponent<Renderer>().material = highlightMaterial;
            }
            else
            {
                foreach (MeshRenderer MS in meshRenderers)
                {
                    MS.material = highlightMaterial;
                }
                
            }
            hightlight = true;
        }
        else
        {
            UnHightLighting();
        }
    }
    private void UnHightLighting()
    {
        if (onlyparent)
        {
            gameObject.GetComponent<Renderer>().material = defaultMaterialList[0][0];
        }
        else
        {
            int i = 0;
            foreach (MeshRenderer MS in meshRenderers)
            {
                MS.materials = defaultMaterialList[i];
                i++;
            }
        }
        
        hightlight = false;
    }
}
