using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour, INotifyPropertyChanged
{
	[SerializeField]
	private NavMeshControl _navMeshControl;
	[SerializeField]
	private GameManager _gameManager;
	[SerializeField]
	private PlayerStats _playerStats;
	[SerializeField]
	private MoneyManager _moneyManager;

	private WaveSettings _settings;
	private int nrOfEnemies = 0;

	private int _waveIndex;
	private float _countdown;

	public event PropertyChangedEventHandler PropertyChanged;

	public int WaveIndex
	{
		get
		{
			return _waveIndex;
		}
		private set
		{
			_waveIndex = value;
			OnPropertyChanged("WaveIndex");
		}
	}
	public float Countdown
	{
		get
		{
			return _countdown;
		}
		private set
		{
			_countdown = value;
			OnPropertyChanged("Countdown");
		}
	}

	private void Reset()
	{
		_navMeshControl = GetComponent<NavMeshControl>();
		_gameManager = GetComponent<GameManager>();
	}

	private void Start()
	{
		_settings = Settings.Instance.WaveSettings;
	}

	private void Update()
	{
		if (Countdown <= 0)
		{
			StartCoroutine(SpawnWave());
			Countdown = _settings.TimeBetweenWaves;
		}

		Countdown -= Time.deltaTime;

		if (Input.GetKey(KeyCode.Escape))
			SceneManager.LoadScene("MainMenu");

		if (_playerStats.Hitpoints <= 0)
			SceneManager.LoadScene("MainMenu");
	}

	private IEnumerator SpawnWave()
	{
		nrOfEnemies++;
		WaveIndex++;
		for (int i = 0; i < nrOfEnemies; i++)
		{
			SpawnEnemy();
			yield return new WaitForSeconds(_settings.TimeBetweenSpawns);
		}
	}

	private void SpawnEnemy()
	{
		Enemy enemyInstance = Instantiate(_settings.EnemyPrefab, _settings.SpawnPosition, _settings.EnemyPrefab.transform.rotation);
		_gameManager.RegisterEnemyUnit(enemyInstance);
		enemyInstance.Init(_navMeshControl, _gameManager);
		enemyInstance.SetDestination(_settings.Destination);
		enemyInstance.DestinationReached += DamagePlayer;
		enemyInstance.OnDie += GetMoney;
	}

	private void OnPropertyChanged(string name)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}

	private void DamagePlayer()
	{
		_playerStats.Hitpoints--;
	}

	private void GetMoney()
	{
		_playerStats.Money++;
	}
}
