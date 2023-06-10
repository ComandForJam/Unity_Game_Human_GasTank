using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BaseCharacter : MonoBehaviour
{// ������, ������� ��������� ��� ��������� 

    public Scr_Health Health;
    public float baseHealth = 50;

    protected float speed = 10;
    protected Vector2 motion;
    protected Vector2 direction; // ����������� � ������� ������� ��������
    protected bool faceRight = true; // ���������� �������� ������� ������

    protected Animator animator;
    protected SpriteRenderer sprite;

    private float damageTimer = 0;

    public List<AudioClip> audios;
    public List<float> volumeScalesForAudios;
    protected AudioSource audioSource;
    protected AudioCode lastPlayed;

    protected new Collider2D collider;

    protected bool isDeath = false;

    protected StateCharacter stateCharacter;
    protected bool canState = true; // ����� �� �������� ���������

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider2D>();

        stateCharacter = StateCharacter.isIdle;
        motion = Vector2.down;
        direction = motion;

        Health = new Scr_Health(baseHealth);
    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {
        UpdateDamage();
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
            case StateCharacter.isDeath:
                break;
        }
    }
    protected virtual void UpdateIdle()
    {
        UpdateAnimator();
    }
    protected virtual void UpdateMove()
    {
        if (motion != Vector2.zero)
        {
            direction = motion.normalized;
            Flip();
        }
        UpdateAnimator();
    }
    protected virtual void UpdateAnimator()
    {
        if (animator != null)
        {
            if (motion == Vector2.zero)
            {
                animator.SetFloat("VerticalMove", 0);
                animator.SetFloat("HorizontalMove", 0);
            }
            else
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
    protected virtual void UpdateDamage()
    {
        if (damageTimer > 0)
        {
            damageTimer -= Time.fixedDeltaTime;
            if (damageTimer <= 0)
            {
                //canState = true;
                //stateCharacter = StateCharacter.isIdle;
                sprite.color = Color.white;
            }
        }
    }
    protected virtual void UpdateSlice()
    {

    }
    void Flip()
    {
        if (faceRight && direction.x < 0 || !faceRight && direction.x > 0)
        {
            sprite.flipX = !sprite.flipX;
            faceRight = !faceRight;
        }
    }

    public virtual void Damage(float damage, bool stan)
    {
        if (audioSource != null)
        {
            PlayAudio(AudioCode.damage);
        }
        Health.Damage(damage);
        if (Health.IsDeath)
        {
            Death();
            return;
        }
        if (stan)
        {
            damageTimer = 0.05f;
            sprite.color = Color.red;
            //stateCharacter = StateCharacter.isDamage;
        }
        //canState = !stan;
    }

    protected virtual void Death()
    {
        if (stateCharacter != StateCharacter.isDeath)
        {
            if (animator != null)
            {
                animator.SetBool("isDeath", true);
            }
            if (audioSource != null)
            {
                PlayAudio(AudioCode.death);
            }
            stateCharacter = StateCharacter.isDeath;
            canState = false;
            Health.health = 0;
            Destroy(gameObject, 1);
        }
    }

    protected void PlayAudio(AudioCode code)
    {
        lastPlayed = code;
        if (audios.Count - 1 >= (int)code && volumeScalesForAudios.Count - 1 >= (int)code)
            audioSource.PlayOneShot(audios[(int)code], volumeScalesForAudios[(int)code]);
    }
}
public enum StateCharacter
{
    isIdle = 0,
    isMove = 1,
    isSlice = 2,
    isDash = 3,
    isDamage = 4,
    isLifeSave = 5,
    isHeal = 6,
    isDeath = 9
}

public enum AudioCode // ���� ������
{
    attack = 0, // �����
    damage = 1, // ��������� �����
    death = 2 // ������
}
