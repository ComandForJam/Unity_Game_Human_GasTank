using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SliceArcher : MonoBehaviour
{
    public GameObject _arrow;
    public GameObject circleSlice;
    [SerializeField]
    float cooldown;
    [SerializeField]
    float beforeTime;
    float timer = 0;
    public bool canSlice = true; // Можно ли вызвать вообще эту функцию
    public bool isSlice = false; // true в промежутке от вызова удара до окончания кулдауна
    public bool canState = true; // true в промежутке от начала до animator event

    public float rangeAttack;
    public float damage;

    Vector2 direction;
    LayerMask layerMask;
    GameObject byGameObject;

    GameObject arrow;

    void Start()
    {
        arrow = Instantiate(_arrow);
        arrow.SetActive(false);
    }
    private void FixedUpdate()
    {
        CooldownUpdate();
    }
    void CooldownUpdate()
    {
        if (!canSlice)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= cooldown)
            {
                canSlice = true;
                isSlice = false;
            }
            else if (!canState && timer >= beforeTime + 0.3f)
            {
                canState = true;
            }
            else if (!isSlice && timer >= beforeTime)
            {
                arrow.SetActive(true);
                arrow.transform.position = (Vector2)transform.position + direction;
                isSlice = true;
            }
        }
    }
    public void Slice(Vector2 _targetPos, int _layerMask, GameObject _byGameObject)
    {
        if (canSlice)
        {
            arrow.transform.position = (Vector2)transform.position + direction;
            arrow.GetComponent<Scr_Bullet>().Slice(_targetPos, _byGameObject, _layerMask, damage);
            arrow.SetActive(false);

            canSlice = false;
            canState = false;
            
            timer = 0;
        }
    }
}
