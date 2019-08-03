using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
	[SerializeField] private PlayerStats _playerStats;

	private void Reset()
	{
		_playerStats = GetComponent<PlayerStats>();
	}

	public void DepleteMoney(int money)
	{
		_playerStats.Money -= money;
	}
}
