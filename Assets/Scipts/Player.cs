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
    public GameController con;
    public bool isAi;
    public string nickname = "Player";
    public bool doneSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnFigures(){
        {
            if (isActive == true)
            {
                foreach (SpawnPoint sp in spawnpoints)
                {
                    sp.Spawn();
                }
            }
        }
        
    }

    public void InitializePlayer(bool playersaktiv){
        isActive = playersaktiv;
        SpawnFigures();
    }
}
