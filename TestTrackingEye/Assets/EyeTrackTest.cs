using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.LiveCapture.ARKitFaceCapture;


public class EyeTrackTest : MonoBehaviour
{
    [SerializeField] FaceActor faceActor;
    [SerializeField] GameObject EyeL;
    [SerializeField] GameObject EyeR;


    // Start is called before the first frame update
    void Start()
    {
       var x = faceActor.RightEyeOrientation;
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

        // Mittlere Blickrichtung berechnen
        Vector3 averageGazeDirection = (leftGazeDirection + rightGazeDirection) / 2;

        //Debug.Log("Gaze direction: " + averageGazeDirection);

        // Raycast in die Blickrichtung vom rechten Auge
        Ray gazeRay = new Ray(rightEyePosition, -rightGazeDirection);
        RaycastHit hit;

        if (Physics.Raycast(gazeRay, out hit))
        {
            Debug.Log("User is looking at: " + hit.collider.name);

            // Setze das angeguckte GameObject inaktiv
            hit.collider.gameObject.SetActive(false);
        }

        // Debug-Linie in Blickrichtung
        Debug.DrawLine(rightEyePosition, -(rightEyePosition + rightGazeDirection * 100), Color.red);
    }
}
