using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
	[SerializeField]
	private LayerMask _mask;
	[SerializeField]
	private NavMeshControl _navMeshControl;
	[SerializeField]
	private NodeManager _nodeManager;

	private TowerSettings _towerSettings;
	private TowerPreview _towerPreviewInstance;
	private Collider _currentCollider;
	private Node _currentNode;

	private void Reset()
	{
		_navMeshControl = GetComponent<NavMeshControl>();
		_nodeManager = GetComponent<NodeManager>();
	}

	private void Update()
	{
		if (_towerPreviewInstance == null)
			return;

		Placing();
	}

	public void BeginPlacing(TowerSettings towerSettings)
	{
		_towerSettings = towerSettings;
		_towerPreviewInstance = towerSettings.SpawnPreview(new Vector3(0, 0, 0));
		_nodeManager.HighlightEmptyNodes(true);
	}

	public void CancelPlacing()
	{
		EndPlacing(false);
	}

	private void Placing()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, _mask))
		{
			if (hit.collider.tag == "Node")
			{
				if (hit.collider != _currentCollider)
				{
					_currentCollider = hit.collider;
					_towerPreviewInstance.transform.position = hit.collider.transform.position;
					_currentNode = hit.collider.GetComponent<Node>();
				}
			}
		}
		PlaceOnClick();
	}

	private void EndPlacing(bool place)
	{
		if (place)
		{
			_nodeManager.FillNode(_currentNode, _towerSettings);
			_navMeshControl.BakeNavMesh();
		}
		Destroy(_towerPreviewInstance.gameObject);
		_towerPreviewInstance = null;
		_nodeManager.HighlightEmptyNodes(false);
	}

	private void PlaceOnClick()
	{
		if (Input.GetMouseButtonDown(0) && _currentNode != null && _currentNode.IsEmpty)
			EndPlacing(true);
	}
}
