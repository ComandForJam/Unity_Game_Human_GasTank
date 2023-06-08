using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Human_GasTank : Scr_BaseCharacter
{
    public int pointsFear = 1; // ���� ������
    void Start()
    {
        speed = 10;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        float x = Input.GetAxis("Horizontal") * speed;
        float y = Input.GetAxis("Vertical") * speed;
        Vector2 motion = new Vector2(x, y) * Time.deltaTime;
        transform.Translate(motion);
    }

    public override void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
        else
        {
            // ����� ��� ��������� �������� ��������� �����
        }
    }
    protected override void Death()
    {
        // ����� ��� ��������� �������� ������
        
    }
}
