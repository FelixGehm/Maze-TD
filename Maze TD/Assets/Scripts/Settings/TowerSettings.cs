using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Settings", menuName = "Settings/Tower Settings")]
public class TowerSettings : ScriptableObject
{
	public Tower TowerPrefab;
	public TowerPreview TowerPreviewPrefab;
	public Projectile ProjectilePrefab;

	public float Range;
	public float FireRate;
	public int Damage;
	public float ProjectileSpeed;

	public TowerPreview SpawnPreview(Vector3 position)
	{
		return Instantiate(TowerPreviewPrefab, position, TowerPreviewPrefab.transform.rotation);
	}

	public Tower SpawnTower(Vector3 position, Quaternion rotation)
	{
		Tower towerInstance = Instantiate(TowerPrefab, position, rotation);
		towerInstance.Init(this);
		return towerInstance;
	}
}
