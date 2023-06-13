using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Bullet : MonoBehaviour
{
    LayerMask layerMask;
    GameObject owner;
    float damage;
    [SerializeField]
    float speed = 10;
    [SerializeField]
    Vector2 motion;

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            transform.Translate(Mathf.Abs(motion.x), motion.y, 0);
            //Debug.Log(transform.right);
        }
    }


    public void Slice(Vector2 targetPos, GameObject _owner, LayerMask _layerMask, float _damage)
    {
        Vector2 dir = (targetPos - (Vector2)transform.position).normalized;
        float rotateZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateZ);
        owner = _owner;
        layerMask = _layerMask;
        damage = _damage;
        motion = dir;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.layer + " " + layerMask);
        if (gameObject.activeSelf)
        {
            if ((int)Mathf.Pow(2, collision.gameObject.layer) != layerMask)
            {
                if ((int)Mathf.Pow(2, collision.gameObject.layer) != (layerMask == LayerMask.GetMask("Enemy") ? LayerMask.GetMask("Heroes") : LayerMask.GetMask("Enemy")))
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                collision.GetComponent<Scr_BaseCharacter>().Damage(damage, true, owner);
                gameObject.SetActive(false);
            }
        }
    }
}
