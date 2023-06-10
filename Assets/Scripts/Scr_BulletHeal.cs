using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BulletHeal : MonoBehaviour
{
    public GameObject owner;
    public float speed = 10;
    public float heal = 10;

    bool isDeath = false;
    float timer = 1;
    void Start()
    {
        Destroy(gameObject, timer);
    }

    void FixedUpdate()
    {

        if (!isDeath)
        {
            timer -= Time.fixedDeltaTime;
            Vector2 motion = speed * Time.fixedDeltaTime * Vector2.right * timer;
            transform.Translate(motion);
        }
            
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Scr_BaseHero hero = collision.GetComponent<Scr_BaseHero>();
        if (collision.gameObject != owner) {
            if (hero != null)
            {
                hero.Healed(heal, owner);
            }
            Boom();
        } 
    }
    void Boom()
    {
        isDeath = true;
        ParticleSystem.ShapeModule shape = GetComponent<ParticleSystem>().shape;
        shape.arcMode  = ParticleSystemShapeMultiModeValue.Random;
        Destroy(gameObject,0.2f);
    }
}
