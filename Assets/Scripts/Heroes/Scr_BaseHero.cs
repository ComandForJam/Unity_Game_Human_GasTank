using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BaseHero : Scr_BaseCharacter
{
    //public Scr_UI ui;
    protected Scr_Dash _dash;

    protected override void Start()
    {
        base.Start();
        _dash = GetComponent<Scr_Dash>();
        _dash.animator = animator;
        Health = new Scr_HealthHero(baseHealth, pointsFear);
        speed = 10;
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        UpdateDamage();
        ((Scr_HealthHero)Health).UpdateMaxHealth(pointsFear);
        switch (stateCharacter)
        {
            case StateCharacter.isIdle:
                UpdateIdle();
                break;
            case StateCharacter.isMove:
                UpdateMove();
                break;
            case StateCharacter.isDamage:
                break;
            case StateCharacter.isSlice:
                UpdateSlice();
                break;
            case StateCharacter.isDash:
                UpdateDash();
                break;
            case StateCharacter.isDeath:
                break;
        }
    }
    protected virtual void UpdateDash()
    {
        if (!_dash.isDash)
        {
            canState = true;
            stateCharacter = StateCharacter.isMove;
            if (animator != null)
            {
                animator.SetBool("Jump", false);
            }
        }
    }
    protected virtual void UpdateLifeSave()
    {
        UpdateMove();
    }
    
    public void Healed(float heal, GameObject owner)
    {
        Health.Heal(heal);
        if (pointsFear > 1 + (int)heal / 2)
        {
            owner.GetComponent<Scr_BaseHero>().pointsFear += (int)heal / 2;
            pointsFear -= (int)heal / 2;
        }
    }
    protected override void Death(GameObject byGameObject)
    {
        Scr_BotAi bot = byGameObject.GetComponent<Scr_BotAi>();
        if (bot != null)
        {
            bot.ownerTrigger.TargetNull(this);
        }
        _scr_ui.GameOver();
        base.Death(byGameObject);
    }
    public void LockController()
    {
        stateCharacter = StateCharacter.isDeath;
    }
}


