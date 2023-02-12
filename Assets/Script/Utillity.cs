using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
	private static readonly float scale = 1f;
	private static readonly float coordinateY = 0.5f;

	/// <summary>
	/// �C���f�b�N�X������W�֕ϊ�
	/// </summary>
	/// <param name="index">�C���f�b�N�X</param>
	/// <returns>coordinate</returns>
	public static Vector3 Index2Coordinate(Vector2Int index)
	{
		return new Vector3(
			 scale*(-3.5f + index.x),
			coordinateY,
			-scale*(-3.5f + index.y)
		);
	}

	/// <summary>
	/// ���W����C���f�b�N�X�֕ϊ�
	/// </summary>
	/// <param name="coordinate">���W</param>
	/// <returns>index</returns>
	public static Vector2Int Coordinate2Index(Vector3 coordinate)
	{
		return new Vector2Int(
			(int)( coordinate.x/scale +3.5f ),
			(int)(-coordinate.z/scale +3.5f )
		);
	}
}
