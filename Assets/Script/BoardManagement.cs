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
		//BoardPrint();
		GeneratePiece();
	}

	// Update is called once per frame
	void Update()
	{
		
	}


	void Init(){
		//駒の配置を初期化
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
					piece = Constants.Pieces.SPACE + Constants.Pieces.SPACE + Constants.Pieces.SPACE;
				}

				board[y,x] = piece;
			}
		}
	}

	void GeneratePiece()
	{
		for (int y = 0; y < board.GetLength(0); y++)
		{
			for (int x = 0; x < board.GetLength(1); x++)
			{
				//コマの種類
				GameObject piece = Instantiate(Constants.PieceDictionary.RANK[board[y, x].Substring(1, 1)]);

				GameObject tile = Instantiate(Constants.PieceObject.Tile);

				if (board[y, x].Substring(1, 1) == Constants.Pieces.SPACE)
				{
					piece.GetComponent<Piece>().Init(
							new Vector2Int(x, y)
						);
				}
				else
				{
					piece.GetComponent<Piece>().Init(
						//コマの色
						Constants.PieceDictionary.COLOR[board[y, x].Substring(0, 1)],
						new Vector2Int(x, y)
					);
				}
				tile.GetComponent<Tile>().Init(new Vector2Int(x, y));
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

	private bool CheckKing(Vector2Int frm, Vector2Int to)
	{
		// ###
		// #@#
		// ###
		for (int di = -1; di <= 1; di++)
		{
			for (int dj = -1; dj <= 1; dj++)
			{
				if (frm + new Vector2Int(di, dj) == to)
				{
					return true;
				}
			}
		}

		return false;
	}
	bool Direct(int sign_x, int sign_y, Vector2Int frm, Vector2Int to)
	{
		for (int d = 0; d < 8; d++)
		{
			Vector2Int movePos = frm + new Vector2Int(sign_x * d, sign_y * d);
			if (!(0 <= movePos.x && movePos.x < 8 && 0 <= movePos.y && movePos.y < 8))
			{
				d = 8;
				break;
			}

			if (movePos == to)
			{
				return true;
			}
		}
		return false;
	}
	private bool CheckQueen(Vector2Int frm, Vector2Int to)
	{
		//#  #  #
		// # # # 
		//  ###  
		//###@###
		//  ###  
		// # # # 
		//#  #  #
		bool flg = false;
		flg |= Direct(1, 0, frm, to);
		flg |= Direct(1, 1, frm, to);
		Direct(0, 1);
		Direct(-1, 1);
		Direct(-1, 0);
		Direct(-1, -1);
		Direct(0, -1);
		Direct(1, -1);

		//for (int direct = 0; direct < 8; direct++)
		//{
		//	//direct 0~8
		//	int radian = direct * 45;
		//	int sign_x = (int)Mathf.Cos((float)radian);
		//	int sign_y = (int)Mathf.Sin((float)radian);


		//}

		return false;
	}

	public bool Check(int player, Vector2Int index)
	{
		string piece = board[index.y, index.x];
		if (
			!(
			(player == 1 && piece.Substring(0, 1) == Constants.Pieces.BLACK) ||
			(player == 0 && piece.Substring(0, 1) == Constants.Pieces.WHITE))
			)
		{
			return false;
		}
		switch (piece.Substring(1,1))
		{
			case "K":
				return CheckKing(frm, to);
			case "Q":
				return CheckQueen(frm, to);
			case "R":
				return CheckKing(frm, to);
			case "B":
				return CheckQueen(frm, to);
			case "N":
				return CheckKing(frm, to);
			case "P":
				return CheckQueen(frm, to);

			default:
				return false;
		}
	}
}
