using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Settings", menuName = "Settings/Player Settings")]
public class PlayerSettings : ScriptableObject
{
	public int StartMoney;
	public int StartHitpoints;
}
