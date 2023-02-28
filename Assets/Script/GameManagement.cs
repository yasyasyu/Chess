using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
	private BoardManagement boardManagement;
	// player -> 0, 1
	private int player;
	// Start is called before the first frame update
	void Start()
	{
		boardManagement = this.GetComponent<BoardManagement>();
		player = 0;
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
				bool pieceCheck = boardManagement.Check(player, index);
				if (!pieceCheck)
				{
					Debug.Log("bad choice");
				}
				else
				{
					Debug.Log("good choice");
					ChangePlayer();
				}
				Debug.Log("Next Player" + player.ToString());
			}
		}
	}

	private void ChangePlayer()
	{
		if (player == 0)
		{
			player = 1;
		}
		else
		{
			player = 0;
		}

		/*
			player = 1 - player;
			player ^= 1
		*/
	}
}
