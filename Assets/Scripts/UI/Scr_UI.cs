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

    public Transform tutorial;
    float timer = 0;
    int okTutorial = 0;
    void Start()
    {
        _healthBarPlayer.GetComponent<Scr_HealthBar>()._hero = _player.GetComponent<Scr_BaseHero>();
        _healthBarChainsaw.GetComponent<Scr_HealthBar>()._hero = _chainsaw.GetComponent<Scr_BaseHero>();
        listLabels = new List<Scr_LabelDamage>();
    }


    void Update()
    {
        UpdateTutorial();
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
                label.Init(pos, text);
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

    void UpdateTutorial()
    {
        if (okTutorial < 6)
        {
            timer += Time.deltaTime;
            switch (timer)
            {
                case < 1f:
                    if (okTutorial < 1)
                    {
                        tutorial.GetChild(0).gameObject.SetActive(true);
                        okTutorial = 1;
                    }
                    break;
                case < 2.5f:
                    if (okTutorial < 2)
                    {
                        tutorial.GetChild(1).gameObject.SetActive(true);
                        okTutorial = 2;
                    }
                    break;
                case < 4f:
                    if (okTutorial < 3)
                    {
                        tutorial.GetChild(2).gameObject.SetActive(true);
                        okTutorial = 3;
                    }
                    break;
                case < 5.5f:
                    if (okTutorial < 4)
                    {
                        tutorial.GetChild(3).gameObject.SetActive(true);
                        okTutorial = 4;
                    }
                    break;
                case > 10f:
                    if (okTutorial < 6)
                    {
                        tutorial.GetChild(2).gameObject.SetActive(false);
                        tutorial.GetChild(3).gameObject.SetActive(false);
                        okTutorial = 6;
                    }
                    break;
                case > 8f:
                    if (okTutorial < 5)
                    {
                        tutorial.GetChild(0).gameObject.SetActive(false);
                        tutorial.GetChild(1).gameObject.SetActive(false);
                        okTutorial = 5;
                    }
                    break;

            }
        }
    }
}
