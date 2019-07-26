using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageTestManager : MonoBehaviour
{
	[SerializeField] private TowerPlacement _towerPlacement;
	[SerializeField] private GameObject _buildingPanel;
	[SerializeField] private int _availableTowers;
	[SerializeField] private NodeManager _nodeManager;

	public int TowersPlaced { get; private set; }
	public int AvaiableTowes { get { return _availableTowers; } }

	private void Reset()
	{
		_towerPlacement = GetComponent<TowerPlacement>();
		_nodeManager = GetComponent<NodeManager>();
		_availableTowers = 5;
	}

	private void Start()
	{
		_towerPlacement.OnTowerPlaced += OnTowerPlaced;
	}

	private void Update()
	{
		_buildingPanel.SetActive(TowersPlaced < _availableTowers);

		if (Input.GetKey(KeyCode.Escape))
			SceneManager.LoadScene("DamageTestMenu");
	}

	private void OnTowerPlaced()
	{
		TowersPlaced += 1;
	}

	public void ClearField()
	{
		TowersPlaced = 0;
		_nodeManager.DepleteAllNodes();
	}
}
