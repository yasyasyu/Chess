using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
	public static class Pieces
	{
		public static readonly string SPACE = "S";

		//K �L���O
		public static readonly string KING = "K";
		//Q �N�C�[��
		public static readonly string QUEEN = "Q";
		//R ���[�N
		public static readonly string ROOK = "R";
		//B �r�V���b�v
		public static readonly string BISHOP = "B";
		//N �i�C�g
		public static readonly string KNIGHT = "N";
		//P �|�[��
		public static readonly string PAWN = "P";

		//��
		public static readonly string BLACK = "B";
		//��
		public static readonly string WHITE = "W";
	}
	public static class PieceObject
	{
		//K �L���O
		public static readonly GameObject KING = Resources.Load("King") as GameObject;
		//Q �N�C�[��
		public static readonly GameObject QUEEN = Resources.Load("Queen") as GameObject;
		//R ���[�N
		public static readonly GameObject ROOK = Resources.Load("Rook") as GameObject;
		//B �r�V���b�v
		public static readonly GameObject BISHOP = Resources.Load("Bishop") as GameObject;
		//N �i�C�g
		public static readonly GameObject KNIGHT = Resources.Load("Knight") as GameObject;
		//P �|�[��
		public static readonly GameObject PAWN = Resources.Load("Pawn") as GameObject;
	}
	public static class PieceColor
	{
		//��
		public static readonly Color BLACK = Color.black;
		//��
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
