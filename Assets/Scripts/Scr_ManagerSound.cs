using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ManagerSound : MonoBehaviour
{
    List<Scr_TriggerMobs> listTriggers;
    AudioSource audioSource;
    void Start()
    {
        listTriggers = new();
        for (int i = 0; i < transform.childCount; i++)
        {
            Scr_TriggerMobs trMobs = transform.GetChild(i).GetComponent<Scr_TriggerMobs>();
            if (trMobs != null)
            {
                listTriggers.Add(trMobs);
                trMobs._manager = this;
            }
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    void Update()
    {
        
    }
    public void Play()
    {
        audioSource.Play();
    }
    public void Stop()
    {
        audioSource.Stop();
    }
}
