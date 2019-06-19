using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
	[SerializeField]
	private Color _highlightColor;

	//Singleton
	public static NodeManager Instance
	{
		get; private set;
	}

	private void Awake()
	{
		Instance = this;
		Nodes = new List<Node>();
		FilledNodes = new List<Node>();
	}

	public List<Node> Nodes { get; private set; }
	public List<Node> FilledNodes { get; private set; }

	public void RegisterNode(Node node)
	{
		Nodes.Add(node);
	}

	public void FillNode(Node node, TowerSettings t)
	{
		if (node.Tower != null)
		{
			Debug.LogWarning("Cannot place 2 towers at the same node");
			return;
		}


		node.SetTower(t);
		FilledNodes.Add(node);
	}

	public void DepleteNode(Node node)
	{
		if (node.Tower != null)
			Destroy(node.Tower.gameObject);

		node.SetTower(null);
		FilledNodes.Remove(node);
	}

	public void DepleteAllNodes()
	{
		foreach (Node node in Nodes)
			DepleteNode(node);
	}

	public void HighlightEmptyNodes(bool b)
	{
		foreach (Node n in Nodes)
		{
			if (b && n.IsEmpty)
				n.Highlight(b, _highlightColor);

			if (!b)
				n.Highlight(b, _highlightColor);
		}
	}
}
