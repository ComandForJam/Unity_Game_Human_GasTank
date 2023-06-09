using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SpawnPlayer : MonoBehaviour
{
    public GameObject _player;
    public GameObject _human_chainsaw;
    public Scr_Camera _scrCamera;
    void Start()
    {
        _player = Instantiate(_player, transform);
        _human_chainsaw = Instantiate(_human_chainsaw, transform);
        _human_chainsaw.GetComponent<Scr_Human_Chainsaw>()._playerTr = _player.transform;
        _scrCamera._chainsaw = _human_chainsaw.transform;
        _scrCamera._player = _player.transform;
    }

    
    void Update()
    {
        
    }
}
