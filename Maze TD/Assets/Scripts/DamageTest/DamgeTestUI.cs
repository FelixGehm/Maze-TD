using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamgeTestUI : MonoBehaviour
{
	[SerializeField] private DamageTestSpawner _spawner;
	[SerializeField] private TextMeshProUGUI _cycleTxt;
	[SerializeField] private TextMeshProUGUI _lastDmgTxt;
	[SerializeField] private TextMeshProUGUI _bestDmgTxt;
	[SerializeField] private TextMeshProUGUI _stateTxt;

	private void Update()
	{
		_cycleTxt.text = _spawner.Cycle.ToString();
		_lastDmgTxt.text = _spawner.LastDmg.ToString();
		_bestDmgTxt.text = _spawner.BestDmg.ToString();
		_stateTxt.text = _spawner.CurrentState.ToString();
	}
}
