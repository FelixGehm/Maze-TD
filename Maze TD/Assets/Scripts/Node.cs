using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
	[SerializeField]
	private Image _indicatorImg;

	[SerializeField]
	private int _index;

	public int Index { get { return _index; } }

	private void Start()
	{
		NodeManager.Instance.RegisterNode(this);
	}

	public Tower Tower { get; private set; }

	public bool IsEmpty => Tower == null;

	public void SetTower(TowerSettings t)
	{
		if (t == null)
		{
			Tower = null;
			return;
		}

		Tower = t.SpawnTower(transform.position, transform.rotation);
	}

	public void Highlight(bool b, Color c)
	{
		_indicatorImg.color = c;
		_indicatorImg.enabled = b;
	}
}
