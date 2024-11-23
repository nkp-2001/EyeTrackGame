using UnityEngine;

public class CalibrateCenterPosition : MonoBehaviour
{

    [SerializeField] Transform CameareLookPostion;
    
    [SerializeField] GameObject Rig;

    [SerializeField] GameObject tra;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
           
            // Not clear if need anymore. Ignore this for now. Later chnages may come.

            Vector3 directionToParent = transform.position - Rig.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(directionToParent);
            // Apply the rotation to the child object
            Rig.transform.rotation = lookRotation;
            // Set Y and Z rotations to 0
            Rig.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, 0);

            print("Try Stuzff");
            transform.position = new Vector3(transform.position.x, transform.position.y - Rig.transform.localPosition.y, transform.position.z);



            //Rig.transform.LookAt(new Vector3(transform.position.x, transform.position.x, transform.position.x));
            //Rig.transform.rotation = new Quaternion(Rig.transform.rotation.x, 0, 0,0);
        }
    }
}
