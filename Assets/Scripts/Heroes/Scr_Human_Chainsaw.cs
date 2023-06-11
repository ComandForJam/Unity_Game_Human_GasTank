using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Human_Chainsaw : Scr_BaseHero
{
    public Transform _playerTr;
    protected Scr_Slice _slice;
    protected Vector2 targetPos;
    protected float distanceToTarget;
    protected override void Start()
    {
        base.Start();
        targetPos = transform.position;
        _slice = GetComponent<Scr_Slice>();
        speed = 10;
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        UpdateDamage();
        ((Scr_HealthHero)Health).UpdateMaxHealth(pointsFear);
        _slice.damage = ((Scr_HealthHero)Health).UpdateDamageSlice();
        switch (stateCharacter)
        {
            case StateCharacter.isIdle:
                UpdateIdle();
                UpdateTarget();
                break;
            case StateCharacter.isMove:
                UpdateMove();
                WaitAttack();
                UpdateTarget();
                break;
            case StateCharacter.isDamage:
                break;
            case StateCharacter.isSlice:
                UpdateSlice();
                break;
            case StateCharacter.isDash:
                UpdateDash();
                break;
            case StateCharacter.isLifeSave:
                UpdateLifeSave();
                UpdateTarget();
                break;
            case StateCharacter.isDeath:
                break;
        }

    }
    protected override void UpdateIdle()
    {
        base.UpdateIdle();
        canState = true;
        distanceToTarget = Vector2.Distance(targetPos, transform.position);
        if (distanceToTarget > 3)
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
        distanceToTarget = Vector2.Distance(targetPos, transform.position);
        if (distanceToTarget > (_slice.radiusAttack + _slice.rangeAttack) * 0.9f)
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
        if (canState)
        {
            if (Health.HealthLessPercent(0.2f))
            {
                stateCharacter = StateCharacter.isLifeSave;
                targetPos = _playerTr.position;
                canState = true;
                return;
            }
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 4, LayerMask.GetMask("Enemy"));
            if (colliders.Length != 0)
            {
                stateCharacter = StateCharacter.isMove;
                if (Health.HealthLessPercent(0.4f))
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
    }
    protected void WaitAttack() // Ожидание атаки, ждет момета нанести удар
    {
        if (canState && _slice.canSlice && distanceToTarget < _slice.radiusAttack + _slice.rangeAttack)
        {
            _slice.Slice(direction, LayerMask.GetMask("Enemy"), true, gameObject);
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
    protected override void UpdateLifeSave()
    {
        base.UpdateLifeSave(); 
        if (canState && !Health.HealthLessPercent(0.7f))
        {
            stateCharacter = StateCharacter.isIdle;
        }
        distanceToTarget = Vector2.Distance(targetPos, transform.position);
        if (_dash.canDash && distanceToTarget > (_dash.delayDash * _dash.speedDash) * 0.8f)
        {
            _dash.Dash((targetPos - (Vector2)transform.position).normalized);
            stateCharacter = StateCharacter.isDash;
            canState = false;
        } 
        else if (distanceToTarget > 2)
        {
            motion = speed * Time.fixedDeltaTime * (targetPos - (Vector2)transform.position).normalized;
            transform.Translate(motion);
        }
        else
        {
            motion = Vector2.zero;
        }
    }
}
