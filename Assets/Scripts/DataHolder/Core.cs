using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public float pointsFearHero1;
    public float pointsFearHero2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float [] GetData()
    {
        Destroy(gameObject, 3);
        return new [] { pointsFearHero1, pointsFearHero2 };
    }
}
