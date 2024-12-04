using UnityEngine;

public class ParticleIndicator : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem ParticleSystem;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found on object: " + gameObject.name);
        }

        ParticleSystem = transform.GetChild(0)?.GetComponent<ParticleSystem>();
        if (ParticleSystem == null)
        {
            Debug.LogError("ParticleSystem not found as child of: " + gameObject.name);
        }
    }

    public void StartEffect()
    {
        animator.SetBool("Active", true);
        ParticleSystem.Play();
    }
    public void StopEffect()
    {
        animator.SetBool("Active", false);
        ParticleSystem.Stop();
    }
    public void SetSpeed(float seedFullSecounds)
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        } 
        animator.speed = 1/seedFullSecounds;
     
    }
}
