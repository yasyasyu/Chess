using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
	private BoardManagement boardManagement;
	// player -> 0, 1
	private int player;

	private Vector2Int pieceIndex = new Vector2Int(-1, -1);
	private Vector2Int moveIndex = new Vector2Int(-1, -1);

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
				if (pieceIndex == new Vector2Int(-1, -1))
				{
					pieceIndex = hitInfo.collider.gameObject.GetComponent<Piece>().Select();

					if (!boardManagement.PlayerCheck(player, pieceIndex))
					{
						Debug.Log("bad choice PlayerCheck");
						ResetIndex(0);
					}
					else
					{
						Debug.Log("good choice PlayerCheck");
					}
				}
				else if(moveIndex == new Vector2Int(-1, -1))
				{
					moveIndex = hitInfo.collider.gameObject.GetComponent<Piece>().Select();

					if (!boardManagement.MoveCheck(pieceIndex, moveIndex))
					{
						Debug.Log("bad choice MoveCheck");
						ResetIndex(1);
					}
					else
					{
						Debug.Log("good choice MoveCheck");
						boardManagement.Move(pieceIndex, moveIndex);
						ResetIndex();
						ChangePlayer();

					}
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
	public void ResetIndex(int _case)
	{
		switch (_case)
		{
			case 0:
				pieceIndex = new Vector2Int(-1, -1);
				break;
			case 1:
				moveIndex = new Vector2Int(-1, -1);
				break;
			default:
				break;
		}
	}
	private void ResetIndex()
	{
		ResetIndex(0);
		ResetIndex(1);
	}
}
