using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Boss_1 : Scr_BotAi
{
    protected Scr_Slice _slice;
    protected Scr_SliceAround _sliceAround;
    protected override void Start()
    {
        base.Start();
        _slice = GetComponent<Scr_Slice>();
        _sliceAround = GetComponent<Scr_SliceAround>();
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        UpdateSlice();
        base.FixedUpdate();
    }

    protected override void UpdateMove()
    {
        if (transformTarget != null)
        {
            Vector2 targetPos = TransformTarget.position;
            if (Vector2.Distance(targetPos, transform.position) > (_slice.radiusAttack + _slice.rangeAttack) * 0.9f)
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
    protected override void WaitAttack() // Ожидание атаки, ждет момета нанести удар
    {
        if (canState)
        {
            if (!ownerTrigger.isAngry)
            {
                stateCharacter = StateCharacter.isIdle;
                return;
            }
            if (_sliceAround.canSlice)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + Vector3.down * 0.4f, _slice.radiusAttack + _slice.rangeAttack, LayerMask.GetMask("Enemy"));
                if (colliders.Length > 1)
                {
                    _sliceAround.Slice(direction, LayerMask.GetMask("Heroes"), true, gameObject);
                    stateCharacter = StateCharacter.isSlice;
                    canState = false;
                    if (audioSource != null)
                    {
                        PlayAudio(AudioCode.attackAround);
                    }
                    if (animator != null)
                    {
                        animator.SetBool("Attack_2", true);
                    }
                    return;
                }
            }
            if (_slice.canSlice)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + Vector3.down * 0.4f, _slice.radiusAttack + _slice.rangeAttack, LayerMask.GetMask("Heroes"));
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
        _sliceAround.canState = true;
    }
    protected override void Death(GameObject byGameObject)
    {
        if (_scr_ui == null) _scr_ui = GameObject.Find("UI(Clone)").GetComponent<Scr_UI>();
        _scr_ui.GameWin();
        base.Death(byGameObject);
        
    }

    private void OnDestroy()
    {
        if (ownerTrigger != null)
        {
            ownerTrigger.RemoveMob(this);
        }
    }
}
