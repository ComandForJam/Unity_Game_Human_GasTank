using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BotAi : Scr_BaseCharacter
{
    public Scr_TriggerMobs ownerTrigger;
    public BoxCollider2D colliderOwner;
    
    [SerializeField]
    protected Transform transformTarget;
    protected Vector2 targetIdle;
    
    protected override void Start()
    {
        base.Start();
        targetIdle = transform.position;
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
        if (Mathf.Abs(targetIdle.x - transform.position.x) > 1)
        {
            FindPath();
            motion += (targetIdle - (Vector2)transform.position).normalized;

            motion = speed * Time.fixedDeltaTime * motion.normalized;
            transform.Translate(motion);
        } else if (colliderOwner != null)
        {
            targetIdle = new Vector2((Random.value * colliderOwner.size.x - 5), 0);
            targetIdle -= targetIdle / 2;
            targetIdle += (Vector2)ownerTrigger.transform.position;
        }
        
        base.UpdateIdle();

    }
    protected override void UpdateMove()
    {
        if (ownerTrigger != null)
        {
            if (canState && !ownerTrigger.isAngry)
            {
                stateCharacter = StateCharacter.isIdle;
                return;
            }
        }
        if (transformTarget != null)
        {
            direction = (TransformTarget.position - transform.position).normalized;
            motion = direction;
        }
        base.UpdateMove();
    }

    protected virtual void WaitAttack()
    {

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
