using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SliceAround : MonoBehaviour
{

    public GameObject circleSlice;
    [SerializeField]
    float cooldown;
    [SerializeField]
    float timer = 0;
    public bool canSlice = true; // Можно ли вызвать вообще эту функцию
    public bool isSlice = false; // true в промежутке от вызова удара до окончания кулдауна
    public bool canState = true; // true в промежутке от начала до animator event

    public float rangeAttack;
    public float damage;
    public float radiusAttack;

    Vector2 direction;
    LayerMask layerMask;
    bool stan;
    GameObject byGameObject;

    int switchIter = 0;

    void Start()
    {

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
            switch (timer)
            {
                case < 0.2f:
                    if (switchIter < 1)
                    {
                        switchIter++;
                    }
                    break;
                case < 0.4f:
                    if (switchIter < 2)
                    {
                        float x = rangeAttack * Mathf.Cos(switchIter);
                        float y = rangeAttack * Mathf.Sin(switchIter);
                        Vector2 pos = new Vector2(x, y);
                        Scr_Attack.Action((Vector2)transform.position + pos, radiusAttack, layerMask, damage, false, stan, byGameObject);
                        CreateCircle(pos);

                        switchIter++;
                    }
                    break;
                case < 0.6f:
                    if (switchIter < 3)
                    {
                        float x = rangeAttack * Mathf.Cos(switchIter);
                        float y = rangeAttack * Mathf.Sin(switchIter);
                        Vector2 pos = new Vector2(x, y);
                        Scr_Attack.Action((Vector2)transform.position + pos, radiusAttack, layerMask, damage, false, stan, byGameObject);
                        CreateCircle(pos);

                        switchIter++;
                    }
                    break;
                case < 0.8f:
                    if (switchIter < 4)
                    {
                        float x = rangeAttack * Mathf.Cos(switchIter);
                        float y = rangeAttack * Mathf.Sin(switchIter);
                        Vector2 pos = new Vector2(x, y);
                        Scr_Attack.Action((Vector2)transform.position + pos, radiusAttack, layerMask, damage, false, stan, byGameObject);
                        CreateCircle(pos);

                        switchIter++;
                    }
                    break;
                case < 1f:
                    if (switchIter < 5)
                    {
                        float x = rangeAttack * Mathf.Cos(switchIter);
                        float y = rangeAttack * Mathf.Sin(switchIter);
                        Vector2 pos = new Vector2(x, y);
                        Scr_Attack.Action((Vector2)transform.position + pos, radiusAttack, layerMask, damage, false, stan, byGameObject);
                        CreateCircle(pos);

                        switchIter++;
                    }
                    break;
                case < 2f:
                    if (switchIter < 5)
                    {
                        canState = true;
                        switchIter++;
                    }
                    break;

            }
            if (timer >= cooldown)
            {
                canSlice = true;
            }
        }
    }
    public void Slice(Vector2 _direction, LayerMask _layerMask, bool _stan, GameObject _byGameObject)
    {
        if (canSlice)
        {
            //Debug.Log(gameObject + " " + radiusAttack);
            direction = _direction;
            layerMask = _layerMask;
            stan = _stan;
            byGameObject = _byGameObject;

            canSlice = false;
            canState = false;
            timer = 0;
        }
    }
    void CreateCircle(Vector2 _dir)
    {
        GameObject circle = Instantiate(circleSlice, (Vector2)transform.position + _dir, transform.rotation);
        circle.transform.localScale = new Vector2(radiusAttack, radiusAttack);
        Destroy(circle, 0.2f);
        if (layerMask == LayerMask.GetMask("Enemy")) circle.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 0.5f);
        else if (layerMask == LayerMask.GetMask("Heroes")) circle.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
    }
}
