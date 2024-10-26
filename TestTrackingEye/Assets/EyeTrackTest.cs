using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.LiveCapture.ARKitFaceCapture;


public class EyeTrackTest : MonoBehaviour
{
    [SerializeField] FaceActor faceActor;
    [SerializeField] GameObject EyeL;
    [SerializeField] GameObject EyeR;
    [SerializeField] float reactivationTime = 5f; // Zeit bis zur Wiederaktivierung
    [SerializeField] GameObject CantLookAway;
    [SerializeField] WaldoManager WaldoManager;

    // Start is called before the first frame update
    void Start()
    {
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
        Vector3 leftGazeDirection = Quaternion.Euler(faceActor.LeftEyeOrientation) * Vector3.forward;
        Vector3 rightGazeDirection = Quaternion.Euler(faceActor.RightEyeOrientation) * Vector3.forward;

        // Berechne die Mitte der beiden Augenpositionen als Startpunkt des Raycasts
        Vector3 gazeOrigin = (leftEyePosition + rightEyePosition) / 2;

        // Mittlere Blickrichtung berechnen
        Vector3 averageGazeDirection = (leftGazeDirection + rightGazeDirection) / 2;

        //Debug.Log("Gaze direction: " + averageGazeDirection);

        // Raycast in die Blickrichtung vom rechten Auge
        Ray gazeRay = new Ray(gazeOrigin, -averageGazeDirection);
        RaycastHit hit;

        if (Physics.Raycast(gazeRay, out hit))
        {
            //Debug.Log("User is looking at: " + hit.collider.name);
            if(WaldoManager.rightObjectToLookAt.name == hit.collider.gameObject.name)
            {
                Debug.Log("Riiiiiiiichtig!!!!!!!");

            }
            else if(hit.collider != null)
            {
                Debug.Log("Falsch du Penner!!!!!!!" + hit.collider.name);
            }

            // Setze das angeguckte GameObject inaktiv
            //hit.collider.gameObject.SetActive(false);
            StartCoroutine(ReactivateObject(hit.collider.gameObject));
        }

        // Debug-Linie in Blickrichtung
        Debug.DrawLine(gazeOrigin, -averageGazeDirection * 100, Color.red);

        CantLookAway.transform.position = gazeOrigin - averageGazeDirection * 5;
    }

    // Coroutine zur Wiederaktivierung des Objekts nach einer bestimmten Zeit
    private IEnumerator ReactivateObject(GameObject obj)
    {
        yield return new WaitForSeconds(reactivationTime);
        obj.SetActive(true);
    }
}
