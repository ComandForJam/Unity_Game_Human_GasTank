using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PointsFear : MonoBehaviour
{
    public Transform target;
    public float speed = 5;
    float timer = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (timer < 2) timer += Time.fixedDeltaTime;
        Vector2 dir = (target.position - transform.position).normalized;
        transform.Translate(dir * speed * timer * Time.fixedDeltaTime);
        if (Vector2.Distance(transform.position,target.position) < 0.5f)
        {
            target.GetComponent<Scr_BaseHero>().pointsFear++;
            Destroy(gameObject);
        }
    }
}
