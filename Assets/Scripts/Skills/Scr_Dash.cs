using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Dash : MonoBehaviour
{
    ParticleSystem particleDash;
    new Collider2D collider;

    Vector2 direction;
    float cooldownDash = 0.7f;
    float timerDash = 0;
    float delayDash = 0.12f;
    public float speedDash = 50;

    public bool isDash = false;
    public bool canDash = true;
    
    void Start()
    {
        particleDash = GetComponentInChildren<ParticleSystem>();
        collider = GetComponent<Collider2D>();
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
    public void Dash(Vector2 _direction) // РЫвок
    {
        if (canDash)
        {
            direction = _direction;
            isDash = true;
            canDash = false;
            collider.enabled = false;
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
                particleDash.Stop();
                isDash = false;
            }
        }
    }
    
}
