using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.LiveCapture.ARKitFaceCapture;


public class EyeTrackTest : MonoBehaviour
{
    [SerializeField] FaceActor faceActor;
    [SerializeField] GameObject EyeL;
    [SerializeField] GameObject EyeR;
    [SerializeField] GameObject Head;
    [SerializeField] float reactivationTime = 5f; // Zeit bis zur Wiederaktivierung
    [SerializeField] GameObject CantLookAway;
    [SerializeField] WaldoManager WaldoManager;

    [SerializeField] bool waldoMode = true;

    Ray gazeRay;

    // Start is called before the first frame update
    void Start()
    {
        gazeRay = new Ray(transform.position, Vector3.forward);
        WaldoManager = FindObjectOfType<WaldoManager>();
    }

    // Update is called once per frame
    void Update()
    {


        if (faceActor == null) return;

        // Hole die Positionen der Augen
        var leftEyePosition = EyeL.transform.position;
        var rightEyePosition = EyeR.transform.position;


        // Berechne die Orientierungen der Augen in Blickrichtungen

        var headDirection = (Head.transform.rotation);

        Vector3 leftGazeDirection = (EyeL.transform.rotation) * Vector3.forward;
        Vector3 rightGazeDirection = (EyeR.transform.rotation) * Vector3.forward;

        leftGazeDirection = headDirection * leftGazeDirection;
        rightGazeDirection = headDirection * rightGazeDirection;

        // Berechne die Mitte der beiden Augenpositionen als Startpunkt des Raycasts
        Vector3 gazeOrigin = (leftEyePosition + rightEyePosition) / 2;


        // Mittlere Blickrichtung berechnen
        Vector3 averageGazeDirection = (leftGazeDirection + rightGazeDirection) / 2;
        averageGazeDirection = new Vector3(averageGazeDirection.x, averageGazeDirection.y, averageGazeDirection.z);

        //Debug.Log("Gaze direction: " + averageGazeDirection);

        // Raycast in die Blickrichtung vom rechten Auge
        gazeRay = new Ray(gazeOrigin, -averageGazeDirection);
        RaycastHit hit;

        if (Physics.Raycast(gazeRay, out hit))
        {
            print("--öef3w");
            if (waldoMode)
            {
                //Debug.Log("User is looking at: " + hit.collider.name);
                if (WaldoManager.rightObjectToLookAt.name == hit.collider.gameObject.name)
                {
                    Debug.Log("Riiiiiiiichtig!!!!!!!");

                }
                else if (hit.collider != null)
                {
                    Debug.Log("Falsch du Penner!!!!!!!" + hit.collider.name);
                }

                // Setze das angeguckte GameObject inaktiv
                //hit.collider.gameObject.SetActive(false);
                StartCoroutine(ReactivateObject(hit.collider.gameObject));
            }
            else
            {
                print("--" + hit.collider.gameObject.name);
            }

        }

        // Debug-Linie in Blickrichtung
        Debug.DrawLine(gazeOrigin, -averageGazeDirection * 100, Color.red);

        Vector3 vector3 = (gazeOrigin - averageGazeDirection * 5);
        CantLookAway.transform.position = new Vector3(vector3.x, vector3.y, vector3.z);
    }

    // Coroutine zur Wiederaktivierung des Objekts nach einer bestimmten Zeit
    private IEnumerator ReactivateObject(GameObject obj)
    {
        yield return new WaitForSeconds(reactivationTime);
        obj.SetActive(true);
    }
    public Ray GetRayCast()
    {
        return gazeRay;
    }
}


