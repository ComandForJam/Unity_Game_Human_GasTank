using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Human_Chainsaw : Scr_BaseCharacter
{
    public Transform _playerTr;

    public int pointsFear = 1; // Очки страха

    protected Vector2 targetPos;

    protected float cooldown = 0.8f;
    protected float timer = 0;
    protected bool canAttack = true;

    protected float rangeAttack = 0.3f;
    protected float damage = 5;
    protected float radiusAttack = 1.5f;
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
        CooldownUpdate();
        UpdateTarget();

        if (targetPos != null && !isDeath)
        {
            Move();
            WaitAttack();
        }
    }
    void Move()
    {
        if (Vector2.Distance(targetPos,transform.position) > radiusAttack + rangeAttack)
        {
            motion = speed * Time.fixedDeltaTime * (targetPos - (Vector2)transform.position).normalized;
        } else
        {
            motion = Vector2.zero;
        }
        transform.Translate(motion);
    }

    void UpdateTarget()
    {
        if (health <= 0.2 * maxHealth)
        {
            targetPos = _playerTr.position;
            return;
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 4, LayerMask.GetMask("Enemy"));
        if (colliders.Length != 0)
        {
            if (health <= 0.4 * maxHealth)
            {
                targetPos = Scr_Attack.NearTarget(_playerTr.position, colliders).transform.position;
                return;
            }
            if (Random.value < 0.5f)
            {
                targetPos = Scr_Attack.NearTarget(transform.position, colliders).transform.position;
            } else
            {
                targetPos = Scr_Attack.FarTarget(transform.position, colliders).transform.position;
            }
        } else
        {
            targetPos = _playerTr.position;
        }
    }
    protected virtual void WaitAttack() // Ожидание атаки, ждет момета нанести удар
    {
        if (Vector2.Distance(targetPos, transform.position) < 5)
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
            if (timer >= 0.5f && animator != null)
            {
                animator.SetBool("isSlice", false);
            }
        }
    }
    protected void Attack()
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
            Scr_Attack.Action((Vector2)transform.position + direction * rangeAttack, radiusAttack, LayerMask.GetMask("Enemy"), damage, true);

            canAttack = false;
            timer = 0;
        }
    }
}
