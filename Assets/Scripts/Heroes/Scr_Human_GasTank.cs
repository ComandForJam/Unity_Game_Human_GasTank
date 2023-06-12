using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Human_GasTank : Scr_BaseHero
{
    Scr_Poison _poison;
    Scr_Heal _heal;
    public Transform dirIndicator;

    protected override void Start()
    {
        base.Start();
        _poison = GetComponent<Scr_Poison>();
        _heal = GetComponent<Scr_Heal>();
        speed = 10;
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
        UpdateDirIndicator();
        UpdateDamage();
        Heal();
        ((Scr_HealthHero)Health).UpdateMaxHealth(pointsFear);
        _poison.damagePoison = ((Scr_HealthHero)Health).UpdateDamagePoison();
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
            case StateCharacter.isHeal:
                UpdateMove();
                break;
            case StateCharacter.isDeath:
                UpdateDeath();
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
    void Heal()
    {
        if (Input.GetMouseButton(0) && canState && _heal.canHeal)
        {
            if (Health.health > _heal.heal / 1.2f)
            {
                _heal.Heal();
                Health.Damage(_heal.heal / 1.2f);
            }
            else
            {

            }
        }
    }

    protected void UpdateDeath()
    {
        _poison.isDeath = true;
        _heal.isDeath = true;
    }
    void UpdateDirIndicator()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        dirIndicator.rotation = Quaternion.Euler(0,0, rotateZ);
    }
}
