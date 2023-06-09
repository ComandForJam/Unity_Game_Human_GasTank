using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_UI : MonoBehaviour
{
    public GameObject _healthBarPlayer;
    public GameObject _healthBarChainsaw;

    public GameObject _player;
    public GameObject _chainsaw;
    void Start()
    {
        _healthBarPlayer.GetComponent<Scr_HealthBar>()._owner = _player;
        _healthBarChainsaw.GetComponent<Scr_HealthBar>()._owner = _chainsaw;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
