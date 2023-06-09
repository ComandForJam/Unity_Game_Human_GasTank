using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Human_GasTank : Scr_BaseCharacter
{
    ParticleSystem _particleDash;

    public int pointsFear = 1; // Очки страха
    float radiusPoison = 3.5f;
    float damagePoison = 1.5f;

    float cooldownPoison = 0.1f;
    bool canPoison = true;
    float timerPoison = 0;

    float cooldownDash = 0.7f;
    bool canDash = true;
    float timerDash = 0;

    bool isDash = false;
    float delayDash = 0.12f;

    protected override void Start()
    {
        base.Start();
        _particleDash = GetComponentInChildren<ParticleSystem>();

        speed = 10;

        maxHealth = 100;
        health = maxHealth;
    }

    protected override void Update()
    {
        base.Update();
        Dash();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!isDash)
        {
            Move();
            Poisoning();
        }
        CooldownUpdate();
    }
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        motion = new Vector2(x, y) * Time.deltaTime * speed;
        transform.Translate(motion);
    }

    private void Poisoning()
    {
        if (canPoison)
        {
            Scr_Attack.ActionPoison(transform.position, radiusPoison, LayerMask.GetMask("Enemy"), damagePoison);
            canPoison = false;
            timerPoison = 0;
        }
    }
    protected virtual void CooldownUpdate()
    {
        if (!canPoison)
        {
            timerPoison += Time.fixedDeltaTime;
            if (timerPoison >= cooldownPoison)
            {
                canPoison = true;
            }
        }
        if(!canDash)
        {
            timerDash += Time.fixedDeltaTime;
            if (timerDash >= cooldownDash)
            {
                canDash = true;
            }
            if (timerDash >= delayDash)
            {
                collider.enabled = true;
                _particleDash.Stop();
                isDash = false;
            }
        }
    }

    protected void Dash() // РЫвок
    {
        if (isDash)
        {
            transform.Translate(direction * 200 * Time.deltaTime);
        } 
        else if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            isDash = true;
            _particleDash.Play();
            collider.enabled = false;
            canDash = false;
            timerDash = 0;
        }
    }
    
    /*
    public override void Damage(float damage)
    {
        base.Damage(damage);
        // Место для включения анимации получения урона
    }
    protected override void Death()
    {
        // Место для включения анимации смерти
        
    }*/
}
