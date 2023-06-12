using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Human_Chainsaw : Scr_BaseHero
{
    public Transform _playerTr;
    protected Scr_Slice _slice;
    protected Scr_SliceAround _sliceAround;
    protected Vector2 targetPos;
    protected float distanceToTarget;

    bool isTargetImportant = false;
    protected override void Start()
    {
        base.Start();
        targetPos = transform.position;
        _slice = GetComponent<Scr_Slice>();
        _sliceAround = GetComponent<Scr_SliceAround>();
        speed = 10;
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        //Debug.Log(stateCharacter + " " + canState + " " + isTargetImportant ); ;
        UpdateDamage();
        ((Scr_HealthHero)Health).UpdateMaxHealth(pointsFear);
        _slice.damage = ((Scr_HealthHero)Health).UpdateDamageSlice();
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
        canState = true;
        distanceToTarget = Vector2.Distance(targetPos, transform.position);
        if (distanceToTarget > 3)
        {
            motion = speed / 2 * Time.fixedDeltaTime * (targetPos - (Vector2)transform.position).normalized;
            transform.Translate(motion);
        } else
        {
            motion = Vector2.zero;
        }
        if (isTargetImportant) 
        {
            stateCharacter = StateCharacter.isMove;
        }
        base.UpdateIdle();
    }
    protected override void UpdateMove()
    {
        if (GoOut()) return;
        distanceToTarget = Vector2.Distance(targetPos, transform.position);
        if (canState && _dash.canDash && distanceToTarget > (_dash.delayDash * _dash.speedDash) * 0.8f)
        {
            _dash.Dash((targetPos - (Vector2)transform.position).normalized);
            stateCharacter = StateCharacter.isDash;
            canState = false;
            return;
        }
        if (isTargetImportant)
        {
            if (distanceToTarget >= 0.5f)
            {
                FindPath();
                motion += (targetPos - (Vector2)transform.position).normalized;

                motion = speed * Time.fixedDeltaTime * motion.normalized;
            }
            else
            {
                isTargetImportant = false;
            }
        } 
        else if (distanceToTarget > (_slice.radiusAttack + _slice.rangeAttack) * 0.9f)
        {
            FindPath();
            motion += (targetPos - (Vector2)transform.position).normalized;

            motion = speed * Time.fixedDeltaTime * motion.normalized;
        }
        else
        {
            targetPos = (Vector2)transform.position +  new Vector2((Random.value * 2), (Random.value * 2));
        }
        transform.Translate(motion);
        base.UpdateMove();
    }

    void UpdateTarget()
    {
        if (canState && !isTargetImportant)
        {
            if (Vector2.Distance(transform.position, _playerTr.position) > Camera.main.orthographicSize)
            {
                targetPos = _playerTr.position;
                return;
            }
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

                targetPos = Scr_Attack.NearTarget(transform.position, colliders).transform.position;
            
            
            
            } else
            {
                targetPos = _playerTr.position;
                stateCharacter = StateCharacter.isIdle;
            }
        }
    }
    protected void WaitAttack() // Ожидание атаки, ждет момета нанести удар
    {
        if (canState)
        {
            if (_sliceAround.canSlice)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _slice.radiusAttack + _slice.rangeAttack, LayerMask.GetMask("Enemy"));
                if (colliders.Length > 2)
                {
                    _sliceAround.Slice(direction, LayerMask.GetMask("Enemy"), true, gameObject);
                    stateCharacter = StateCharacter.isSlice;
                    canState = false;
                    if (audioSource != null)
                    {
                        PlayAudio(AudioCode.attackAround);
                    }
                    if (animator != null)
                    {
                        animator.SetBool("isSliceAround", true);
                    }
                    return;
                }
            }
            if (_slice.canSlice)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _slice.radiusAttack + _slice.rangeAttack, LayerMask.GetMask("Enemy"));
                
                if (colliders.Length > 0)
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
        }
    }

    protected override void UpdateSlice()
    {
        if (_slice.canState)
        {
            if (animator != null)
            {
                animator.SetBool("isSlice", false);
            }
            canState = true;
            stateCharacter = StateCharacter.isMove;
        }
        if (_sliceAround.canState)
        {
            if (animator != null)
            {
                animator.SetBool("isSliceAround", false);
            }
            canState = true;
            stateCharacter = StateCharacter.isMove;
        }
    }
    public void AnimatorEventSlice()
    {
        _slice.canState = true;
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
    bool GoOut()
    {
        if (canState && _dash.canDash && (!_sliceAround.canSlice || Health.HealthLessPercent(0.4f)))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2, LayerMask.GetMask("Enemy"));
            if (colliders.Length > 3)
            {
                _dash.Dash(-(colliders[0].transform.position - transform.position).normalized);
                stateCharacter = StateCharacter.isDash;
                canState = false;
                return true;
            }
        }
        return false;
    }

    public void SetTargetPos(Vector2 pos)
    {
        targetPos = pos;
        isTargetImportant = true;
    }
}
