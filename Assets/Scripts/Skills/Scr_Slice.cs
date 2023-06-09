using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Slice : MonoBehaviour
{
    [SerializeField]
    float cooldown = 1;
    float timer = 0;
    public bool canSlice = true;
    public bool isSlice = false;

    public float rangeAttack = 1;
    [SerializeField]
    float damage = 5;
    public float radiusAttack = 1;

    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        CooldownUpdate();
    }
    void CooldownUpdate()
    {
        if (!canSlice)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= cooldown)
            {
                canSlice = true;
            }
            if (timer >= 0.3f) isSlice = false;
        }
    }
    public void Slice(Vector2 direction, LayerMask layerMask, bool stan)
    {
        if (canSlice)
        {
            Scr_Attack.Action((Vector2)transform.position + direction * rangeAttack, radiusAttack, layerMask, damage, false, stan);
            canSlice = false;
            isSlice = true;
            timer = 0;
        }
    }
}
