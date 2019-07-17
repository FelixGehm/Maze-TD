using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCProjectile : Projectile
{
	private bool _targetSet = false;

	private void Update()
	{
		if (_target == null)
		{
			Destroy(gameObject);
			return;
		}

		
		Collider[] colliders = Physics.OverlapSphere(transform.position, _aoeRadius);
		foreach (var c in colliders)
		{
			if (c.tag == "Enemy")
			{
				Debug.Log("nofnw");
				Enemy e = c.GetComponent<Enemy>();
				e.TakeDamage(_dmg);
				e.Slow(0.5f, _ccTime);
			}
		}
		Destroy(gameObject);
	}
}
