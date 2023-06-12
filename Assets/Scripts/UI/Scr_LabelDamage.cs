using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_LabelDamage : MonoBehaviour
{
    float speed = 5;
    float timer = 0;
    float deathTime = 1;

    public bool isActive = false;

    public TMPro.TextMeshProUGUI tmp;
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= deathTime)
            { 
                gameObject.SetActive(false);
                isActive = false;
            }
            Vector2 motion = Vector2.up * speed * Time.deltaTime;
            transform.Translate(motion);
        }
    }
    public void Init(Vector2 pos, string text)
    {
        isActive = true;
        gameObject.SetActive(true);
        timer = 0;
        tmp.text = text;
        transform.position = pos;
    }
}
