using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] public Material highlightMaterial = null;
    protected MeshRenderer[] meshRenderers;
    protected List<Material[]> defaultMaterialList = new List<Material[]>();
    public bool onlyparent = false;
    public GameObject Ignore;
    protected bool hightlight;

    private void Start()
    {
        SetUpMaterialRefrence();
    }

    protected virtual void SetUpMaterialRefrence()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer MS in meshRenderers)
        {
            defaultMaterialList.Add(MS.materials);

        }
    }
    public virtual void HightLighting()
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
                    if (MS.gameObject != Ignore)
                    {                     
                        MS.material = highlightMaterial;
                    }

                }

            }
            hightlight = true;
        }
        else
        {
            UnHightLighting();
        }
    }
    public virtual void UnHightLighting()
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
