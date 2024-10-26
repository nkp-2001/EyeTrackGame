using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;


public class WaldoManager : MonoBehaviour
{
    [SerializeField] Mesh[] possibleMesh;
    Color[] Colors = { Color.green, Color.red, Color.blue , Color.yellow };
    public GameObject rightObjectToLookAt;
    MeshFilter[] allObject;
    [SerializeField] TextMeshProUGUI rightObjectToLookAtText; // TMP-Text
    // Start is called before the first frame update

    private void Start()
    {
        allObject = transform.GetComponentsInChildren<MeshFilter>();
        StartCoroutine(WaldoingInterval());
    }

    public void Waldoing()
    {
        List<Color> workingColors = new List<Color>(Colors);
        List<MeshFilter> workingObject = new List<MeshFilter>(allObject);

        //Big right one
        MeshFilter rightObject = workingObject[Random.Range(0, workingObject.Count())];
        rightObject.mesh = possibleMesh[Random.Range(0, possibleMesh.Length)];
        Color rightColor = Colors[Random.Range(0, Colors.Length)];
        rightObject.GetComponent<Renderer>().material.SetColor("_Color", Colors[Random.Range(0, Colors.Length)]);
        workingColors.Remove(rightColor);

        workingObject.Remove(rightObject);

        rightObjectToLookAt = rightObject.gameObject;

        rightObjectToLookAtText.text = rightObjectToLookAt.name;

        foreach (MeshFilter gO in workingObject)
        {
            gO.mesh = possibleMesh[Random.Range(0, possibleMesh.Length)];

            var cubeRenderer = gO.GetComponent<Renderer>();

            // Use SetColor to set the main color shader property
            cubeRenderer.material.SetColor("_Color", workingColors[Random.Range(0, workingColors.Count)]);
            // If your project uses URP, uncomment the following line and use it instead of the previous line
            // cubeRenderer.material.SetColor("_BaseColor", Color.red);
        }
    }

    IEnumerator WaldoingInterval()
    {
        while (true)
        {
            Waldoing();
            yield return new WaitForSeconds(15);       
        }
        
    }
}
