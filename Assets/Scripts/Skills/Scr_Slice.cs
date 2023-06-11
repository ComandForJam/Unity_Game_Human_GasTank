using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Slice : MonoBehaviour
{
    public GameObject circleSlice;
    [SerializeField]
    float cooldown;
    float timer = 0;
    public bool canSlice = true;
    public bool isSlice = false;

    public float rangeAttack;
    public float damage;
    public float radiusAttack;

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
    public void Slice(Vector2 direction, LayerMask layerMask, bool stan, GameObject byGameObject)
    {
        if (canSlice)
        {
            //Debug.Log(gameObject + " " + radiusAttack);
            Scr_Attack.Action((Vector2)transform.position + direction * rangeAttack, radiusAttack, layerMask, damage, false, stan, byGameObject);
            
            canSlice = false;
            isSlice = true;
            timer = 0;
            GameObject circle = Instantiate(circleSlice, (Vector2)transform.position + direction * rangeAttack, transform.rotation);
            circle.transform.localScale = new Vector2(radiusAttack, radiusAttack);
            Destroy(circle, 0.2f);
            if (layerMask == LayerMask.GetMask("Enemy")) circle.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 0.5f);
            else if (layerMask == LayerMask.GetMask("Heroes")) circle.GetComponent<SpriteRenderer>().color = new Color(1,0,0,0.5f);
            
        }
    }
}
