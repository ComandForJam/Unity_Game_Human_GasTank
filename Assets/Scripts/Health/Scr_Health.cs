using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Health
{
    public float maxHealth;
    public float health;

    public Scr_Health(float _maxHealth)
    {
        maxHealth = _maxHealth;
        health = maxHealth;
    }

    public void Damage(float damage)
    {
        health -= damage;
        
    }
    public void Heal(float heal)
    {
        health += heal;
    }
    public bool IsDeath
    {
        get
        {
            if (health <= 0) return true;
            return false;
        }

    }
    public bool HealthLessPercent(float percent)
    {
        return percent > health / maxHealth;
    }

}
