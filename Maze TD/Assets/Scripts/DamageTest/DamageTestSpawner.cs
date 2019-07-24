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

	private void CycleFinished()
	{
		CurrentState = State.Waiting;
		LastDmg = _enemy.Stats.MaxHitpoints - _enemy.Stats.Hitpoints;
		if (LastDmg > BestDmg)
			BestDmg = LastDmg;
	}

	private void SpawnEnemy()
	{
		Enemy enemyInstance = Instantiate(_settings.EnemyPrefab, _settings.SpawnPosition, _settings.EnemyPrefab.transform.rotation);
		_enemy = enemyInstance;
		enemyInstance.DestinationReached += CycleFinished;
		_gameManager.RegisterEnemyUnit(enemyInstance);
		enemyInstance.Init(_navMeshControl, _gameManager);
		enemyInstance.SetDestination(_settings.Destination);
	}


}
