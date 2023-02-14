using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManagement : MonoBehaviour
{
	readonly string[] defaultPieces = new string[8] {
			Constants.Pieces.ROOK,
			Constants.Pieces.KNIGHT,
			Constants.Pieces.BISHOP,
			Constants.Pieces.KING,
			Constants.Pieces.QUEEN,
			Constants.Pieces.BISHOP,
			Constants.Pieces.KNIGHT,
			Constants.Pieces.ROOK
		};

	string[,] board = new string[8,8];
	// Start is called before the first frame update
	void Start()
	{
		Init();
		BoardPrint();
		generatePiece();
	}

	// Update is called once per frame
	void Update()
	{
		
	}


	void Init(){
		//ãÓÇÃîzíuÇèâä˙âª
		for (int y = 0; y < board.GetLength(0); y++)
		{
			for (int x = 0; x < board.GetLength(1); x++)
			{
				string piece;

				string color;
				if (y < board.GetLength(0)/2 )
				{
					color = Constants.Pieces.BLACK;
				}
				else
				{
					color = Constants.Pieces.WHITE;
				}

				if (y == 0 || y == 7)
				{
					int d = 0;
					if (y == 7)
					{
						d = 7;
					}

					string pieceNum = " ";
					if (x < board.GetLength(1) / 2 - 1)
					{
						pieceNum = "1";
					}
					else if (board.GetLength(1) / 2 + 1 <= x)
					{
						pieceNum = "2";
					}
					piece = color + defaultPieces[(int)Mathf.Abs(d - x)] + pieceNum;
				}
				else if (y == 1 || y == 6)
				{
					piece = color + Constants.Pieces.PAWN + (x + 1).ToString();
				}
				else {
					piece = color + Constants.Pieces.SPACE + Constants.Pieces.SPACE;
				}

				board[y,x] = piece;
			}
		}
	}

	void generatePiece()
	{
		for (int y = 0; y < board.GetLength(0); y++)
		{
			for (int x = 0; x < board.GetLength(1); x++)
			{
				if(board[y,x].Substring(1,2) != Constants.Pieces.SPACE + Constants.Pieces.SPACE){

					GameObject piece = Instantiate(Constants.PieceDictionary.RANK[board[y, x].Substring(1, 1)]);
					piece.GetComponent<Piece>().Init(
							Constants.PieceDictionary.COLOR[board[y, x].Substring(0, 1)],
							new Vector2Int(x, y)
						);
				}
			}
		}
	}

	void BoardPrint(){
		for(int y=0; y < board.GetLength(0); y++)
		{
			string printString = "";
			for(int x = 0; x < board.GetLength(1); x++)
			{
				printString += board[y, x] + "|";
			}
			Debug.Log(printString);
		}
	}
}
