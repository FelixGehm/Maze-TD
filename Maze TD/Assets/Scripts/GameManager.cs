using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Singleton and Init
	public static GameManager Instance
	{
		get; private set;
	}

	private void Awake()
	{
		Instance = this;
		EnemyUnits = new List<Enemy>();
	}
	#endregion

	public List<Enemy> EnemyUnits
	{
		get; private set;
	}

	public void RegisterEnemyUnit(Enemy e)
	{
		EnemyUnits.Add(e);
	}
	public void UnregisterEnemyUnit(Enemy e)
	{
		EnemyUnits.Remove(e);
	}

}
