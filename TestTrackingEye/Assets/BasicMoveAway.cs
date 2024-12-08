using UnityEngine;

public class BasicMoveAway : MonoBehaviour
{

    bool active = false;
    [SerializeField] float destroyAfter = 5;
    [SerializeField] float moveRate = 0.1f;
    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            destroyAfter -= Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y,transform.position.z - (moveRate*Time.deltaTime));
            if(destroyAfter < 0)
            {
                Destroy(gameObject);
            }

        }
    }
    public void Active()
    {
        active = true;
    }
}
