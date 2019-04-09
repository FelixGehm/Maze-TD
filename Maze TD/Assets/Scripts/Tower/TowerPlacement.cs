using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
	private TowerSettings _towerSettings;
	private TowerPreview _towerPreviewInstance;

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
			if (hit.collider.tag == "Ground")
			{
				_towerPreviewInstance.transform.position = hit.point;
				PlaceOnClick();
			}
		}
	}

	private void EndPlacing(bool place)
	{
		if (place)
		{
			_towerSettings.SpawnTower(_towerPreviewInstance.transform.position, _towerPreviewInstance.transform.rotation);
			NavMeshControl.Instance.BakeNavMesh();
		}
		Destroy(_towerPreviewInstance.gameObject);
		_towerPreviewInstance = null;
	}

	private void PlaceOnClick()
	{
		if (Input.GetMouseButtonDown(0) && _towerPreviewInstance.CanPlace)
			EndPlacing(true);
	}
}
