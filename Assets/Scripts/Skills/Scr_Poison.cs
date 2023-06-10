using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Poison : MonoBehaviour
{
    float radiusPoison = 3.5f;
    public float damagePoison = 1.5f;

    float cooldownPoison = 0.1f;
    bool canPoison = true;
    float timerPoison = 0; 

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
        if (canPoison)
        {
            Scr_Attack.ActionPoison(transform.position, radiusPoison, LayerMask.GetMask("Enemy"), damagePoison);
            canPoison = false;
            timerPoison = 0;
        }
    }
}
