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

        var leftEyePosition = EyeL.transform.position;
        var rightEyePosition = EyeR.transform.position;

        // Berechne den mittleren Punkt der Augen für die Blickrichtung
        Vector3 gazeDirection = (leftEyePosition + rightEyePosition) / 2;

        Debug.Log("Gaze direction: " + gazeDirection);

        // Nutze die Blickrichtung, um eine Interaktion basierend auf dem Eye-Tracking zu erzeugen
        Ray gazeRay = new Ray(gazeDirection, faceActor.RightEyeOrientation);
        RaycastHit hit;

        if (Physics.Raycast(gazeRay, out hit))
        {
            Debug.Log("User is looking at: " + hit.collider.name);
            // Hier kannst du interaktive Logik hinzufügen, z.B. ein UI-Element hervorheben oder aktivieren.
        }
    }
}
