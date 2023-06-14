using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_SpawnPlayer : MonoBehaviour
{
    public GameObject _player;
    public GameObject _human_chainsaw;
   
    Camera _mainCamera;
    public Canvas _UI;

    Scr_Camera _scrCamera;

    public Scr_HealthBar _barPlayer;
    public Scr_HealthBar _barChainsaw;

    public GameObject core;

    public Texture2D cursorTexture;
    void Start()
    {
        Scr_BaseHero heroPlayer = _player.GetComponent<Scr_BaseHero>(); 
        Scr_BaseHero heroChainsaw = _human_chainsaw.GetComponent<Scr_BaseHero>();

        _mainCamera = Camera.main;
        _player = Instantiate(_player, transform);
        _human_chainsaw = Instantiate(_human_chainsaw, transform);
        _human_chainsaw.GetComponent<Scr_Human_Chainsaw>()._playerTr = _player.transform;


        _scrCamera = _mainCamera.GetComponent<Scr_Camera>();
        _scrCamera._chainsaw = _human_chainsaw.transform;
        _scrCamera._player = _player.transform;

        _UI = Instantiate(_UI);
        _UI.GetComponent<Canvas>().worldCamera = Camera.main;
        Scr_UI _scr_UI = _UI.GetComponent<Scr_UI>();
        _scr_UI._player = _player;
        _scr_UI._chainsaw = _human_chainsaw;
        _scr_UI.spawnPlayer = this;

        //_barChainsaw._hero = heroChainsaw;
        //_barPlayer._hero = heroPlayer;

        //heroChainsaw._scr_ui = _scr_UI;
        //heroPlayer.ui = _scr_UI;
        _player.GetComponent<Scr_Human_GasTank>()._chainsaw = _human_chainsaw.GetComponent<Scr_Human_Chainsaw>();

        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameAgain();
        }
    }
    
    public void GameAgain()
    {
        //DontDestroyOnLoad(gameObject);

        //_player.transform.position = transform.position;
       // _human_chainsaw.transform.position = transform.position;
        //_human_chainsaw.GetComponent<Scr_BaseHero>().Health.Heal(1000);
        //_player.GetComponent<Scr_BaseHero>().Health.Heal(1000);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
