using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Heal : MonoBehaviour
{
    public GameObject _bulletHeal;
    [SerializeField]
    float cooldown;
    public float heal;
    float timer = 0;
    public bool canHeal = true;
    public bool isDeath = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CooldownUpdate();
    }
    void CooldownUpdate()
    {
        if (!canHeal)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= cooldown)
            {
                canHeal = true;
            }
        }
    }
    public void Heal()
    {
        if (!isDeath)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            GameObject bulletHeal = Instantiate(_bulletHeal);
            bulletHeal.transform.position = transform.position + difference.normalized * 1;
            bulletHeal.transform.localRotation = Quaternion.Euler(0f, 0f, rotateZ);
            bulletHeal.GetComponent<Scr_BulletHeal>().owner = gameObject;
            bulletHeal.GetComponent<Scr_BulletHeal>().heal = heal;

            canHeal = false;
            timer = 0;
        }
    }
}
