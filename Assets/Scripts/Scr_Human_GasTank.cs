using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Human_GasTank : Scr_BaseCharacter
{
    public int pointsFear = 1; // Очки страха
    float radiusPoison = 3.5f;
    float damagePoison = 1.5f;

    float cooldownPoison = 0.1f;
    bool canPoison = true;
    float timerPoison = 0;
    protected override void Start()
    {
        base.Start();
        speed = 10;

        maxHealth = 100;
        health = maxHealth;
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
        CooldownUpdate();
        Poisoning();
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
