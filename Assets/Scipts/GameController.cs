using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int[] origin = new int[2] {-44, -176};
    /*Field states:
     * null: no field
     * 0: free field
     * 1: player 1
     * 2: player 2
     * 3: player 3
     * 4: player 4
     */

    private int[] HomeFigures = new int[4] { 5, 5, 5, 5 };
    public int[] BoatFigures = new int[4] { 0, 0, 0, 0 };
    // BoatPositions: {N, E, S, W}
    private int[] BoatPositions = new int[4] { 1, 2, 3, 4 };

    private int?[,] playing_field_states = new int?[16, 16]{ {null,null,null,null,null,null,   0,   0,   0,   0,null,null,null,null,null,null},
                                                             {null,null,null,null,null,null,null,   0,   0,null,null,null,null,null,null,null},
                                                             {null,null,   2,null,null,null,null,   0,   0,null,null,null,null,   3,null,null},
                                                             {null,null,null,null,null,null,null,   0,   0,null,null,null,null,null,null,null},
                                                             {null,null,null,null,   2,   0,   2,   0,   2,   0,   0,   3,null,null,null,null},
                                                             {null,null,null,null,   0,   0,   0,   0,   0,   0,   0,   0,null,null,null,null},
                                                             {   0,null,null,null,   0,   0,null,null,null,null,   0,   3,null,null,null,   0},
                                                             {   0,   0,   0,   0,   1,   0,null,null,null,null,   0,   0,   0,   0,   0,   0},
                                                             {   0,   0,   0,   0,   0,   0,null,null,null,null,   0,   3,   0,   0,   0,   0},
                                                             {   0,null,null,null,   1,   0,null,null,null,null,   0,   0,null,null,null,   0},
                                                             {null,null,null,null,   0,   0,   0,   0,   0,   0,   0,   0,null,null,null,null},
                                                             {null,null,null,null,   1,   0,   0,   4,   0,   4,   0,   4,null,null,null,null},
                                                             {null,null,null,null,null,null,null,   0,   0,null,null,null,null,null,null,null},
                                                             {null,null,   1,null,null,null,null,   0,   0,null,null,null,null,   4,null,null},
                                                             {null,null,null,null,null,null,null,   0,   0,null,null,null,null,null,null,null},
                                                             {null,null,null,null,null,null,   0,   0,   0,   0,null,null,null,null,null,null}};
    private Vector3[] figureOffsetOnBoat;
    private Dictionary<Vector2,Figure>[] figureByPosition;
    private Transform trans;
    private PlayTile[] playTiles;
    public PlayTile[] boats;
    public PlayTile originTile;

    public Player[] players;

    public int winner = 0;

    public int currentPlayer = 0;
    private bool moveMade = false;
    public Figure[] allFigures;
    public GameObject BoatPivot;
    private float rotationTime = 7.0f;
    private float rotationEnd = 0;
    private float timer;
    private bool rotating;
    private Vector3 nextRotation;
    void Start()
    {
        trans = gameObject.transform;
        playTiles = GetComponentsInChildren<PlayTile>();
        figureOffsetOnBoat = new Vector3[]{
            new Vector3(-0.00139999995f,-0.0174499992f,0.0104f),
            new Vector3(0.00810000021f,-0.0105299996f,0.0104f),
            new Vector3(-0.0114000002f,-0.00488999998f,0.0104f),
            new Vector3(0.00989999995f,0.00987999979f,0.0104f),
            new Vector3(-0.00789999962f,0.0161899999f,0.0104f)
        };
    }


    void Update()
    {
        if(rotating){
            Debug.Log("Spin?");
            timer += Time.deltaTime;
            if(timer <= rotationEnd){
                BoatPivot.transform.eulerAngles = new Vector3(0, BoatPivot.transform.eulerAngles.y + (90/rotationTime) * Time.deltaTime,0);
                
            }else{
                rotating = false;
            }
        }
    }
    public void EndTrain()
    {
        if (!players[currentPlayer].isActive)
        {
            //NextPlayer();
        }
        if (winner == 0)
        {
            if (moveMade)
            {
                NextPlayer();
                moveMade = false;
            }
        }
        else
        {
            Debug.Log("Player " + winner + " wins!");
        }
    }
    public void NextPlayer(){
        if(currentPlayer < 3){
            currentPlayer++;
        }else{
            currentPlayer = 0;
        }
    }

    void RotateBoats()
    {
        rotationEnd = Time.time + rotationTime;
        timer = Time.time;
        rotating = true;
        int swap = BoatPositions[0];
        BoatPositions[0] = BoatPositions[1];
        BoatPositions[1] = BoatPositions[2];
        BoatPositions[2] = BoatPositions[3];
        BoatPositions[3] = swap;

        PlayTile[] temp = boats[0].Neighbors;
        boats[0].Neighbors = boats[1].Neighbors;
        boats[1].Neighbors = boats[2].Neighbors;
        boats[2].Neighbors = boats[3].Neighbors;
        boats[3].Neighbors = temp;

        foreach (PlayTile boat in boats)
        {
            foreach (PlayTile n in boat.Neighbors)
            {
                for (int i = 0; i < n.Neighbors.Length; i++)
                {
                    if(n.Neighbors[i] != null){
                        if(n.Neighbors[i].isBoat){
                            n.Neighbors[i] = boat;
                        }
                    }
                }   
            }
        }
        
    }

    public void ResetFieldStates(){
        foreach(var tile in playTiles){
            tile.ResetState();
        }
    }

    public void MakeMove(PlayTile targetTile){
        if(!originTile.isBase && !targetTile.isBoat){
            //Regular Move
            playing_field_states[(int)targetTile.position.x,(int)targetTile.position.y] = currentPlayer;
            playing_field_states[(int)originTile.position.x,(int)originTile.position.y] = 0;
        }else if(originTile.isBase){
            HomeFigures[currentPlayer] -= 1;
        }else if(targetTile.isBoat){
            BoatFigures[currentPlayer] += 1;
            if(BoatFigures[currentPlayer] >= 5){
                winner = currentPlayer + 1;
            }
            RotateBoats();
        }
        originTile.MakeMove(targetTile);
        originTile = null;
        moveMade = true;
    }

    private bool CheckMoveValid(PlayTile targetTile){
        if(targetTile.isBoat){
            if(targetTile.boatColor != currentPlayer){
                return false;
            }
        }
        if(!targetTile.isBase){
            if(!originTile.isBoat){
                if(targetTile.currentFigure == null){
                    if(originTile.Neighbors.Contains(targetTile)){
                        return true;
                    }else if(CheckForJump(originTile, targetTile)){
                        return true;
                    }
                }
            }
        }else 
        if(CheckForJump(originTile, targetTile)){
            return true;
        }
        return false;
    }

    public bool CheckForJump(PlayTile startTile, PlayTile targetTile){
        for(int i = 0; i < 8; i++)
        {
            if(startTile.Neighbors[i] != null)
            {
                if(startTile.Neighbors[i].Neighbors[i] != null)
                {
                    if(startTile.Neighbors[i].currentFigure != null)
                    {
                        if(startTile.Neighbors[i].Neighbors[i] == targetTile)
                        {
                            return true;
                        }
                    }
                }
            }
                    
        }
        return false;
    }

    public void SelectTile(PlayTile tile){
        if(originTile == tile){
            originTile = null;
            return;
        } 

        if(originTile == null){
            if(tile.currentFigure != null){
                originTile = tile;
            }
           
        }else{
            //Debug.Log("Move Valid");
            if(CheckMoveValid(tile)){
                MakeMove(tile);
                originTile = null;
            }else{
                originTile = tile;
            }
        }
    }
    public void InitializeGame(bool[] playersActive){
        int j = playersActive.Length;
        for (int i = 0; i < playersActive.Length; i++)
        {
            players[i].InitializePlayer(playersActive[i]);
        }

        for (int i = 0; i < playersActive.Length; i++)
        {
            if(!playersActive[i])
            {
                for(int y = 0; y < 16; y++)
                {
                    for(int x = 0; x < 16; x++)
                    {
                        if(playing_field_states[x,y] == i+1)
                        {
                            playing_field_states[x,y] = 0;
                        }
                    }
                }
            }
        }
    }

    public void AddToFigures(Figure fig)
    {
        Array.Resize(ref allFigures, allFigures.Length + 1);
        allFigures[allFigures.Length-1] = fig;
    }
}
