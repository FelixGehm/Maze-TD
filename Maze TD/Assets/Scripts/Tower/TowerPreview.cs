using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPreview : MonoBehaviour
{
	public Action StateChanged;
	private List<Collider> _collisions;


	public bool CanPlace { get; private set; }
	

	private void Start()
	{
		_collisions = new List<Collider>();
		CanPlace = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("trigger enter");
		if (other.tag == "Ground")
			return;

		

		_collisions.Add(other);
		CanPlace = false;
		StateChanged?.Invoke();
	}

	private void OnTriggerExit(Collider other)
	{
		_collisions.Remove(other);
		if (_collisions.Count == 0)
		{
			CanPlace = true;
			StateChanged?.Invoke();
		}	
	}
}
