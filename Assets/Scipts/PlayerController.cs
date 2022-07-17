using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Material Player1;
    public Material Player2;
    public Material Player3;
    public Material Player4;
    [Range(1,4)]
    public int player = 0;
    // Start is called before the first frame update
    void Start()
    {
        switch (player)
        {
            case 1:
                GetComponent<Renderer>().material = Player1;
                break;
            case 2:
                GetComponent<Renderer>().material = Player2;
                break;
            case 3:
                GetComponent<Renderer>().material = Player3;
                break;
            case 4:
                GetComponent<Renderer>().material = Player4;
                break;
            default:
                print("No Player set for Figure: " + this.name);
                break;
        } 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}