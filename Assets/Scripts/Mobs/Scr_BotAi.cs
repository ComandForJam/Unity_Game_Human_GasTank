using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BotAi : Scr_BaseCharacter
{
    public Scr_TriggerMobs ownerTrigger;
    protected Scr_Slice _slice;
    protected Transform transformTarget;
    protected override void Start()
    {
        base.Start();
        _slice = GetComponent<Scr_Slice>();
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        UpdateDamage();
        //UpdateState();
        switch (stateCharacter)
        {
            case StateCharacter.isIdle:
                UpdateIdle();
                break;
            case StateCharacter.isMove:
                UpdateMove();
                WaitAttack();
                break;
            case StateCharacter.isDamage:
                break;
            case StateCharacter.isSlice:
                UpdateSlice();
                break;
            case StateCharacter.isDeath:
                break;
        }
    }
    public Transform TransformTarget 
    { 
        get { return transformTarget; } 
        set 
        {
            transformTarget = value;
            if (value == null)
            {
                stateCharacter = StateCharacter.isIdle;
            }
        } 
    }
    protected override void UpdateIdle()
    {
        base.UpdateIdle();
        canState = true;
        if (TransformTarget != null)
        {
            stateCharacter = StateCharacter.isMove;
            return;
        }

    }
    protected override void UpdateMove()
    {
        base.UpdateMove();
    }

    protected void WaitAttack() // Ожидание атаки, ждет момета нанести удар
    {
        
        if (canState) {
            if (_slice.canSlice) { 
                
                if (Vector2.Distance(TransformTarget.position, transform.position) < _slice.radiusAttack + _slice.rangeAttack)
                {
                    direction = (TransformTarget.position - transform.position).normalized;
                    Flip();
                    UpdateAnimator();

                    _slice.Slice(direction, LayerMask.GetMask("Heroes"), false, gameObject);
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
        if (!_slice.isSlice)
        {
            animator.SetBool("isSlice", false);
            canState = true;
            stateCharacter = StateCharacter.isMove;
        }
    }
    public void AnimatorEventSlice()
    {
        _slice.isSlice = false;
    }
    protected override void Death(GameObject byGameObject)
    {
        if (stateCharacter != StateCharacter.isDeath)
        {
            Destroy(gameObject, 1);
        }
        base.Death(byGameObject);
    }
    private void OnDestroy()
    {
        if (ownerTrigger != null)
        {
            ownerTrigger.RemoveMob(this);
        }
    }
    protected void UpdateState()
    {
        if (canState && TransformTarget == null)
        {
            stateCharacter = StateCharacter.isIdle;
        }
    }
}
