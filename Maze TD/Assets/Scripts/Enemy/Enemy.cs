using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(EnemyStats))]
public class Enemy : MonoBehaviour
{
	[Header("Setup Fields")]
	[SerializeField]
	private EnemyStats _stats;
	[SerializeField]
	private NavMeshAgent _agent;

	private Vector3 _destination;
	private bool _hasDestination;

	public Action DestinationReached;

	public EnemyStats Stats
	{
		get { return _stats; }
	}

	public void SetDestination(Vector3 destination)
	{
		_hasDestination = true;
		_destination = destination;
		_agent.SetDestination(destination);
	}

	public void TakeDamage(int dmg)
	{
		_stats.TakeDamage(dmg);
	}

	public bool HasPath()
	{
		return _agent.hasPath;
	}

	private void Reset()
	{
		_stats = GetComponent<EnemyStats>();
		_agent = GetComponent<NavMeshAgent>();
	}

	private void Start()
	{
		NavMeshControl.Instance.OnNavMeshChanged += UpdateDestination;
		_agent.speed = _stats.Speed;
	}

	private void Update()
	{
		//Stattdessen Endzone mit OnTriggerEnter?
		if (transform.position.z <= _destination.z + 0.2f)
		{
			OnDestinationReached();
		}
	}

	private void OnDestroy()
	{
		NavMeshControl.Instance.OnNavMeshChanged -= UpdateDestination;
		GameManager.Instance.UnregisterEnemyUnit(this);
	}

	private void OnDestinationReached()
	{
		DestinationReached?.Invoke();
		PlayerStats.Instance.Hitpoints--;
		Destroy(gameObject);
	}

	private void UpdateDestination()
	{
		_agent.SetDestination(_destination);
	}


}
