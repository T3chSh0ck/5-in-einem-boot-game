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
    private Dictionary<Vector2,Figure>[] figureByPosition;
    private Transform trans;
    private PlayTile[] playTiles;
    private PlayTile originTile;

    public Player[] players;

    public int winner = 0;

    private int currentPlayer = 0;
    private bool moveMade = false;
    private Figure[] allFigures;
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.transform;
        playTiles = GetComponentsInChildren<PlayTile>();
        
        /*
        figureByPosition = new Dictionary<Vector2, Figure>[]
        {
            new Dictionary<Vector2, Figure>();
            new Dictionary<Vector2, Figure>();
            new Dictionary<Vector2, Figure>();
            new Dictionary<Vector2, Figure>();
        };*/
        
    }

    // Update is called once per frame
    void Update()
    {
        if(winner == 0){
            //Debug.Log("Player " + (currentPlayer+1) + "'s turn");
            if(moveMade){
                if(currentPlayer < 3){
                    currentPlayer++;
                }else{
                    currentPlayer = 0;
                }
                moveMade = false;
            }

        }else{
            Debug.Log("Player " + winner + " wins!");
        }
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

    public void MakeMove(PlayTile targetTile){
        if(!originTile.isBase && !targetTile.isBoat){
            //Regular Move
            playing_field_states[(int)targetTile.position.x,(int)targetTile.position.y] = currentPlayer;
            playing_field_states[(int)originTile.position.x,(int)originTile.position.y] = 0;
        }else if(originTile.isBase){
            //TODO
        }else if(targetTile.isBoat){
            //TODO
        }
        targetTile.MakeMove(targetTile);
        originTile = null;
        moveMade = true;
    }

    private bool CheckMoveValid(PlayTile targetTile){
       
        return true;
    }

    public void SelectTile(PlayTile tile){
        if(originTile == null){
            originTile = tile;
        }else{
           if(CheckMoveValid(tile)){
                MakeMove(tile);
           }else{
            originTile = tile;
           }
        }
    }
    public void InitializeGame(bool[] playersActive){
        int j = playersActive.Length;
        allFigures = FindObjectsOfType<Figure>();

        foreach (Player p in players)
        {
            for (int i = 0; i < playersActive.Length; i++)
            {
                p.InitializePlayer(playersActive[i]);
            }
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

        foreach(PlayTile tile in playTiles)
        {
            tile.state =(int) playing_field_states[(int) tile.position.x,(int) tile.position.y];
            if (playing_field_states[(int) tile.position.x,(int) tile.position.y] > 0)
            {
                tile.currentFigure = FindFigureAtCoordinates(tile.transform.position,5);
            }
        }
    }

    private Figure FindFigureAtCoordinates(Vector3 targetPos, int offset_y){
        Vector3 off = new Vector3(0, offset_y, 0);
        foreach (Figure fig in allFigures)
        {
            if(fig.transform.position == (targetPos-off)){
                return fig;
            }
        }
        return null;
    }
}
