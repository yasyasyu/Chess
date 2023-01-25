using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utillity : MonoBehaviour
{
	private static readonly int scale = 2;
	private static readonly float coordinateY = 0.5f;

	/// <summary>
	/// インデックスから座標へ変換
	/// </summary>
	/// <param name="index">インデックス</param>
	/// <returns>coordinate</returns>
	public static Vector3 Index2Coordinate(Vector2 index)
	{
		return new Vector3(index.x, 0, index.y) * scale - new Vector3(1,0,1) * (scale * 8) / 2 + new Vector3(0,coordinateY,0);
	}

	/// <summary>
	/// 座標からインデックスへ変換
	/// </summary>
	/// <param name="coordinate">座標</param>
	/// <returns>index</returns>
	public static Vector2 Coordinate2Index(Vector3 coordinate)
	{
		Vector3 tempCoordinate = (coordinate + new Vector3(1, 0, 1) * (scale * 8) / 2) / scale;
		return new Vector2(tempCoordinate.x, tempCoordinate.z);
	}
}
