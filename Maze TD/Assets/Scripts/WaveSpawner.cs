using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class WaveSpawner : MonoBehaviour, INotifyPropertyChanged
{
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
		GameManager.Instance.RegisterEnemyUnit(enemyInstance);
		enemyInstance.SetDestination(_settings.Destination);
	}

	private void OnPropertyChanged(string name)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}
