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

	[SerializeField]
	private NodeManager _nodeManager;
	[SerializeField]
	private GameManager _gameManager;

	public int Index { get { return _index; } }

	private void Start()
	{
		_nodeManager.RegisterNode(this);
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

		Tower = t.SpawnTower(transform.position, transform.rotation, _gameManager);
	}

	public void SetTower(TowerSettings t, Transform parent)
	{
		if (t == null)
		{
			Tower = null;
			return;
		}

		Tower = t.SpawnTower(transform.position, transform.rotation, _gameManager, parent);
	}

	public void Highlight(bool b, Color c)
	{
		_indicatorImg.color = c;
		_indicatorImg.enabled = b;
	}
}
