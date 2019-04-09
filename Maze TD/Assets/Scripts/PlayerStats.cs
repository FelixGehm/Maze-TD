using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	#region Singleton
	public static PlayerStats Instance
	{
		get; private set;
	}

	private void Awake()
	{
		Instance = this;
	}
	#endregion

	public int Hitpoints { set; get; }
	public int Money { set; get; }

	//TODO: auslagern
	private void Start()
	{
		PlayerSettings settings = Settings.Instance.PlayerSettings;
		Hitpoints = settings.StartHitpoints;
		Money = settings.StartMoney;
	}
}
