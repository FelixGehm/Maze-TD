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

	private NavMeshControl _navMeshControl;
	private GameManager _gameManager;

	private Vector3 _destination;
	private bool _hasDestination;
	private bool _isCC = false;
	private bool _isFrozen = false;

	public Action DestinationReached;

	public EnemyStats Stats
	{
		get { return _stats; }
	}

	public void Init(NavMeshControl navMeshControl, GameManager gameManager)
	{
		_navMeshControl = navMeshControl;
		_gameManager = gameManager;
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

	public void Slow(float slowPercent, float slowTime)
	{
		if (_isCC)
			return;

		float defaultSpeed = _stats.Speed;
		_stats.Speed = defaultSpeed * (1 - slowPercent);
		_agent.speed = _stats.Speed;                    //TODO: agent speed automatisch mit stats synchronisieren
		_isCC = true;
		StartCoroutine(ResetSpeed(defaultSpeed, slowTime));
	}

	private IEnumerator ResetSpeed(float speed, float slowTime)
	{
		yield return new WaitForSeconds(slowTime);
		_stats.Speed = speed;
		_agent.speed = _stats.Speed;
		_isCC = false;
	}

	public bool HasPath()
	{
		return _agent.hasPath;
	}

	public void Freeze()
	{
		_agent.speed = 0;
		_isFrozen = true;
	}

	public void Unfreeze()
	{
		_agent.speed = _stats.Speed;
		_isFrozen = false;
	}

	private void Reset()
	{
		_stats = GetComponent<EnemyStats>();
		_agent = GetComponent<NavMeshAgent>();
	}

	private void Start()
	{
		_navMeshControl.OnNavMeshChanged += UpdateDestination;
		_agent.speed = _stats.Speed;
		if (_isFrozen)
			_agent.speed = 0;
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
		_navMeshControl.OnNavMeshChanged -= UpdateDestination;
		_gameManager.UnregisterEnemyUnit(this);
	}

	private void OnDestinationReached()
	{
		DestinationReached?.Invoke();
		//PlayerStats.Instance.Hitpoints--;
		Destroy(gameObject);
	}

	private void UpdateDestination()
	{
		_agent.SetDestination(_destination);
	}


}
