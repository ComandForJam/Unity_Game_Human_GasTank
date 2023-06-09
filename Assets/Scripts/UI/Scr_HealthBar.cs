using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_HealthBar : MonoBehaviour
{
    public Image _bar;
    public Image _background;

    public GameObject _owner;

    int stateCharacter;
    Scr_Human_GasTank _gasTank;
    Scr_Human_Chainsaw _chainsaw;
    void Start()
    {
        _gasTank = _owner.GetComponent<Scr_Human_GasTank>();
        _chainsaw = _owner.GetComponent<Scr_Human_Chainsaw>();

        if (_gasTank != null)
        {
            stateCharacter = 1;
        } else if (_chainsaw != null)
        {
            stateCharacter = 2;
        }
        //_bar.GetComponent<Rect>().width =
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
