using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Human_Chainsaw : Scr_BaseHero
{
    public Transform _playerTr;
    protected Scr_Slice _slice;
    protected Vector2 targetPos;
    protected override void Start()
    {
        base.Start();
        targetPos = transform.position;
        _slice = GetComponent<Scr_Slice>();
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
        UpdateDamage();
        switch (stateCharacter)
        {
            case StateCharacter.isIdle:
                UpdateTarget();
                UpdateIdle();
                break;
            case StateCharacter.isMove:
                UpdateTarget();
                UpdateMove();
                WaitAttack();
                break;
            case StateCharacter.isDamage:
                break;
            case StateCharacter.isSlice:
                UpdateSlice();
                break;
            case StateCharacter.isDash:
                UpdateDash();
                break;
            case StateCharacter.isDeath:
                break;
        }

    }
    protected override void UpdateIdle()
    {
        base.UpdateIdle();
        canState = true;
        if (Vector2.Distance(targetPos, transform.position) > 3)
        {
            motion = speed * Time.fixedDeltaTime * (targetPos - (Vector2)transform.position).normalized;
            transform.Translate(motion);
        } else
        {
            motion = Vector2.zero;
        }
    }
    protected override void UpdateMove()
    {
        base.UpdateMove();
        if (Vector2.Distance(targetPos,transform.position) > (_slice.radiusAttack + _slice.rangeAttack) * 0.9f)
        {
            motion = speed * Time.fixedDeltaTime * (targetPos - (Vector2)transform.position).normalized;
            transform.Translate(motion);
        } else
        {
            motion = Vector2.zero;
        }
    }

    void UpdateTarget()
    {
        if (health <= 0.2 * maxHealth)
        {
            stateCharacter = StateCharacter.isMove;
            targetPos = _playerTr.position;
            return;
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 4, LayerMask.GetMask("Enemy"));
        if (colliders.Length != 0)
        {
            stateCharacter = StateCharacter.isMove;
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
            stateCharacter = StateCharacter.isIdle;
        }
    }
    protected void WaitAttack() // Ожидание атаки, ждет момета нанести удар
    {
        if (canState && _slice.canSlice && Vector2.Distance(targetPos, transform.position) < _slice.radiusAttack + _slice.rangeAttack)
        {
            _slice.Slice(direction, LayerMask.GetMask("Enemy"), true);
            stateCharacter = StateCharacter.isSlice;
            canState = false;
            if (audioSource != null)
            {
                PlayAudio(AudioCode.attack);
            }
            if (animator != null)
            {
                animator.SetBool("isSlice", true);
            }
        }
    }

    protected override void UpdateSlice()
    {
        if (!_slice.isSlice)
        {
            if (animator != null)
            {
                animator.SetBool("isSlice", false);
            }
            canState = true;
            stateCharacter = StateCharacter.isMove;
        }
    }
    public void AnimatorEventSlice()
    {
        _slice.isSlice = false;
    }
}
