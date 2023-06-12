using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Mob_1 : Scr_BotAi
{

    protected override void Start()
    {
        base.Start();
        speed = 5;
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
    protected override void UpdateSlice()
    {
        base.UpdateSlice();
    }

    private void OnDestroy()
    {
        if (ownerTrigger != null)
        {
            ownerTrigger.RemoveMob(this);
        }
    }
}
