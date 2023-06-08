using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BaseCharacter : MonoBehaviour
{// Скрипт, который наследуют все персонажи 

    protected float maxHealth;
    protected float health;

    protected float speed = 10;
    protected Vector2 motion;
    protected Vector2 direction; // Направление в котором смотрит персонаж
    protected bool faceRight = true; // Изначально персонаж смотрит вправо

    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {
        Direction();
    }
    protected void Direction()
    {
        if (motion != Vector2.zero)
        {
            Debug.Log("direction");
            direction = motion;
            Flip();
        }
        
        if (animator != null)
        {
            if (motion == Vector2.zero)
            {
                animator.SetFloat("VerticalMove", 0);
                animator.SetFloat("HorizontalMove", 0);
            } else
            {
                if (motion.x != 0)
                {
                    animator.SetFloat("HorizontalMove", 1);
                }
                if (motion.y > 0)
                {
                    animator.SetFloat("VerticalMove", 1);
                }
                if (motion.y < 0)
                {
                    animator.SetFloat("VerticalMove", -1);
                }

            }
        }
    }
    void Flip()
    {
        if (faceRight && direction.x < 0 || !faceRight && direction.x > 0)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            faceRight = !faceRight;
        }
    }

    public virtual void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }
    protected virtual void Attack()
    {

    }
    protected virtual void Death() { }
}
