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
    public AI AIController;
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

    public void SpawnFigures(PlayTile[] gameBoard){
        /*
        Description:
            Spawns the Figures of this player and creates an AI if necessary

        Parameters: PlayTile[] gameBoard: All tiles on the playing field

        Returns: N/A
        */
            if (isActive == true)
            {
                foreach (SpawnPoint sp in spawnpoints)
                {
                    sp.Spawn().InitializePosition();
                }
                if(isAi){
                    
                    
                    AIController.InitializeAI(playerNumber, gameBoard);
                }
            }

    }
    public void InitializePlayer(bool playersaktiv, PlayTile[] gameBoard){
        /*
        Description:
            Sets up a player object

        Parameters: 
            bool playersaktiv: Is the player participating?
            PlayTile[] gameBoard: All tiles on the playing field

        Returns: N/A
        */
        isActive = playersaktiv;
        SpawnFigures(gameBoard);
    }
}
