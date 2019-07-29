using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneticAlgorithmAdvanced
{
	public class GeneticAlgorithmImproved : MonoBehaviour
	{
		public enum GAState { PlaceTowers, WaitingForAgent, TestFitness, WaitingForResult, NewGeneration, Idle }

		[SerializeField] private Element _elementPrefab;

		[SerializeField] private float _mutationRate;
		[SerializeField] private int _populationSize;
		[SerializeField] private int _nrOfTowers;

		[SerializeField]
		private int _nodes = 100;

		public Population Population { get; set; }
		public GAState CurrentState { get; set; }
		public bool IsRunning { get; set; }
		public float BestFitness { get; set; }
		public float AverageFitness { get; set; }
		public int TestsFinished { get; set; }
		public float BestFitnessTime { get; set; }
		public int BestFintessGeneration { get; set; }
		public int AgentsRdy { get; set; }

		private List<Element> _elements;
		private WaveSettings _waveSettings;

		private float _startTime;

		private void Start()
		{
			CurrentState = GAState.Idle;
			AverageFitness = 0;
			TestsFinished = 0;
			SpawnElements(new Vector3(0, 0, 50));
		}

		private void Update()
		{
			if (Input.GetKeyUp(KeyCode.N))
			{
				//Initialise();
				//Debug.Log(Population.Pop[0].Genes.Length);
				foreach (int gene in Population.Pop[0].Genes)
					Debug.Log(gene);
			}



			switch (CurrentState)
			{
				case GAState.PlaceTowers:
					//Debug.Log("place towers");
					PlaceTowers();
					break;
				case GAState.TestFitness:
					//Debug.Log("test fitness");
					TestFitness();
					break;
				case GAState.WaitingForAgent:
					//Debug.Log("place towers");
					WaitingForAgent();
					break;
				case GAState.WaitingForResult:
					//Debug.Log("waiting for result");
					WaitingForResult();
					break;
				case GAState.NewGeneration:
					//Debug.Log("new generation");
					NewGeneration();
					break;
				case GAState.Idle:
					break;
			}
		}

		public void Initialise()
		{
			Population = new Population(_mutationRate, _populationSize, _nrOfTowers, FindNumberOfNodes());
			CurrentState = GAState.PlaceTowers;

			_waveSettings = Settings.Instance.WaveSettings;
			IsRunning = true;
			_startTime = Time.time;
		}

		private void SpawnElements(Vector3 offset)
		{
			_elements = new List<Element>();
			Vector3 spawnPositon = new Vector3(0, 0, 0);
			for (int i = 0; i < _populationSize; i++)
			{
				Element e = Instantiate(_elementPrefab, spawnPositon, _elementPrefab.transform.rotation);
				e.Init(this);
				_elements.Add(e);
				spawnPositon += offset;
			}
		}

		private void PlaceTowers()
		{
			for (int i = 0; i < _populationSize; i++)
			{
				_elements[i].PlaceTowers(Population.Pop[i].Genes);
			}
			CurrentState = GAState.TestFitness;
		}		

		private void TestFitness()
		{
			foreach (Element e in _elements)
				e.TestFitness();
			AgentsRdy = 0;
			CurrentState = GAState.WaitingForAgent;
		}

		private void WaitingForAgent()
		{
			if(AgentsRdy >= _populationSize)
			{
				foreach (var e in _elements)
				{
					e.EnemyInstance.Unfreeze();
					e.ActivateTowers(true);
				}
				CurrentState = GAState.WaitingForResult;
			}

			//Debug.Log(AgentsRdy);
		}

		private void WaitingForResult()
		{
			if (TestsFinished == _populationSize)
			{
				float fitnessSum = 0;
				for (int i = 0; i < _populationSize; i++)
				{
					float fitness = _elements[i].Fitness;
					Population.Pop[i].Fitness = fitness;
					if (fitness > BestFitness)
					{
						BestFitness = fitness;
						BestFintessGeneration = Population.Generation;
						BestFitnessTime = Time.time - _startTime;
					}

					fitnessSum += fitness;
				}
				AverageFitness = fitnessSum / _populationSize;
				CurrentState = GAState.NewGeneration;
			}
		}

		private void NewGeneration()
		{
			TestsFinished = 0;
			Population.NewGeneration();
			CurrentState = GAState.PlaceTowers;
		}

		private int FindNumberOfNodes()
		{
			return _nodes;
		}
	}
}

