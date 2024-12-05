using UnityEngine;

public class SelectOnTime : MonoBehaviour
{
    [SerializeField] float timeToSelect;
    [SerializeField] float currentSelctedTime = 2;

    EyeTrackTest eyeTrackTest;

    [SerializeField] int myValue;
    [SerializeField] GameObject TimeIndiactor;
    [SerializeField] bool blockedMode = false; // If On A Filed can not be selcted if an Selection had happend and you keep looking at it
    bool lookedAT = false;
    bool lookedATAfterSelected = false;

    public bool TotalBlock = false;

    public float TimeToSelect { get => timeToSelect; set => timeToSelect = value; }

    private void Start()
    {
        eyeTrackTest = FindAnyObjectByType<EyeTrackTest>();
        currentSelctedTime = timeToSelect;
    }

    private void Update()
    {
        if (!TotalBlock)
        {
          
            if (eyeTrackTest != null)
            {
              
                Ray gazeRay = eyeTrackTest.GetRayCast();
                RaycastHit hit;

                if (Physics.Raycast(gazeRay, out hit))
                {
                    if (hit.collider.transform == transform)
                    {
                        lookedAT = true;
                        currentSelctedTime -= Time.deltaTime;
                        if (TimeIndiactor != null)
                        {
                            TimeIndiactor.transform.localScale = new Vector3(1 - (currentSelctedTime / timeToSelect), TimeIndiactor.transform.localScale.y, TimeIndiactor.transform.localScale.z);
                        }

                    }
                    else
                    {
                        lookedAT = false;
                        if (lookedATAfterSelected)
                        {
                            lookedATAfterSelected = false;
                        }
                        currentSelctedTime = timeToSelect;
                        if (TimeIndiactor != null)
                        {
                            TimeIndiactor.transform.localScale = new Vector3(0, TimeIndiactor.transform.localScale.y, TimeIndiactor.transform.localScale.z);
                        }

                    }

                }

                if (currentSelctedTime <= 0)
                {

                    if (!(blockedMode && lookedATAfterSelected))
                    {

                        CodeEventHandler.Trigger_BasicSelection(myValue);
                        Debug.Log($"Field {myValue} selected.");
                    }

                    lookedATAfterSelected = true;
                }
            }
            else
            {
                eyeTrackTest = FindAnyObjectByType<EyeTrackTest>();
            }
        }

       
       

    }
    public void SetValue(int i)
    {
        myValue = i;
    }
}
