using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
	#region Singleton
	public static Settings Instance
	{
		get; private set;
	}

	private void Awake()
	{
		Instance = this;
	}
	#endregion

	public EnemySettings EnemySettings;
	public PlayerSettings PlayerSettings;
	public WaveSettings WaveSettings;
	public TowerSettings DefaultTower;
}
