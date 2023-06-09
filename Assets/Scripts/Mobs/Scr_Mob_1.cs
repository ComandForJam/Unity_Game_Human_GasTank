using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Mob_1 : Scr_BotAi
{
    protected Scr_Slice _slice;
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
        base.FixedUpdate();
    }

    protected override void UpdateMove()
    {
        if (transformTarget != null)
        {
            Vector2 targetPos = TransformTarget.position;
            if (Vector2.Distance(targetPos, transform.position) > (_slice.radiusAttack + _slice.rangeAttack)  * 0.9f)
            {
                FindPath();
                motion += (targetPos - (Vector2)transform.position).normalized;
                
                motion = speed * Time.fixedDeltaTime * motion.normalized;
            }
            else
            {
                motion = Vector2.zero;
            }
            transform.Translate(motion);
        }
        base.UpdateMove();
    }
    protected override void WaitAttack() // �������� �����, ���� ������ ������� ����
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

    private void OnDestroy()
    {
        if (ownerTrigger != null)
        {
            ownerTrigger.RemoveMob(this);
        }
    }
}
