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
    protected SpriteRenderer sprite;

    private float damageTimer = 0;
    private bool damageTimerActive = false;

    public List<AudioClip> audios;
    public List<float> volumeScalesForAudios;
    protected AudioSource audioSource;
    protected AudioCode lastPlayed;

    protected new Collider2D collider;

    protected bool isDeath = false;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        if (!isDeath)
        {
            Direction();
        }
        if (damageTimerActive)
        {
            damageTimer -= Time.fixedDeltaTime;
            if (damageTimer <= 0)
            {
                sprite.color = Color.white;
            }
        }
    }
    protected void Direction()
    {
        
        if (motion != Vector2.zero)
        {
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
                if (motion.x != 0 && Mathf.Abs(motion.x) > Mathf.Abs(motion.y))
                {
                    animator.SetFloat("HorizontalMove", 1);
                    animator.SetFloat("VerticalMove", 0);
                }
                else if (motion.y > 0)
                {
                    animator.SetFloat("HorizontalMove", 0);
                    animator.SetFloat("VerticalMove", 1);
                }
                else if (motion.y < 0)
                {
                    animator.SetFloat("HorizontalMove", 0);
                    animator.SetFloat("VerticalMove", -1);
                }

            }
        }
    }
    void Flip()
    {
        if (faceRight && direction.x < 0 || !faceRight && direction.x > 0)
        {
            sprite.flipX = !sprite.flipX;
            //transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            faceRight = !faceRight;
        }
    }

    public virtual void Damage(float damage)
    {
        if (audioSource != null)
        {
            if (!audioSource.isPlaying || audioSource.isPlaying && lastPlayed != AudioCode.attack)
            {
                PlayAudio(AudioCode.damage);
            }
            
        }
        damageTimer = 0.05f;
        damageTimerActive = true;
        sprite.color = Color.red;

        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }
    
    protected virtual void Death() 
    { 
        if (!isDeath)
        {
            if (animator != null)
            {
                animator.SetBool("isDeath", true);
            }
            if (audioSource != null)
            {
                PlayAudio(AudioCode.death);
            }
            isDeath = true;
        }
    }

    protected void PlayAudio(AudioCode code)
    {
        lastPlayed = code;
        if (audios.Count - 1 >= (int)code && volumeScalesForAudios.Count - 1 >= (int)code)
        audioSource.PlayOneShot(audios[(int)code], volumeScalesForAudios[(int)code]);
    }
}

public enum AudioCode // Коды звуков
{
    attack = 0, // удара
    damage = 1, // получения урона
    death = 2 // смерити
} 
