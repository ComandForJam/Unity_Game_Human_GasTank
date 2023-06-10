using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_UI : MonoBehaviour
{
    public Scr_SpawnPlayer spawnPlayer;

    public GameObject _healthBarPlayer;
    public GameObject _healthBarChainsaw;

    public GameObject _panelGameOver;

    public GameObject _player;
    public GameObject _chainsaw;
    void Start()
    {
        _healthBarPlayer.GetComponent<Scr_HealthBar>()._owner = _player;
        _healthBarChainsaw.GetComponent<Scr_HealthBar>()._owner = _chainsaw;
    }

    
    void Update()
    {
        
    }
    public void GameOver()
    {
        _healthBarChainsaw.SetActive(false);
        _healthBarPlayer.SetActive(false);
        _panelGameOver.SetActive(true);
    }

    public void PlayAgain()
    {
        _healthBarChainsaw.SetActive(true);
        _healthBarPlayer.SetActive(true);
        _panelGameOver.SetActive(false);
        spawnPlayer.GameAgain();
    }
}
