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
    private int[] BoatFigures = new int[4] { 0, 0, 0, 0 };
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
    private Transform trans;
    private PlayTile[] playTiles;
    private int[] originTile;

    public Player[] players;
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.transform;
        playTiles = GetComponentsInChildren<PlayTile>();
        
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(playing_field_states);
    }

    void UpdatePlacements()
    {

    }

    /*PlayTile getTileAtCoordinates(int x, int y)
    {

    }*/

    

    int[] convertBoardToCoordinates(int x, int y)
    {
        int[] realCoords = new int[2] { origin[0] + x * 22, origin[1] + y * 22 };
        return realCoords;
    }

    void RotateBoats()
    {
        int swap = BoatPositions[0];
        BoatPositions[0] = BoatPositions[3];
        BoatPositions[3] = BoatPositions[2];
        BoatPositions[2] = BoatPositions[1];
        BoatPositions[1] = swap;
        
    }

    public void ResetFieldStates(){
        foreach(var tile in playTiles){
            tile.ResetState();
        }
    }

    public void MakeMove(int x, int y){
        	if(originTile is null){
                originTile[0] = x;
                originTile[1] = y;
            }else{
                if(CheckMoveValid(x, y)){
                    Debug.Log("Valid move");
                    playing_field_states[x,y] = playing_field_states[originTile[0],originTile[1]];
                    playing_field_states[originTile[0],originTile[1]] = 0;
                }
            }
            OutputFieldStates();
    }

    private bool CheckMoveValid(int x, int y){
        if(playing_field_states[x,y] == 0){
            if(originTile[0] != x && originTile[1] != y){
                if( ((originTile[0] - 1 == x || originTile[1] - 1 == y) || (originTile[0] + 1 == x || originTile[1] +1 == y))){
                    return true;
                }
            }
            
        }
        return false;
    }

    private void OutputFieldStates(){
        string line = "";
        for(int y = 0; y < 16; y++){
            for(int x = 0; x < 16; x++){
                if(playing_field_states[x,y] is null){
                    line += "x";
                }
                else{
                    line += playing_field_states[x,y];
                }
            }
            Debug.Log(line);
            line = "";
        }
    }

    public void InitializeGame(bool[] playersActive){
        int i = 0;
        foreach(Player p in players){
            p.InitializePlayer(playersActive[i]);
        }
    }
}
