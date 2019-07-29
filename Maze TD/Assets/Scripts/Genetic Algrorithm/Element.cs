using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneticAlgorithmAdvanced
{
	public class Element : MonoBehaviour
	{
		[SerializeField]
		private Transform _waveStart, _waveEnd;
		[SerializeField]
		private Transform _towerParent;


		[SerializeField]
		private NavMeshControl _navMeshControl;
		[SerializeField]
		private NodeManager _nodeManager;
		[SerializeField]
		private GameManager _gameManager;

		GeneticAlgorithmImproved _ga;
		private WaveSettings _waveSettings;
		private Enemy _enemyInstance;
		private List<Tower> _towers;
		private bool _agentRdy = false;

		public float Fitness { set; get; }
		public Enemy EnemyInstance { get { return _enemyInstance; } }

		private bool _isTesting = false;

		private int _framesStuck = 0;

		public void Init(GeneticAlgorithmImproved ga)
		{
			_ga = ga;
		}

		private void Start()
		{
			_waveSettings = Settings.Instance.WaveSettings;
			_towers = new List<Tower>();
		}

		private void Update()
		{
			if (_ga.CurrentState == GeneticAlgorithmImproved.GAState.WaitingForAgent)
			{
				if (_enemyInstance != null && _enemyInstance.HasPath() && !_agentRdy)
				{
					_ga.AgentsRdy++;
					_agentRdy = true;
				}

			}

			if (_isTesting && _ga.CurrentState == GeneticAlgorithmImproved.GAState.WaitingForResult)
			{
				CheckEnemyPath();
				//if (_framesStuck >= 200)
				//	CancelTest();
			}


			if (_ga.CurrentState == GeneticAlgorithmImproved.GAState.WaitingForResult)
			{
				_isTesting = true;
			}
			else
			{
				_isTesting = false;
				_framesStuck = 0;
			}
		}


		public void PlaceTowers(int[] genes)
		{
			_towers.Clear();
			foreach (Node node in _nodeManager.Nodes)
			{
				foreach (int g in genes)
				{
					if (node.Index == g)
					{
						_nodeManager.FillNode(node, Settings.Instance.DefaultTower, _towerParent);
						_towers.Add(node.Tower);
					}
				}
			}
			ActivateTowers(false);
			_navMeshControl.BakeNavMesh();
			_agentRdy = false;
		}

		public void TestFitness()
		{
			_enemyInstance = Instantiate(_waveSettings.EnemyPrefab, _waveStart.position, _waveSettings.EnemyPrefab.transform.rotation);
			_enemyInstance.Init(_navMeshControl, _gameManager);
			_enemyInstance.DestinationReached += FitnessTestFinished;
			_gameManager.RegisterEnemyUnit(_enemyInstance);
			_enemyInstance.SetDestination(_waveEnd.position);
			_enemyInstance.Freeze();
		}

		private void FitnessTestFinished()
		{
			_isTesting = false;
			Fitness = CalcFitness();
			ClearField();
			_ga.TestsFinished++;
		}


		//unities method HasPath() sometimes returns false while the agent actualy has a path. so i only cancel the test if the agent is stuck for multiple frames.
		private void CheckEnemyPath()
		{
			//if (_enemyInstance == null)
			//	return;

			//if (!_enemyInstance.HasPath())
			//{
			//	_framesStuck++;
			//}

			if (_enemyInstance == null)
				return;

			if (!_enemyInstance.HasPath())
			{
				Debug.Log("test canceled");
				_gameManager.UnregisterEnemyUnit(_enemyInstance);
				Destroy(_enemyInstance.gameObject);
				_enemyInstance = null;
				FitnessTestFinished();
			}
		}

		private void CancelTest()
		{
			if (_enemyInstance == null)
				return;

			Debug.Log("test canceled");
			_gameManager.UnregisterEnemyUnit(_enemyInstance);
			Destroy(_enemyInstance.gameObject);
			_enemyInstance = null;
			FitnessTestFinished();
		}

		private float CalcFitness()
		{
			_isTesting = false;
			if (_enemyInstance == null)
				return 0;

			return _enemyInstance.Stats.MaxHitpoints - _enemyInstance.Stats.Hitpoints;
		}

		private void ClearField()
		{
			_nodeManager.DepleteAllNodes();
		}
		public void ActivateTowers(bool b)
		{
			foreach (var t in _towers)
				t.Activate(b);
		}
	}
}

