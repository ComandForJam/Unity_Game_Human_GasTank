using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Poison : MonoBehaviour
{
    public ParticleSystem particlePoison;

    float radiusPoison = 3f;
    public float damagePoison = 1.5f;

    float cooldownPoison = 0.2f;
    bool canPoison = true;
    float timerPoison = 0;
    public bool isDeath = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        CooldownUpdate();
        Poisoning();
    }
    void CooldownUpdate()
    {
        if (!canPoison)
        {
            timerPoison += Time.fixedDeltaTime;
            if (timerPoison >= cooldownPoison)
            {
                canPoison = true;
            }
        }
    }
    private void Poisoning()
    {
        if (!isDeath)
        {
            if (canPoison)
            {
                Scr_Attack.ActionPoison(transform.position, radiusPoison, LayerMask.GetMask("Enemy"), damagePoison, gameObject);
                
                canPoison = false;
                timerPoison = 0;
            }
        } else
        {
                particlePoison.Stop();
        }
    }
}
