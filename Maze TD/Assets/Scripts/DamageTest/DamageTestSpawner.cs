using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DamageTestSpawner : MonoBehaviour
{
	public enum State { Running, Waiting, Starting }

	[SerializeField]
	private NavMeshControl _navMeshControl;
	[SerializeField]
	private GameManager _gameManager;

	[SerializeField] private Transform _waveStart, _waveEnd;

	private WaveSettings _settings;

	private Enemy _enemy;

	public State CurrentState { get; private set; }
	public int Cycle { get; private set; }
	public int BestDmg { get; private set; }
	public int LastDmg { get; private set; }

	private void Reset()
	{
		_navMeshControl = GetComponent<NavMeshControl>();
		_gameManager = GetComponent<GameManager>();
	}

	private void Start()
	{
		CurrentState = State.Waiting;
		_settings = Settings.Instance.WaveSettings;
		Cycle = 0;
		BestDmg = 0;
		LastDmg = 0;
	}

	private void Update()
	{
		switch (CurrentState)
		{
			case State.Running:
				CheckEnemyPath();
				break;
			case State.Waiting:
				if (Input.GetKeyUp(KeyCode.N))
					CurrentState = State.Starting;
				break;
			case State.Starting:
				SpawnEnemy();
				Cycle++;
				CurrentState = State.Running;
				break;
		}
	}

	public void Initialise()
	{
		if (!(CurrentState == State.Waiting))
			return;

		CurrentState = State.Starting;
	}

	private void CycleFinished()
	{
		CurrentState = State.Waiting;
		LastDmg = _enemy.Stats.MaxHitpoints - _enemy.Stats.Hitpoints;
		if (LastDmg > BestDmg)
			BestDmg = LastDmg;
	}

	private void SpawnEnemy()
	{
		Enemy enemyInstance = Instantiate(_settings.EnemyPrefab, _waveStart.position, _settings.EnemyPrefab.transform.rotation);
		_enemy = enemyInstance;
		enemyInstance.DestinationReached += CycleFinished;
		_gameManager.RegisterEnemyUnit(enemyInstance);
		enemyInstance.Init(_navMeshControl, _gameManager);
		enemyInstance.SetDestination(_waveEnd.position);
	}

	private void CheckEnemyPath()
	{
		if (_enemy == null)
			return;

		if (!_enemy.HasPath())
		{
			_gameManager.UnregisterEnemyUnit(_enemy);
			Destroy(_enemy.gameObject);
			_enemy = null;
			CurrentState = State.Waiting;
			LastDmg = 0;
		}
	}
}
