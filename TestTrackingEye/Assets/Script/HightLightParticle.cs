using UnityEngine;

public class HightLightParticle : MonoBehaviour
{
    [SerializeField] ParticleIndicator particleSystemHighlight;

    bool hightlight;

    EyeTrackTest eyeTrackTest;

    SelectOnTime selectOnTime;

    public bool totalBlock;

    public bool TotalBlock { get => totalBlock; set => totalBlock = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {  
        eyeTrackTest = FindAnyObjectByType<EyeTrackTest>();

        particleSystemHighlight.SetSpeed(GetComponent<SelectOnTime>().TimeToSelect); // maybe not best Method
    }

    // Update is called once per frame
    void Update()
    {
        if (eyeTrackTest != null)
        {
            if (!totalBlock)
            {

                Ray gazeRay = eyeTrackTest.GetRayCast();
                RaycastHit hit;

                if (Physics.Raycast(gazeRay, out hit))
                {
                    CheckRaycast(hit);

                }
                else
                {
                    if (hightlight)
                    {
                        UnHightLighting();
                    }
                }
            }
           
        }
        else
        {
            eyeTrackTest = FindAnyObjectByType<EyeTrackTest>();
        }
    }
    public void CheckRaycast(RaycastHit hit)
    {
        if (hit.collider.transform == transform & !hightlight)
        {

            HightLighting();
        }
        if (!(hit.collider.transform == transform) & hightlight)
        {
            UnHightLighting();
        }
    }
    private void HightLighting()
    {
        particleSystemHighlight.StartEffect();
        hightlight = true;
    }
    private void UnHightLighting()
    {

        particleSystemHighlight.StopEffect();
        hightlight = false;
    }
}
