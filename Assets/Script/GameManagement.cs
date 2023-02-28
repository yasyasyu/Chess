using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
	private BoardManagement boardManagement;
	// Start is called before the first frame update
	void Start()
	{
		//boardManagement = 
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out RaycastHit hitInfo))
			{
				Vector2Int index = hitInfo.collider.gameObject.GetComponent<Piece>().Select();
				Vector3 coordinate = hitInfo.collider.transform.position;
				Debug.Log("GM"+index.ToString()+":"+coordinate.ToString());
				//boardManagement.Check();
			}
		}
	}
}
