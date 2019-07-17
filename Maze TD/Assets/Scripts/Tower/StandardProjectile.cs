using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardProjectile : Projectile
{
	private bool _targetSet = false;

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
