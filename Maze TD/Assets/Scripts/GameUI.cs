using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{

	[Header("Setup Fields")]
	[SerializeField]
	private WaveSpawner _waveSpawner;
	[SerializeField]
	private PlayerStats _playerStats;

	[Header("Setup UI Elements")]
	[SerializeField]
	private TextMeshProUGUI _waveIndexTxt;
	[SerializeField]
	private TextMeshProUGUI _waveCountdownTxt;
	[SerializeField]
	private TextMeshProUGUI _playerHitpointsTxt;
	[SerializeField]
	private TextMeshProUGUI _moneyText;
	[SerializeField]
	private GameObject _buildingPanel;

	public void ToggleBuildingPanel()
	{
		_buildingPanel.SetActive(!_buildingPanel.activeSelf);
	}

	private void Start()
	{
		_waveSpawner.PropertyChanged += WaveSpawner_OnPropertyChanged;
	}

	private void Update()
	{
		_playerHitpointsTxt.text = $"Lifes: {_playerStats.Hitpoints}";
		_moneyText.text = $"Money: {_playerStats.Money}";
	}

	private void OnDestroy()
	{
		_waveSpawner.PropertyChanged -= WaveSpawner_OnPropertyChanged;
	}

	private void WaveSpawner_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "WaveIndex")
			_waveIndexTxt.text = $"Wave: {_waveSpawner.WaveIndex}";

		if (e.PropertyName == "Countdown")
			_waveCountdownTxt.text = $"Next Wave in {(int)_waveSpawner.Countdown}";
	}
}
