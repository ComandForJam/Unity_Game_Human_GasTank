using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Human_Chainsaw : Scr_BaseCharacter
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
