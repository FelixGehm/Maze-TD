using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	private float _speed = 70;
	private int _dmg = 50;

	private Enemy _target;

	private bool _targetSet = false;

	public void Init(Enemy target, int damage, float speed)
	{
		_target = target;
		_dmg = damage;
		_speed = speed;
	}

	private void Update()
	{
		if (_target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 direction = _target.transform.position - transform.position;
		float distance = _speed * Time.deltaTime;

		if (direction.magnitude <= distance)
		{
			HitTarget();
			return;
		}

		transform.Translate(direction.normalized * distance, Space.World);
	}

	private void HitTarget()
	{
		_target.TakeDamage(_dmg);
		Destroy(gameObject);
	}
}
