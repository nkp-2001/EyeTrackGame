using UnityEngine;

public class ParticleIndicator : MonoBehaviour
{
    Animator animator;
    ParticleSystem ParticleSystem;

    private void Start()
    {
        ParticleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
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
