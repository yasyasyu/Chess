using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void SetPosition(Vector2Int argIndex){
		transform.position = Utility.Index2Coordinate(argIndex);
		//Debug.Log("SP"+argIndex.ToString() + "|" + Utility.Coordinate2Index(this.gameObject.transform.position));
	}

	public void Init(Color argColor, Vector2Int argIndex)
	{
		this.gameObject.GetComponent<Renderer>().material.color = argColor;
		SetPosition(argIndex);
	}

	public Vector2Int Select()
	{
		return Utility.Coordinate2Index(this.gameObject.transform.position);
	}
}
