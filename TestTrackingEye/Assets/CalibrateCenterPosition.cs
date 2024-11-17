using UnityEngine;

public class CalibrateCenterPosition : MonoBehaviour
{

    [SerializeField] Transform CameareLookPostion;
    
    [SerializeField] GameObject Rig;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            print("Try Stuzff");
            transform.position = new Vector3(transform.position.x, transform.position.y - Rig.transform.localPosition.y, transform.position.z);
        }
    }
}
