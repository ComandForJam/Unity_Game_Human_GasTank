using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Human_Chainsaw : Scr_BaseCharacter
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

        //transform.Translate(motion);
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
