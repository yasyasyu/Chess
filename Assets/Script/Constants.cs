using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
	public static class Pieces
	{
		public static readonly string SPACE = "S";

		//K キング
		public static readonly string KING = "K";
		//Q クイーン
		public static readonly string QUEEN = "Q";
		//R ルーク
		public static readonly string ROOK = "R";
		//B ビショップ
		public static readonly string BISHOP = "B";
		//N ナイト
		public static readonly string KNIGHT = "N";
		//P ポーン
		public static readonly string PAWN = "P";

		//黒
		public static readonly string BLACK = "B";
		//白
		public static readonly string WHITE = "W";
	}
	public static class PieceObject
	{
		//K キング
		public static readonly GameObject KING = Resources.Load("King") as GameObject;
		//Q クイーン
		public static readonly GameObject QUEEN = Resources.Load("Queen") as GameObject;
		//R ルーク
		public static readonly GameObject ROOK = Resources.Load("Rook") as GameObject;
		//B ビショップ
		public static readonly GameObject BISHOP = Resources.Load("Bishop") as GameObject;
		//N ナイト
		public static readonly GameObject KNIGHT = Resources.Load("Knight") as GameObject;
		//P ポーン
		public static readonly GameObject PAWN = Resources.Load("Pawn") as GameObject;
	}
	public static class PieceColor
	{
		//黒
		public static readonly Color BLACK = Color.black;
		//白
		public static readonly Color WHITE = Color.white;
	}

	public static class PieceDictionary
	{
		public static readonly Dictionary<string, GameObject> RANK = new Dictionary<string, GameObject>()
		{
			{Pieces.KING, PieceObject.KING},
			{Pieces.QUEEN, PieceObject.QUEEN},
			{Pieces.ROOK, PieceObject.ROOK},
			{Pieces.BISHOP, PieceObject.BISHOP},
			{Pieces.KNIGHT, PieceObject.KNIGHT},
			{Pieces.PAWN, PieceObject.PAWN},
		};
		public static readonly Dictionary<string, Color> COLOR = new Dictionary<string, Color>()
		{
			{Pieces.BLACK, PieceColor.BLACK},
			{Pieces.WHITE, PieceColor.WHITE},
		};
	}
}
