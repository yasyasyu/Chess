using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetPosition(Vector2Int argIndex)
    {
        Vector3 coordinate = Utility.Index2Coordinate(argIndex);
        this.transform.position = coordinate - new Vector3(0, 0.7f, 0);
        //Debug.Log("SP"+argIndex.ToString() + "|" + Utility.Coordinate2Index(this.gameObject.transform.position));
    }

    public void Init(Vector2Int argIndex)
    {
        SetPosition(argIndex);
        transform.localScale = new Vector3( Utility.scale*0.8f, transform.localScale.y, Utility.scale*0.8f);
    }
}
