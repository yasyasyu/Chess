using System;
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

	string[,] board = new string[8, 8];
	// Start is called before the first frame update
	void Start()
	{
		Init();
		BoardPrint();
		GeneratePiece();
	}

	// Update is called once per frame
	void Update()
	{

	}


	void Init()
	{
		//駒の配置を初期化
		for (int y = 0; y < board.GetLength(0); y++)
		{
			for (int x = 0; x < board.GetLength(1); x++)
			{
				string piece;

				string color;
				if (y < board.GetLength(0) / 2)
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
				else
				{
					piece = Constants.Pieces.SPACE + Constants.Pieces.SPACE + Constants.Pieces.SPACE;
				}

				board[y, x] = piece;
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
				GameObject piece = Instantiate(Constants.PieceDictionary.RANK[GetRank(board[y, x])]);

				GameObject tile = Instantiate(Constants.PieceObject.Tile);

				if (GetRank(board[y, x]) == Constants.Pieces.SPACE)
				{
					piece.GetComponent<Piece>().Init(
							new Vector2Int(x, y)
						);
				}
				else
				{
					piece.GetComponent<Piece>().Init(
						//コマの色
						Constants.PieceDictionary.COLOR[GetColor(board[y, x])],
						new Vector2Int(x, y)
					);
				}
				tile.GetComponent<Tile>().Init(new Vector2Int(x, y));
			}
		}
	}

	void BoardPrint()
	{
		for (int y = 0; y < board.GetLength(0); y++)
		{
			string printString = "";
			for (int x = 0; x < board.GetLength(1); x++)
			{
				printString += board[y, x] + "|";
			}
			Debug.Log(printString);
		}
	}
	private string GetColor(string piece)
	{
		return piece.Substring(0, 1);
	}

	private string GetRank(string piece)
	{
		return piece.Substring(1, 1);
	}

	private int GetCount(string piece)
	{
		return int.Parse(piece.Substring(2, 1));
	}

	public bool PlayerCheck(int player, Vector2Int index)
	{
		string piece = board[index.y, index.x];
		if (
			!(
			(player == 1 && GetColor(piece) == Constants.Pieces.BLACK) ||
			(player == 0 && GetColor(piece) == Constants.Pieces.WHITE))
			)
		{
			return false;
		}
		return true;
	}

	private bool CheckKing(Vector2Int frm, Vector2Int to)
	{
		/*
			###
			#@#
			###
		*/
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
	private bool CheckKing(Vector2Int frm)
	{
		/*
			###
			#@#
			###
		*/
		for (int di = -1; di <= 1; di++)
		{
			for (int dj = -1; dj <= 1; dj++)
			{
				Vector2Int to = frm + new Vector2Int(di, dj);
				if (GetRank(board[to.y, to.x]) == Constants.Pieces.WHITE || GetColor(board[to.y, to.x]) != GetColor(board[frm.y, frm.x]))
				{
					return true;
				}
			}
		}

		return false;
	}

	private bool Direct(int sign_x, int sign_y, Vector2Int frm, Vector2Int to)
	{
		for (int d = 1; d < 8; d++)
		{
			Vector2Int movePos = frm + new Vector2Int(sign_x * d, sign_y * d);
			if (!(0 <= movePos.x && movePos.x < 8 && 0 <= movePos.y && movePos.y < 8))
			{
				return false;
			}

			string piece = board[movePos.y, movePos.x];
			if (movePos == to)
			{
				return true;
			}
			else if (GetRank(piece) != Constants.Pieces.SPACE)
			{
				return false;
			}
		}
		return false;
	}
	private bool Direct(int sign_x, int sign_y, Vector2Int frm)
	{
		for (int d = 1; d < 8; d++)
		{
			Vector2Int movePos = frm + new Vector2Int(sign_x * d, sign_y * d);
			if (!(0 <= movePos.x && movePos.x < 8 && 0 <= movePos.y && movePos.y < 8))
			{
				return false;
			}

			string piece = board[movePos.y, movePos.x];
			if (GetRank(piece) == Constants.Pieces.WHITE || GetColor(piece) != GetColor(board[frm.y, frm.x]))
			{
				return true;
			}
			else if (GetRank(piece) != Constants.Pieces.SPACE)
			{
				return false;
			}
		}
		return false;
	}

	private bool CheckQueen(Vector2Int frm, Vector2Int to)
	{
		/*
			#   #   #
			 #  #  # 
			  # # #  
			   ###   
			####@####
			   ###   
			  # # #  
			 #  #  # 
			#   #   #
		*/

		bool flg = false;

		/*
			flg |= Direct( 1,  0, frm, to);
			flg |= Direct( 1,  1, frm, to);
			flg |= Direct( 0,  1, frm, to);
			flg |= Direct(-1,  1, frm, to);
			flg |= Direct(-1,  0, frm, to);
			flg |= Direct(-1, -1, frm, to);
			flg |= Direct( 0, -1, frm, to);
			flg |= Direct( 1, -1, frm, to);
		*/

		for (int direct = 0; direct < 8; direct++)
		{
			//direct 0~7
			int radian = direct * 45;
			//Debug.Log(radian);
			int sign_x = (int)Math.Round(Math.Cos(radian * (Math.PI / 180)));
			int sign_y = (int)Math.Round(Math.Sin(radian * (Math.PI / 180)));
			//Debug.Log(sign_x.ToString() + ", " + sign_y.ToString());
			flg |= Direct(sign_x, sign_y, frm, to);
		}

		return flg;
	}

	private bool CheckQueen(Vector2Int frm)
	{
		/*
			#   #   #
			 #  #  # 
			  # # #  
			   ###   
			####@####
			   ###   
			  # # #  
			 #  #  # 
			#   #   #
		*/

		bool flg = false;
		for (int direct = 0; direct < 8; direct++)
		{
			//direct 0~7
			int radian = direct * 45;
			int sign_x = (int)Math.Round(Math.Cos(radian * (Math.PI / 180)));
			int sign_y = (int)Math.Round(Math.Sin(radian * (Math.PI / 180)));

			flg |= Direct(sign_x, sign_y, frm);
		}

		return flg;
	}

	private bool CheckRook(Vector2Int frm, Vector2Int to)
	{
		/*
			    #    
			    #    
			    #    
			    #    
			####@####
			    #    
			    #    
			    #    
			    #    
		*/

		bool flg = false;

		for (int direct = 0; direct < 4; direct++)
		{
			//direct 0~7
			int radian = direct * 90;
			//Debug.Log(radian);
			int sign_x = (int)Math.Round(Math.Cos(radian * (Math.PI / 180)));
			int sign_y = (int)Math.Round(Math.Sin(radian * (Math.PI / 180)));
			//Debug.Log(sign_x.ToString() + ", " + sign_y.ToString());
			flg |= Direct(sign_x, sign_y, frm, to);
		}

		return flg;
	}
	private bool CheckRook(Vector2Int frm)
	{
		/*
			    #    
			    #    
			    #    
			    #    
			####@####
			    #    
			    #    
			    #    
			    #    
		*/

		bool flg = false;

		for (int direct = 0; direct < 4; direct++)
		{
			//direct 0~7
			int radian = direct * 90;
			//Debug.Log(radian);
			int sign_x = (int)Math.Round(Math.Cos(radian * (Math.PI / 180)));
			int sign_y = (int)Math.Round(Math.Sin(radian * (Math.PI / 180)));
			//Debug.Log(sign_x.ToString() + ", " + sign_y.ToString());
			flg |= Direct(sign_x, sign_y, frm);
		}

		return flg;
	}

	private bool CheckBishop(Vector2Int frm, Vector2Int to)
	{
		/*
			#       #
			 #     # 
			  #   #  
			   # #   
			    @    
			   # #   
			  #   #  
			 #     # 
			#       #
		*/

		bool flg = false;

		for (int direct = 0; direct < 4; direct++)
		{
			//direct 0~7
			int radian = direct * 90 + 45;
			//Debug.Log(radian);
			int sign_x = (int)Math.Round(Math.Cos(radian * (Math.PI / 180)));
			int sign_y = (int)Math.Round(Math.Sin(radian * (Math.PI / 180)));
			//Debug.Log(sign_x.ToString() + ", " + sign_y.ToString());
			flg |= Direct(sign_x, sign_y, frm, to);
		}

		return flg;
	}
	private bool CheckBishop(Vector2Int frm)
	{
		/*
			#       #
			 #     # 
			  #   #  
			   # #   
			    @    
			   # #   
			  #   #  
			 #     # 
			#       #
		*/

		bool flg = false;

		for (int direct = 0; direct < 4; direct++)
		{
			//direct 0~7
			int radian = direct * 90 + 45;
			//Debug.Log(radian);
			int sign_x = (int)Math.Round(Math.Cos(radian * (Math.PI / 180)));
			int sign_y = (int)Math.Round(Math.Sin(radian * (Math.PI / 180)));
			//Debug.Log(sign_x.ToString() + ", " + sign_y.ToString());
			flg |= Direct(sign_x, sign_y, frm);
		}

		return flg;
	}

	private bool CheckKnight(Vector2Int frm, Vector2Int to)
	{
		/*
			 # # 
			#   #
			  @  
			#   #
			 # # 
		*/

		bool flg = false;

		for (int dx = 1; dx <= 2; dx++)
		{
			int dy = 3 - dx;
			//flg |= frm + new Vector2Int( dx, dy) == to;
			//flg |= frm + new Vector2Int(-dx, dy) == to;
			//flg |= frm + new Vector2Int( dx,-dy) == to;
			//flg |= frm + new Vector2Int(-dx,-dy) == to;

			for (int sign = 0; sign < 1 << 2; sign++)
			{
				int signX = 1, signY = 1;
				for (int shift = 0; shift < 2; shift++)
				{
					if ((sign >> shift & 1) == 1)
					{
						if (shift % 2 == 0)
						{
							signX = -1;
						}
						else
						{
							signY = -1;
						}
					}
				}
				flg |= frm + new Vector2Int(signX * dx, signY * dy) == to;
			}
		}
		return flg;
	}
	private bool CheckKnight(Vector2Int frm)
	{
		/*
			 # # 
			#   #
			  @  
			#   #
			 # # 
		*/

		bool flg = false;

		for (int dx = 1; dx <= 2; dx++)
		{
			int dy = 3 - dx;

			for (int sign = 0; sign < 1 << 2; sign++)
			{
				int signX = 1, signY = 1;
				for (int shift = 0; shift < 2; shift++)
				{
					if ((sign >> shift & 1) == 1)
					{
						if (shift % 2 == 0)
						{
							signX = -1;
						}
						else
						{
							signY = -1;
						}
					}
				}
				Vector2Int to = frm + new Vector2Int(signX * dx, signY * dy);
				if (!(0 <= to.x && to.x < 8 && 0 <= to.y && to.y < 8))
				{
					continue;
				}

				flg |= GetColor(board[frm.y, frm.x]) != GetColor(board[to.y, to.x]);
			}
		}
		return flg;
	}
	private bool CheckPawn(Vector2Int frm, Vector2Int to)
	{
		/*
			#	@
			@	#
		 */
		string color = board[frm.y, frm.x].Substring(0, 1);
		bool flg = false;
		int direct = 1;
		if (color == Constants.Pieces.WHITE)
		{
			direct *= -1;
		}
		if (frm + new Vector2Int(0, direct) == to)
		{
			flg = true;
		}
		// TODO add Pawn move
		return flg;
	}
	private bool CheckPawn(Vector2Int frm)
	{
		/*
			#	@
			@	#
		 */

		bool flg = false;
		int direct = 1;
		if (GetColor(board[frm.y, frm.x]) == Constants.Pieces.WHITE)
		{
			direct *= -1;
		}

		Vector2Int to = frm + new Vector2Int(0, direct);
		if (GetRank(board[to.y, to.x]) == Constants.Pieces.SPACE || GetColor(board[to.y, to.x]) != GetColor(board[frm.y, frm.x]))
		{
			flg = true;
		}
		// TODO add Pawn move
		return flg;
	}

	public bool Movable(Vector2Int frm)
	{
		string piece = board[frm.y, frm.x];
		switch (piece.Substring(1, 1))
		{
			case "K":
				return CheckKing(frm);
			case "Q":
				return CheckQueen(frm);
			case "R":
				return CheckRook(frm);
			case "B":
				return CheckBishop(frm);
			case "N":
				return CheckKnight(frm);
			case "P":
				return CheckPawn(frm);

			default:
				return false;
		}
	}

	public bool SameColorCheck(Vector2Int frm, Vector2Int to)
	{
		return GetColor(board[frm.y, frm.x]) != GetColor(board[to.y, to.x]);
	}

	public bool MoveCheck(Vector2Int frm, Vector2Int to)
	{
		string piece = board[frm.y, frm.x];
		switch (piece.Substring(1, 1))
		{
			case "K":
				return CheckKing(frm, to);
			case "Q":
				return CheckQueen(frm, to);
			case "R":
				return CheckRook(frm, to);
			case "B":
				return CheckBishop(frm, to);
			case "N":
				return CheckKnight(frm, to);
			case "P":
				return CheckPawn(frm, to);

			default:
				return false;
		}
	}

	private void ResetPieceObject()
	{
		GameObject[] pieces = GameObject.FindGameObjectsWithTag("Piece");
		foreach (GameObject piece in pieces)
		{
			Destroy(piece);
		}
	}

	public void Move(Vector2Int frm, Vector2Int to)
	{
		ResetPieceObject();
		board[to.y, to.x] = board[frm.y, frm.x];
		board[frm.y, frm.x] = Constants.Pieces.SPACE + Constants.Pieces.SPACE + Constants.Pieces.SPACE;
		BoardPrint();
		GeneratePiece();
	}
}
