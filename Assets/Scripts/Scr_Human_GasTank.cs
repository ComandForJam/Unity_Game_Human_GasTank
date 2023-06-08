using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Human_GasTank : Scr_BaseCharacter
{
    public int pointsFear = 1; // ���� ������
    protected override void Start()
    {
        base.Start();
        speed = 10;
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        motion = new Vector2(x, y) * Time.deltaTime;
        transform.Translate(motion * speed);
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
        // ����� ��� ��������� �������� ��������� �����
    }
    protected override void Death()
    {
        // ����� ��� ��������� �������� ������
        
    }
}
