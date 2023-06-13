using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Mob_5 : Scr_BotAi
{
    protected Scr_SliceArcher _slice;
    protected override void Start()
    {
        base.Start();
        _slice = GetComponent<Scr_SliceArcher>();
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void UpdateMove()
    {
        if (transformTarget != null)
        {
            Vector2 targetPos = TransformTarget.position;
            if (Vector2.Distance(targetPos, transform.position) > _slice.rangeAttack  * 0.9f)
            {
                FindPath();
                motion += (targetPos - (Vector2)transform.position).normalized;
                
                motion = speed * Time.fixedDeltaTime * motion.normalized;
            }
            else
            {
                motion = speed * Time.fixedDeltaTime * Vector2.up;
            }
            transform.Translate(motion);
        }
        base.UpdateMove();
    }
    protected override void WaitAttack() // Ожидание атаки, ждет момета нанести удар
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
                //direction = (transformTarget.position - transform.position).normalized;
                _slice.Slice(transformTarget.position, LayerMask.GetMask("Heroes"), gameObject);
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
    private void OnDestroy()
    {
        if (ownerTrigger != null)
        {
            ownerTrigger.RemoveMob(this);
        }
    }
}
