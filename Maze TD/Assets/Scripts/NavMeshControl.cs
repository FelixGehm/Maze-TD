using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshControl : MonoBehaviour
{
	#region Singleton
	public static NavMeshControl Instance
	{
		get; private set;
	}

	private void Awake()
	{
		Instance = this;
	}
	#endregion

	[SerializeField]
	private NavMeshSurface _surface;

	public Action OnNavMeshChanged;

	private void Start()
	{
		_surface.BuildNavMesh();
	}

	public void BakeNavMesh()
	{
		_surface.BuildNavMesh();
		OnNavMeshChanged?.Invoke();
	}
}
