﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	[Header("Setup Fields")]
	[SerializeField]
	private Projectile _projectilePrefab;
	[SerializeField]
	private Transform _head;
	[SerializeField]
	private Transform __firePoint;
	[SerializeField]
	private TowerStats _stats;

	private GameManager _gameManager;
	private List<Enemy> _enemyUnits;

	private float _turnSpeed = 10;
	private Enemy _target;
	private float _fireCountdown;

	private bool _active = true;

	public void Init(TowerSettings settings, GameManager gameManager)
	{
		_gameManager = gameManager;
		_stats.Init(settings.Range, settings.FireRate, settings.Damage, settings.ProjectileSpeed, settings.AoERadius, settings.CCTime);
		_projectilePrefab = settings.ProjectilePrefab;
	}

	private void Reset()
	{
		_stats = GetComponent<TowerStats>();
	}

	private void Start()
	{
		_enemyUnits = _gameManager.EnemyUnits;
		InvokeRepeating("UpdateTarget", 0f, 0.1f);
	}

	private void Update()
	{
		if (!_active)
			return;

		if (_target == null)
			return;

		UpdateRotation();

		if (_fireCountdown <= 0f)
		{
			Fire();
			_fireCountdown = 1 / _stats.FireRate;
		}
		_fireCountdown -= Time.deltaTime;
	}

	private void UpdateRotation()
	{
		Vector3 direction = _target.transform.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		Vector3 rotation = Quaternion.Lerp(_head.rotation, lookRotation, Time.deltaTime * _turnSpeed).eulerAngles;
		_head.rotation = Quaternion.Euler(0, rotation.y, 0);
	}

	private void Fire()
	{
		Projectile projectileGO = Instantiate(_projectilePrefab, __firePoint.position, __firePoint.rotation);
		projectileGO.Init(_target, _stats.Damage, _stats.ProjectileSpeed, _stats.AoERadius, _stats.CCTime);
	}

	private void UpdateTarget()
	{
		if (_enemyUnits.Count == 0)
			return;

		float shortestDistance = Mathf.Infinity;
		Enemy closestEnemy = null;

		foreach (Enemy e in _enemyUnits)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, e.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				closestEnemy = e;
			}
		}

		if (closestEnemy != null && shortestDistance <= _stats.Range)
		{
			_target = closestEnemy;
		}
		else
		{
			_target = null;
		}
	}

	public void Activate (bool active)
	{
		_active = active;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _stats.Range);
	}

}
