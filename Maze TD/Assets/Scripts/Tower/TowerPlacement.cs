using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
	private TowerSettings _towerSettings;
	private TowerPreview _towerPreviewInstance;
	private Collider _currentCollider;
	private Node _currentNode;

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
		NodeManager.Instance.HighlightEmptyNodes(true);
	}

	public void CancelPlacing()
	{
		EndPlacing(false);
	}

	private void Placing()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider.tag == "Node")
			{
				if(hit.collider != _currentCollider)
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
			NodeManager.Instance.FillNode(_currentNode, _towerSettings);
			NavMeshControl.Instance.BakeNavMesh();
		}
		Destroy(_towerPreviewInstance.gameObject);
		_towerPreviewInstance = null;
		NodeManager.Instance.HighlightEmptyNodes(false);
	}

	private void PlaceOnClick()
	{
		if (Input.GetMouseButtonDown(0) && _currentNode != null && _currentNode.IsEmpty)
			EndPlacing(true);
	}
}
