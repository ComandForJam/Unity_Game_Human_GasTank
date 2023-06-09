using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Human_GasTank : Scr_BaseHero
{
    Scr_Poison _poison;
    protected override void Start()
    {
        base.Start();
        _poison = GetComponent<Scr_Poison>();
        speed = 10;

        maxHealth = 100;
        health = maxHealth;
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _dash.Dash(direction);
            stateCharacter = StateCharacter.isDash;
            canState = false;
        }
    }
    protected override void FixedUpdate()
    {
        UpdateDamage();
        switch (stateCharacter)
        {
            case StateCharacter.isIdle:
                UpdateMove();
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
    protected override void UpdateMove()
    {
        base.UpdateMove();
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        motion = speed * Time.deltaTime * new Vector2(x, y);
        transform.Translate(motion);
    }
}
