using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Player player;
    public GameObject Figure;
    public Vector3 offset = new Vector3(0, 10, 0);
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }        
    public Figure Spawn(){
        /*
        Description:
            Creates a new Instance of a Figure on top of itself

        Parameters: N/A

        Returns: N/A
        */
        GameObject instanz = Instantiate(Figure, gameObject.transform.position + offset, Quaternion.identity);
        instanz.GetComponent<Renderer>().material.color = player.color;
        instanz.GetComponent<Figure>().playerNr = player.playerNumber;
        return instanz.GetComponent<Figure>();
        //foreach(SpawnPoint p in player1)
        //GameObject instanz = Instantiate(Figure, gameObject.transform.position, Quaternion.identity); 
        //instanz.GetComponent<Renderer>ssssssssssssssssssssssssss().material.color = player.color; 
    }
}
