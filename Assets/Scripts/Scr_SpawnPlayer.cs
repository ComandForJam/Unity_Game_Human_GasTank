using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SpawnPlayer : MonoBehaviour
{
    public GameObject _player;
    public GameObject _human_chainsaw;
   
    public Camera _mainCamera;
    public Canvas _UI;

    Scr_Camera _scrCamera;
    void Start()
    {
        _mainCamera = Instantiate(_mainCamera);
        _player = Instantiate(_player, transform);
        _human_chainsaw = Instantiate(_human_chainsaw, transform);
        _human_chainsaw.GetComponent<Scr_Human_Chainsaw>()._playerTr = _player.transform;

        _scrCamera = _mainCamera.GetComponent<Scr_Camera>();
        _scrCamera._chainsaw = _human_chainsaw.transform;
        _scrCamera._player = _player.transform;

        _UI = Instantiate(_UI);
        Scr_UI _scr_UI = _UI.GetComponent<Scr_UI>();
        _scr_UI._player = _player;
        _scr_UI._chainsaw = _human_chainsaw;
    }

    
    void Update()
    {
        
    }
}
