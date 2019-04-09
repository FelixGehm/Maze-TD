using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Settings", menuName = "Settings/Wave Settings")]
public class WaveSettings : ScriptableObject
{
	public Enemy EnemyPrefab;
	public Vector3 SpawnPosition;
	public Vector3 Destination;
	public float TimeBetweenSpawns;
	public float TimeBetweenWaves;
   
}
