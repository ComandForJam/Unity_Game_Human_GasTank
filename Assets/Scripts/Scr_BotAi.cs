using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BotAi : Scr_BaseCharacter
{
    public Scr_TriggerMobs ownerTrigger;
    public Transform _transformTarget;
    protected override void Start()
    {
        base.Start();
        speed = 10;
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_transformTarget != null)
        {
            Move(_transformTarget.position);
        }
    }

    void Move(Vector2 targetPos)
    {
        motion = speed * Time.fixedDeltaTime * (targetPos - (Vector2)transform.position).normalized;
        transform.Translate(motion);
    }
    private void OnDestroy()
    {
        if (ownerTrigger != null)
        {
            ownerTrigger.RemoveMob(this);
        }
    }
}
