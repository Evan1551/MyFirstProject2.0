using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttackDistanceSystem;

public class MapCellClick : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseDown()
    {
        Debug.Log("这是一个方格");
        /*Debug.Log(this.transform.position.x);
        Debug.Log(this.transform.position.y);*/
    }


    private void OnMouseExit()
    {
        transform.GetComponent<MeshRenderer>().material.color = Color.white;
    }


    private void OnMouseEnter()
    {
        transform.GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
