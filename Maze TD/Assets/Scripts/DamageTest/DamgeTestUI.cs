using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamgeTestUI : MonoBehaviour
{
	[SerializeField] private DamageTestSpawner _spawner;
	[SerializeField] private DamageTestManager _manager;
	[SerializeField] private TextMeshProUGUI _cycleTxt;
	[SerializeField] private TextMeshProUGUI _lastDmgTxt;
	[SerializeField] private TextMeshProUGUI _bestDmgTxt;
	[SerializeField] private TextMeshProUGUI _stateTxt;

	[SerializeField] private TextMeshProUGUI _towersPlaced;


	private void Update()
	{
		_cycleTxt.text = $"Tries: {_spawner.Cycle.ToString()}";
		_lastDmgTxt.text = $"Last Dmg: {_spawner.LastDmg.ToString()}";
		_bestDmgTxt.text = $"Best Dmg: {_spawner.BestDmg.ToString()}";
		_stateTxt.text = _spawner.CurrentState.ToString();

		_towersPlaced.text = $"Towers Placed: {_manager.TowersPlaced}/{_manager.AvaiableTowes}";
	}
}
