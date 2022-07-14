using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int playerNumber;
    public Color color;
    public SpawnPoint[] spawnpoints;
    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {





    }

    public void SpawnFigures(){
        if(isActive){
            foreach(SpawnPoint spawn in spawnpoints){
                spawn.Spawn();
            }
        }
        
    }

    public void InitializePlayer(bool active){
        isActive = active;
        SpawnFigures();
    }
}
