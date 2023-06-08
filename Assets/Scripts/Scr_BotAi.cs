using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BotAi : Scr_BaseCharacter
{
    public Scr_TriggerMobs ownerTrigger;
    public Transform _transformTarget;
    void Start()
    {
        speed = 10;
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (_transformTarget != null)
        {
            Move(_transformTarget.position);
        }
    }

    void Move(Vector2 targetPos)
    {
        Vector2 motion = (targetPos - (Vector2)transform.position).normalized * speed * Time.fixedDeltaTime;
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
