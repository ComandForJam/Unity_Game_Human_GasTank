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

    public GameObject labelDamage;
    List<Scr_LabelDamage> listLabels;
    void Start()
    {
        _healthBarPlayer.GetComponent<Scr_HealthBar>()._hero = _player.GetComponent<Scr_BaseHero>();
        _healthBarChainsaw.GetComponent<Scr_HealthBar>()._hero = _chainsaw.GetComponent<Scr_BaseHero>();
        listLabels = new List<Scr_LabelDamage>();
    }

    
    void Update()
    {
        
    }
    public void GameOver()
    {
        
        _healthBarChainsaw.SetActive(false); 
        _healthBarPlayer.SetActive(false);
        _panelGameOver.SetActive(true);

        _player.GetComponent<Scr_BaseHero>().LockController();
        _chainsaw.GetComponent<Scr_BaseHero>().LockController();
    }

    public void PlayAgain()
    {
        _healthBarChainsaw.SetActive(true);
        _healthBarPlayer.SetActive(true);
        _panelGameOver.SetActive(false);
        spawnPlayer.GameAgain();
    }

    public void Damage(Vector2 pos, string text)
    {
        bool ok = false;
        foreach (var label in listLabels)
        {
            if (!label.isActive)
            {
                label.Init(pos,text);
                ok = true;
                break;
            }
        }
        if (!ok)
        {
            GameObject label = Instantiate(labelDamage, transform);
            Scr_LabelDamage scrLabel = label.GetComponent<Scr_LabelDamage>();
            scrLabel.Init(pos, text);
            listLabels.Add(scrLabel);
        }
    }
}
