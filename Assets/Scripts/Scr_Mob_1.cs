using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Mob_1 : Scr_BotAi
{

    protected override void Start()
    {
        base.Start();
        speed = 5;
        rangeAttack = 0.4f;
        damage = 5;
        radiusAttack = 0.6f;
        cooldown = 1.5f;

        maxHealth = 50;
        health = maxHealth;
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Move()
    {
        Vector2 targetPos = _transformTarget.position;
        motion = speed * Time.fixedDeltaTime * (targetPos - (Vector2)transform.position).normalized;
        transform.Translate(motion);
    }

    protected override void WaitAttack() // Ожидание атаки, ждет момета нанести удар
    {
        if (Vector2.Distance(_transformTarget.position, transform.position) < rangeAttack + radiusAttack)
        {
            Attack();
        }
    }
    protected override void CooldownUpdate()
    {
        if (!canAttack)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= cooldown)
            {
                canAttack = true;
            }
            if (timer >= 0.5f) animator.SetBool("isSlice", false);
        }
    }
    protected override void Attack()
    {
        if (canAttack)
        {
            if (animator != null)
            {
                animator.SetBool("isSlice", true);
            }

            Scr_Attack.Action((Vector2)transform.position + direction * rangeAttack, radiusAttack, LayerMask.GetMask("Heroes"), damage, false);

            canAttack = false;
            timer = 0;
        }
    }

    private void OnDestroy()
    {
        if (ownerTrigger != null)
        {
            ownerTrigger.RemoveMob(this);
        }
    }
}
