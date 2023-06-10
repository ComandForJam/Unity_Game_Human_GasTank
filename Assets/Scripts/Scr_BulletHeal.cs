using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BulletHeal : MonoBehaviour
{
    public GameObject owner;
    public float speed = 10;
    public float heal = 10;
    void Start()
    {
        Destroy(gameObject, 1);
    }

    void FixedUpdate()
    {
        transform.Translate(speed * Time.fixedDeltaTime * Vector2.right);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Scr_BaseHero hero = collision.GetComponent<Scr_BaseHero>();
        if (collision.gameObject != owner) {
            if (hero != null)
            {
                hero.Health.Heal(heal);
            }
            Boom();
        } 
        
    }
    void Boom()
    {
        Destroy(gameObject);
    }
}
