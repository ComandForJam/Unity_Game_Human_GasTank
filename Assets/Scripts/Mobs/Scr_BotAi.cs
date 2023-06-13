using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BotAi : Scr_BaseCharacter
{
    public Scr_TriggerMobs ownerTrigger;
    public BoxCollider2D colliderOwner;
    protected Scr_Slice _slice;
    [SerializeField]
    protected Transform transformTarget;
    protected Vector2 targetIdle;
    
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
        } 
    }
    protected override void UpdateIdle()
    {
        
        canState = true;
        if (ownerTrigger != null && ownerTrigger.isAngry)
        {
            stateCharacter = StateCharacter.isMove;
            return;
        }
        if (targetIdle != null && Vector2.Distance(transform.position, targetIdle) > 1)
        {
            FindPath();
            motion += (targetIdle - (Vector2)transform.position).normalized;

            motion = speed / 2 * Time.fixedDeltaTime * motion.normalized;
            transform.Translate(motion);
        } else
        {
            targetIdle = new Vector2((Random.value * colliderOwner.size.x - 5), (Random.value * colliderOwner.size.y - 5));
            targetIdle -= targetIdle / 2;
            targetIdle += (Vector2)ownerTrigger.transform.position;
        }
        base.UpdateIdle();

    }
    protected override void UpdateMove()
    {
        if (canState && !ownerTrigger.isAngry)
        {
            stateCharacter = StateCharacter.isIdle;
            return;
        }
        direction = (TransformTarget.position - transform.position).normalized;
        motion = direction;
        base.UpdateMove();
    }

    protected void WaitAttack() // Ожидание атаки, ждет момета нанести удар
    {
        if (canState)
        {
            if (!ownerTrigger.isAngry)
            {
                stateCharacter = StateCharacter.isIdle;
                return;
            }
            if (_slice.canSlice)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _slice.radiusAttack + _slice.rangeAttack, LayerMask.GetMask("Heroes"));
                if (colliders.Length > 0)
                {
                    
                    _slice.Slice(direction, LayerMask.GetMask("Heroes"), false, gameObject);
                    stateCharacter = StateCharacter.isSlice;
                    canState = false;
                    if (audioSource != null)
                    {
                        PlayAudio(AudioCode.attack);
                    }
                    if (animator != null)
                    {
                        animator.SetBool("Attack_1", true);
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
                animator.SetBool("Attack_1", false);
            }
            canState = true;
            stateCharacter = StateCharacter.isMove;
        }
    }
    public void AnimatorEventSlice()
    {
        _slice.canState = true;
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
