using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BotAi : Scr_BaseCharacter
{
    public Scr_TriggerMobs ownerTrigger;
    public Transform _transformTarget;

    protected float cooldown = 1;
    protected float timer = 0;
    protected bool canAttack = true;

    protected float rangeAttack = 1;
    protected float damage = 5;
    protected float radiusAttack = 1;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        CooldownUpdate();

        if (_transformTarget != null && !isDeath)
        {
            Move();
            WaitAttack();
        }
    }

    protected virtual void Move()
    {
        Vector2 targetPos = _transformTarget.position;
        if (Vector2.Distance(targetPos, transform.position) > radiusAttack + rangeAttack)
        {
            motion = speed * Time.fixedDeltaTime * (targetPos - (Vector2)transform.position).normalized;
        }
        else
        {
            motion = Vector2.zero;
        }
        transform.Translate(motion);
    }

    protected virtual void WaitAttack() // Ожидание атаки, ждет момета нанести удар
    {
        if (Vector2.Distance(_transformTarget.position, transform.position) < 5)
        {
            Attack();
        }
    }
    protected virtual void CooldownUpdate()
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
    protected virtual void Attack()
    {
        if (canAttack)
        {
            if (audioSource != null)
            {
                PlayAudio(AudioCode.attack);
            }
            if (animator != null)
            {
                animator.SetBool("isSlice", true);
            }

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
    protected override void Death()
    {
        base.Death();
        
        Destroy(gameObject, 1);
    }
}
