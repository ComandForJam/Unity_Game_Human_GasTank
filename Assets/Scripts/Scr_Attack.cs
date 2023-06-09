using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Attack
{

	// ������� ���������� ��������� ������ �� �������, ������������ ��������� �������
	public static GameObject NearTarget(Vector3 position, Collider2D[] array)
	{
		if (array.Length == 0) return null;

		Collider2D current = null;
		float dist = Mathf.Infinity;

		foreach (Collider2D coll in array)
		{
			float curDist = Vector3.Distance(position, coll.transform.position);

			if (curDist < dist)
			{
				current = coll;
				dist = curDist;
			}
		}

		return (current != null) ? current.gameObject : null;
	}
	public static GameObject FarTarget(Vector3 position, Collider2D[] array)
	{
		if (array.Length == 0) return null;

		Collider2D current = null;
		float dist = 0;

		foreach (Collider2D coll in array)
		{
			float curDist = Vector3.Distance(position, coll.transform.position);

			if (curDist > dist)
			{
				current = coll;
				dist = curDist;
			}
		}

		return (current != null) ? current.gameObject : null;
	}

	// point - ����� ��������
	// radius - ������ ���������
	// layerMask - ����� ����, � ������� ����� ��������������
	// damage - ��������� ����
	// allTargets - ������-�� �������� ���� ��� ����, �������� � ���� ���������
	public static void Action(Vector2 point, float radius, int layerMask, float damage, bool allTargets, bool stan)
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, layerMask);
		if (colliders.Length == 0) return;

		if (!allTargets)
		{
			GameObject obj = NearTarget(point, colliders);
			Scr_BaseCharacter baseChara = obj.GetComponent<Scr_BaseCharacter>();
			if (obj != null && baseChara != null)
			{
				baseChara.Damage(damage, stan);
			}
			return;
		}

		foreach (Collider2D hit in colliders)
		{
			Scr_BaseCharacter baseChara = hit.GetComponent<Scr_BaseCharacter>();
			if (baseChara != null)
			{
				baseChara.Damage(damage, stan);
			}
		}
	}
	public static void ActionPoison(Vector2 point, float radius, int layerMask, float damage) // Poison
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, layerMask);
		if (colliders.Length == 0) return;

		foreach (Collider2D hit in colliders)
		{
			Scr_BaseCharacter baseChara = hit.GetComponent<Scr_BaseCharacter>();
			if (baseChara != null)
			{
				float _damage = damage / Vector2.Distance(point, hit.transform.position);
				_damage = Mathf.Clamp(_damage, damage/10, damage);
				baseChara.Damage(_damage, false);
			}
		}
	}
}