using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Attack
{

	// функция возвращает ближайший объект из массива, относительно указанной позиции
	public static GameObject NearTarget(Vector3 position, Collider2D[] array)
	{
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

	// point - точка контакта
	// radius - радиус поражения
	// layerMask - номер слоя, с которым будет взаимодействие
	// damage - наносимый урон
	// allTargets - должны-ли получить урон все цели, попавшие в зону поражения
	public static void Action(Vector2 point, float radius, int layerMask, float damage, bool allTargets)
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, layerMask);

		if (!allTargets)
		{
			GameObject obj = NearTarget(point, colliders);
			Scr_BaseCharacter baseChara = obj.GetComponent<Scr_BaseCharacter>();
			if (obj != null && baseChara != null)
			{
				baseChara.Damage(damage);
			}
			return;
		}

		foreach (Collider2D hit in colliders)
		{
			Scr_BaseCharacter baseChara = hit.GetComponent<Scr_BaseCharacter>();
			if (baseChara != null)
			{
				baseChara.Damage(damage);
			}
		}
	}
	public static void ActionPoison(Vector2 point, float radius, int layerMask, float damage)
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, layerMask);

		foreach (Collider2D hit in colliders)
		{
			Scr_BaseCharacter baseChara = hit.GetComponent<Scr_BaseCharacter>();
			if (baseChara != null)
			{
				float _damage = damage / Vector2.Distance(point, hit.transform.position);
				_damage = Mathf.Clamp(_damage, damage/10, damage);
				baseChara.Damage(_damage);
			}
		}
	}
}