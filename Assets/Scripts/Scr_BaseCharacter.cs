using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BaseCharacter : MonoBehaviour
{
    // Скрипт, который наследуют все персонажи 
    protected float maxHealth;
    protected float health;

    protected float speed = 10;

    void Start()
    {
        
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        
    }

    public virtual void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }
    protected virtual void Death() { }
}
