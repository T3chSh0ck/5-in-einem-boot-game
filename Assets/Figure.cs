using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    public int playerNr;
    /*
    public Figure(Vector3 position, Color col, int playerNr)
    {
        this.playerNr = playerNr;
        this.transform.position = position;
        this.GetComponent<Renderer>().material.color = col;
    }*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move(int x, int y)
    {
        transform.position = new Vector3(transform.position.x + y * 22, 0, transform.position.z + x * 22);
    }
}
