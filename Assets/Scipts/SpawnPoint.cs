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
        //Figure.playerNr = player.playerNumber;

    }

    // Update is called once per frame
    void Update()
    {
        
    }        
    public void Spawn(){
        GameObject instanz = Instantiate(Figure, gameObject.transform.position + offset, Quaternion.identity);
        instanz.GetComponent<Renderer>().material.color = player.color;
        instanz.GetComponent<Figure>().playerNr = player.playerNumber;
        //foreach(SpawnPoint p in player1)
        //GameObject instanz = Instantiate(Figure, gameObject.transform.position, Quaternion.identity); 
        //instanz.GetComponent<Renderer>ssssssssssssssssssssssssss().material.color = player.color; 
    }
}
