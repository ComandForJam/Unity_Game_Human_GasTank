using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BaseCharacter : MonoBehaviour
{// Скрипт, который наследуют все персонажи 
    public GameObject particlePointsFear;
    public int pointsFear = 10; // Очки страха

    public Scr_Health Health;
    public float baseHealth = 50;

    protected float speed = 10;
    protected Vector2 motion;
    protected Vector2 direction; // Направление в котором смотрит персонаж
    protected bool faceRight = true; // Изначально персонаж смотрит вправо

    protected Animator animator;
    protected SpriteRenderer sprite;

    private float damageTimer = 0;

    public List<AudioClip> audios;
    public List<float> volumeScalesForAudios;
    protected AudioSource audioSource;
    protected AudioCode lastPlayed;

    protected new Collider2D collider;

    protected bool isDeath = false;

    public StateCharacter stateCharacter;
    protected bool canState = true; // Можно ли изменить состояние

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
        if (motion != Vector2.zero)
        {
            direction = motion.normalized;
            Flip();
        }
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
    protected void Flip()
    {
        if (faceRight && direction.x < 0 || !faceRight && direction.x > 0)
        {
            sprite.flipX = !sprite.flipX;
            faceRight = !faceRight;
        }
    }

    public virtual void Damage(float damage, bool stan, GameObject byGameObject)
    {
        if (stateCharacter != StateCharacter.isDeath)
        {
            if (audioSource != null)
            {
                PlayAudio(AudioCode.damage);
            }
            Health.Damage(damage);
            if (Health.IsDeath)
            {
                Death(byGameObject);
                return;
            }
            if (stan)
            {
                damageTimer = 0.2f;
                sprite.color = Color.red;
            }
        }
        if (stan)
            transform.Translate(-(byGameObject.transform.position - transform.position).normalized * 0.5f);
    }

    protected virtual void Death(GameObject byGameObject)
    {
        if (stateCharacter != StateCharacter.isDeath)
        {
            Scr_BaseHero hero = byGameObject.GetComponent<Scr_BaseHero>();
            if (hero != null)
            {
                for (int i = 0; i < pointsFear; i++)
                {
                    GameObject particle = Instantiate(particlePointsFear);
                    particle.transform.position = (Vector2)transform.position + new Vector2((Random.value * transform.localScale.y), (Random.value * transform.localScale.y));
                    particle.GetComponent<Scr_PointsFear>().target = byGameObject.transform;
                }
            }
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
            //Destroy(gameObject, 1);
        }
    }

    protected void PlayAudio(AudioCode code)
    {
        lastPlayed = code;
        if (audios.Count - 1 >= (int)code && volumeScalesForAudios.Count - 1 >= (int)code)
            audioSource.PlayOneShot(audios[(int)code], volumeScalesForAudios[(int)code]);
    }
    protected void FindPath()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2, LayerMask.GetMask("Enemy"));


        if (colliders.Length == 0) return;
        Vector2 vector = (colliders[0].transform.position - transform.position).normalized;
        if (colliders.Length > 1)
        {
            foreach (var item in colliders)
            {
                vector += (Vector2)(item.transform.position - transform.position).normalized;
            }
        }
        if (Vector2.Angle(vector, motion.normalized) < 20)
        {
            motion -= vector;
        }

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

public enum AudioCode // Коды звуков
{
    attack = 0, // удара
    damage = 1, // получения урона
    death = 2 // смерти
}
