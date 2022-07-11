using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Player player;
    public GameObject Figure;
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
        //figure = new Figure(gameObject.transform.position, player.color, player.playerNumber);
        GameObject instanz = Instantiate(Figure, gameObject.transform.position, Quaternion.identity); 
        instanz.GetComponent<Renderer>().material.color = player.color; 
    }
}
