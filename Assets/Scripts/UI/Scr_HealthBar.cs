using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_HealthBar : MonoBehaviour
{
    public Image _bar;
    public Image _background;
    public TMPro.TextMeshProUGUI _barNum;
    RectTransform rectBackground;

    //public GameObject _owner;
    public Scr_BaseHero _hero;

    float health;
    float maxHealth;

    float expandKoef = 3;
    void Start()
    {
        //_hero = _owner.GetComponent<Scr_BaseHero>();
        rectBackground = _background.rectTransform;
        rectBackground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _hero.Health.maxHealth * expandKoef);

        _barNum.text = _hero.Health.health.ToString();
        health = _hero.Health.health;
        maxHealth = _hero.Health.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hero.Health.maxHealth != maxHealth)
        {
            maxHealth = _hero.Health.maxHealth;
            rectBackground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxHealth * expandKoef);
        }
        if (_hero.Health.health != health)
        {
            health = _hero.Health.health;
            _barNum.text = ((int)_hero.Health.health).ToString();
            _bar.fillAmount = health / maxHealth;
        }
    }
}
