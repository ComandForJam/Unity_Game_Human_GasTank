using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BotAi : Scr_BaseCharacter
{
    public Scr_TriggerMobs ownerTrigger;
    public Transform _transformTarget;
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
        UpdateDamage();
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
    protected override void UpdateIdle()
    {
        base.UpdateIdle();
        canState = true;
        if (_transformTarget != null && Vector2.Distance(_transformTarget.position, transform.position) > (_slice.radiusAttack + _slice.rangeAttack) * 0.9f)
        {
            stateCharacter = StateCharacter.isMove;
        }
    }
    protected override void UpdateMove()
    {
        base.UpdateMove();
    }

    protected void WaitAttack() // Ожидание атаки, ждет момета нанести удар
    {
        if (canState && _slice.canSlice && Vector2.Distance(_transformTarget.position, transform.position) < _slice.radiusAttack + _slice.rangeAttack)
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
                animator.SetBool("isSlice", true);
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
}
