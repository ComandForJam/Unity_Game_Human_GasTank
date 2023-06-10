using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_HealthHero : Scr_Health
{
    public float baseHealth;
    public float pointsfear;
    public float koeff = 2;

    public Scr_HealthHero(float _baseHealth, float fear) : base (_baseHealth)
    {
        baseHealth = _baseHealth;
        maxHealth = baseHealth + fear * koeff;
        health = maxHealth;
    }

    public float UpdateDamagePoison()
    {
        return health / 90;
    }
    public void UpdateMaxHealth(float fear)
    {
        float prevMax = maxHealth;
        pointsfear = fear;
        maxHealth = baseHealth + fear * koeff;
        
        health = health * maxHealth / prevMax;
        if (health > maxHealth) health = maxHealth;
    }
}
