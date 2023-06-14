using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Replic : MonoBehaviour
{
    float timeLife = 1;
    float timer;

    Transform targetTr;
    public TMPro.TextMeshProUGUI tmp;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            transform.position = targetTr.position + Vector3.up * targetTr.localScale.y / 2;
        }
        else if (gameObject.activeSelf) gameObject.SetActive(false);
    }

    public void SayReplic(Transform _targetTr, string text)
    {
        targetTr = _targetTr;
        gameObject.SetActive(true);
        tmp.text = text;
        timer = timeLife;
    }
}
