using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm : MonoBehaviour
{
	public enum GAState { PlaceTowers, TestFitness, WaitingForResult, NewGeneration, Idle }

	[SerializeField]
	private Transform _waveStart, _waveEnd;


	[SerializeField]
	private NavMeshControl _navMeshControl;
	[SerializeField]
	private NodeManager _nodeManager;
	[SerializeField]
	private GameManager _gameManager;

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

	private void Reset()
	{
		_navMeshControl = GetComponent<NavMeshControl>();
		_nodeManager = GetComponent<NodeManager>();
	}

	private void Start()
	{
		CurrentState = GAState.Idle;
		CurrentFintess = 0;
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.N))
			Initilise();

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
		Population = new Population(_mutationRate, _populationSize, _nrOfTowers, _nodeManager.Nodes);
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
		Enemy enemyInstance = Instantiate(_waveSettings.EnemyPrefab, _waveStart.position, _waveSettings.EnemyPrefab.transform.rotation);
		enemyInstance.Init(_navMeshControl, _gameManager);
		_enemy = enemyInstance;
		enemyInstance.DestinationReached += FitnessTestFinished;
		_gameManager.RegisterEnemyUnit(enemyInstance);
		enemyInstance.SetDestination(_waveEnd.position);
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
			_nodeManager.FillNode(node, Settings.Instance.DefaultTower, transform);
		}
		_navMeshControl.BakeNavMesh();
	}

	private void ClearField()
	{
		_nodeManager.DepleteAllNodes();
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
