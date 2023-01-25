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

	void SetPosition(Vector2 argIndex){
		transform.position = Utillity.Index2Coordinate(argIndex);
		//Debug.Log(index.ToString() + "|" + Utillity.Coordinate2Index(this.gameObject.transform.position));
	}

	public void Init(Color argColor, Vector2 argIndex)
	{
		this.gameObject.GetComponent<Renderer>().material.color = argColor;
		SetPosition(argIndex);
	}

	private Vector2 Select()
	{
		return Utillity.Index2Coordinate(this.gameObject.transform.position);
	}
}
