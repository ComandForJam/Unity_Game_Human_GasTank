using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Dash : MonoBehaviour
{
    Animator animator;
    public ParticleSystem particleDash;
    new Collider2D collider;
    Collider2D colliderCharacter;
    public GameObject goColliderCharacter;
    

    Vector2 direction;
    float cooldownDash = 0.7f;
    float timerDash = 0;
    public readonly float delayDash = 0.12f;
    public readonly float speedDash = 50;

    public bool isDash = false;
    public bool canDash = true;

    public bool audioPlayed = false;

    
    void Start()
    {
        collider = GetComponent<Collider2D>();
        colliderCharacter = Instantiate(goColliderCharacter, transform).GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        CooldownUpdate();
        if (isDash)
        {
            transform.Translate(speedDash * Time.deltaTime * direction);
        }
    }
    public void Dash(Vector2 _direction) // �����
    {
        if (canDash)
        {
            if (animator != null)
            {
                animator.SetBool("Jump", true);
            }
            
            direction = _direction;
            isDash = true;
            canDash = false;
            audioPlayed = false;
            collider.enabled = false;
            colliderCharacter.enabled = true;
            timerDash = 0;
            particleDash.Play();
        }
    }
    void CooldownUpdate()
    {
        if (!canDash)
        {
            timerDash += Time.fixedDeltaTime;
            if (timerDash >= cooldownDash)
            {
                canDash = true;
            }
            if (timerDash >= delayDash)
            {
                collider.enabled = true;
                colliderCharacter.enabled = false;
                particleDash.Stop();
                isDash = false;
            }
        }
    }
    
}
