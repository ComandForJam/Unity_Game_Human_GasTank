using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BaseHero : Scr_BaseCharacter
{
    public Scr_Dash _dash;
    
    public int pointsFear = 10; // Очки страха

    protected override void Start()
    {
        base.Start();
        _dash = GetComponent<Scr_Dash>();
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
            stateCharacter = StateCharacter.isIdle;
        }
    }
    protected virtual void UpdateLifeSave()
    {
        UpdateMove();
    }
    

}


