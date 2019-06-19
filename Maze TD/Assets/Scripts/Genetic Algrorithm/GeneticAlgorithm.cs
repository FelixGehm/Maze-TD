using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm : MonoBehaviour
{
	public enum GAState { PlaceTowers, TestFitness, WaitingForResult, NewGeneration, Idle }

	[SerializeField]
	private float _mutationRate;
	[SerializeField]
	private int _populationSize;
	[SerializeField]
	private int _nrOfTowers;

	public Population Population { get; private set; }
	public GAState CurrentState { get; private set; }
	public int PopulationIndex { get; private set; }
	public bool IsRunning { get; private set; }
	public float LastFitness { get; private set; }
	public float CurrentFintess { get; private set; }

	//Wave stuff
	private WaveSettings _waveSettings;
	private Enemy _enemy;



	private void Start()
	{
		CurrentState = GAState.Idle;
		CurrentFintess = 0;
	}

	private void Update()
	{
		switch (CurrentState)
		{
			case GAState.PlaceTowers:
				PlaceTowers();
				break;
			case GAState.TestFitness:
				TestFitness();
				break;
			case GAState.WaitingForResult:
				CheckEnemyPath();
				CurrentFintess = CalcFitness();
				break;
			case GAState.NewGeneration:
				NewGeneration();
				break;
			case GAState.Idle:
				break;
		}
	}

	public void Initilise()
	{
		Population = new Population(_mutationRate, _populationSize, _nrOfTowers);
		CurrentState = GAState.PlaceTowers;

		_waveSettings = Settings.Instance.WaveSettings;
		PopulationIndex = 0;
		IsRunning = true;
	}

	//place the towers based on the genes of the current dna.
	private void PlaceTowers()
	{
		PlaceTowersFromDNA(Population.Pop[PopulationIndex]);
		CurrentState = GAState.TestFitness;
		//CurrentState = GAState.Idle;
	}

	//start a cycle to test the fintess of the current dna.
	private void TestFitness()
	{
		Enemy enemyInstance = Instantiate(_waveSettings.EnemyPrefab, _waveSettings.SpawnPosition, _waveSettings.EnemyPrefab.transform.rotation);
		_enemy = enemyInstance;
		enemyInstance.DestinationReached += FitnessTestFinished;
		GameManager.Instance.RegisterEnemyUnit(enemyInstance);
		enemyInstance.SetDestination(_waveSettings.Destination);
		CurrentState = GAState.WaitingForResult;
	}

	//Set Fitness Value for Current dna and Move on to the next dna or start a new generation if every dna of the population was tested.
	private void FitnessTestFinished()
	{
		float fitness = CalcFitness();
		LastFitness = fitness;
		Population.Pop[PopulationIndex].Fitness = fitness;
		PopulationIndex++;
		ClearField();

		CurrentFintess = 0;

		if (PopulationIndex >= _populationSize - 1)
		{
			CurrentState = GAState.NewGeneration;
			//CurrentState = GAState.Idle;
		}
		else
		{
			CurrentState = GAState.PlaceTowers;
		}
		//CurrentState = GAState.Idle;
	}

	private void NewGeneration()
	{
		PopulationIndex = 0;
		Population.NewGeneration();
		CurrentState = GAState.PlaceTowers;
	}

	private void PlaceTowersFromDNA(DNA dna)
	{
		foreach (Node node in dna.Genes)
		{
			NodeManager.Instance.FillNode(node, Settings.Instance.DefaultTower);
		}
		NavMeshControl.Instance.BakeNavMesh();
	}

	private void ClearField()
	{
		NodeManager.Instance.DepleteAllNodes();
	}

	private void CheckEnemyPath()
	{
		if (_enemy == null)
			return;

		if (!_enemy.HasPath())
		{
			GameManager.Instance.UnregisterEnemyUnit(_enemy);
			Destroy(_enemy.gameObject);
			_enemy = null;
			FitnessTestFinished();
		}
	}

	private float CalcFitness()
	{
		if (_enemy == null)
			return 0;

		return _enemy.Stats.MaxHitpoints - _enemy.Stats.Hitpoints;
	}
}
