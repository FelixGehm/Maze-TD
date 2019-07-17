using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	protected float _speed;
	protected int _dmg;
	protected Enemy _target;
	protected float _aoeRadius;
	protected float _ccTime;

	public void Init(Enemy target, int damage, float speed, float aoeRadius, float ccTime)
	{
		_target = target;
		_dmg = damage;
		_speed = speed;
		_aoeRadius = aoeRadius;
		_ccTime = ccTime;
	}

}
