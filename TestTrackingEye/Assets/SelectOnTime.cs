using UnityEngine;

public class SelectOnTime : MonoBehaviour
{
    [SerializeField] float timeToSelect;
    [SerializeField] float currentSelctedTime = 2;

    EyeTrackTest eyeTrackTest;

    [SerializeField] int myValue;
    [SerializeField] GameObject TimeIndiactor;

    private void Start()
    {
        eyeTrackTest = FindAnyObjectByType<EyeTrackTest>();
        currentSelctedTime = timeToSelect;
    }

    private void Update()
    {
        if (eyeTrackTest != null)
        {
            Ray gazeRay = eyeTrackTest.GetRayCast();
            RaycastHit hit;

            if (Physics.Raycast(gazeRay, out hit))
            {
                if (hit.collider.transform == transform)
                {
                    currentSelctedTime -= Time.deltaTime;
                    TimeIndiactor.transform.localScale = new Vector3(1-(currentSelctedTime/ timeToSelect), TimeIndiactor.transform.localScale.y, TimeIndiactor.transform.localScale.z);
                }
                else
                {
                    currentSelctedTime = timeToSelect;
                    TimeIndiactor.transform.localScale = new Vector3(0, TimeIndiactor.transform.localScale.y, TimeIndiactor.transform.localScale.z);
                }

            }
           
            if(currentSelctedTime <= 0)
            {
                CodeEventHandler.Trigger_BasicSelection(myValue);
            }
        }

    }
}
