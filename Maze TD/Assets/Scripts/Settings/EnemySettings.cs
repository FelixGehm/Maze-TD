using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Settings", menuName = "Settings/Enemy Settings")]
public class EnemySettings : ScriptableObject
{
	public float Speed;
	public int MaxHitpoints;
}
