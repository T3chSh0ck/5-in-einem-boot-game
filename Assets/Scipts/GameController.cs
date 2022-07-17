using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int[] BoatFigures = new int[4] { 0, 0, 0, 0 };
    // BoatPositions: {N, E, S, W}
    private int[] BoatPositions = new int[4] { 0, 1, 2, 3 };


    private Vector3[] figureOffsetOnBoat;
    private Dictionary<Vector2,Figure>[] figureByPosition;
    private Transform trans;
    private PlayTile[] playTiles;
    public PlayTile[] boats;
    public PlayTile originTile;

    public Player[] players;
    public int winner = 0;

    public int currentPlayer = 0;
    public Figure[] allFigures;
    public GameObject BoatPivot;
    private float rotationTime = 3.0f;
    private float rotationEnd = 0;
    private float timer;
    private bool rotating;
    private Vector3 nextRotation;

    public bool moreJumpsPossibleThisTurn = false;
    private PlayTile lastJumpTarget;
    bool AIMoveMade = false;

    public MainMenu menu;

    private System.Random rand;

    void Start()
    {
        /*
        Description:
            Start is called on the first frame the object exists

        Parameters: N/A

        Returns: N/A
        */

        //Initialize relevant attributes
        rand = new System.Random();
        trans = gameObject.transform;
        playTiles = GetComponentsInChildren<PlayTile>();

        //Measured offset for boat seats
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
        /*
        Description:
            Update is called every frame update

        Parameters: N/A

        Returns: N/A
        */

        //Check if the currentPlayer is participating. If not, move on to the next player
        if (!players[currentPlayer].isActive)  
        {
            NextPlayer();
        }

        //Check if it is the turn of an AI
        if(players[currentPlayer].isAi && !AIMoveMade){
            Debug.Log("AI " + currentPlayer + "'s turn");
            AIMoveMade = true;
            WaitBeforeDeciding();
        }

        //Animation controller for boat movement
        if (rotating){
            timer += Time.deltaTime;
            if(timer <= rotationEnd){
                BoatPivot.transform.eulerAngles = new Vector3(0, BoatPivot.transform.eulerAngles.y + (90/rotationTime) * Time.deltaTime,0);
                
            }else{
                rotating = false;
            }
        }
    }


    public void EndTurnIfNotAI(){
        /*
        Description:
            Prevents the player from ending the AIs turn prematurely

        Parameters: N/A

        Returns: N/A
        */

        if(!players[currentPlayer].isAi){
            EndTurn();
        }
    }


    public void EndTurn()
    {
        /*
        Description:
            Ends the current players turn and checks if they won

        Parameters: N/A

        Returns: N/A
        */

        if (winner == 0)
        {
            Debug.Log("Player " + currentPlayer + " done");
            NextPlayer();
        }
        else
        {
            menu.AndTheWinnerIs(players[winner-1].nickname, players[winner-1].color);
        }
    }


    public void RestartGame(){
        /*
        Description:
            Restart the game from the beginning

        Parameters: N/A

        Returns: N/A
        */

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void NextPlayer(){
        /*
        Description:
            Changes the active player to the next player and resets all relevant values

        Parameters: N/A

        Returns: N/A
        */

        if(currentPlayer < 3){
            currentPlayer++;
        }else{
            currentPlayer = 0;
        }
        originTile = null;
        lastJumpTarget = null;
        moreJumpsPossibleThisTurn = false;
        AIMoveMade = false;
        menu.SetPlayerActiveText(players[currentPlayer].nickname, players[currentPlayer].color);
    }
    

    public void WaitBeforeDeciding(){
        /*
        Description:
            Wait a random amount of time before calling DecideMove

        Parameters: N/A

        Returns: N/A
        */

        StartCoroutine(PauseGame(0.9f + (1 / (rand.Next(9)+1) )));
    }


    public IEnumerator PauseGame(float pauseTime){
        /*
        Description:
            Pauses the game for a specified amount of seconds,
            calls DecideMove once finished waiting

        Parameters: 
            float pauseTime: Time to pause the game for (in Seconds)

        Returns: N/A
        */

        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        
        players[currentPlayer].AIController.DecideMove();
    }


    void RotateBoats()
    {
        /*
        Description:
            Move all Boats to next Port

        Parameters: N/A

        Returns: N/A
        */

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
        /*
        Description:
            Reset the current state of all PlayTiles

        Parameters: N/A

        Returns: N/A
        */

        foreach(var tile in playTiles){
            tile.ResetState();
        }
    }

    public void MakeMove(PlayTile targetTile){
        /*
        Description:
            Movement Logic entry point

        Parameters: 
            PlayTile targetTile: PlayTile to move to

        Returns: N/A
        */

        if(targetTile.isBoat){
            BoatFigures[currentPlayer] += 1;
            if(BoatFigures[currentPlayer] >= 5){
                winner = currentPlayer + 1;
            }
            RotateBoats();
        }
        originTile.MakeMove(targetTile);
        originTile = null;
    }

    public bool CheckMoveValid(PlayTile startTile, PlayTile targetTile){
        /*
        Description:
            Checks whether a move from startTile to targetTile is valid

        Parameters: 
            PlayTile startTile: Origin PlayTile of the move
            PlayTile targetTile: PlayTile to move to

        Returns: bool moveValid
        */

        //If the target is another player's boat, abort
        if(targetTile.isBoat){
            if(targetTile.boatColor != currentPlayer){
                return false;
            }
        }

        //Check regular moves
        if(!targetTile.isBase){
            if(!startTile.isBoat){
                if(targetTile.currentFigure == null){
                    if(startTile.Neighbors.Contains(targetTile)){
                        if(!moreJumpsPossibleThisTurn){
                            return true;
                        }
                    }else if(moreJumpsPossibleThisTurn){
                        if(startTile == lastJumpTarget){
                            if(CheckForJump(startTile, targetTile)){
                                return true;
                            }
                        }
                    }else if(CheckForJump(startTile, targetTile)){
                        return true;
                    }
                }
            }
        }else if(CheckForJump(startTile, targetTile)){
            return true;
        }
        // If nothing else applies, the move is invalid
        return false;
    }

    public bool CheckForJump(PlayTile startTile, PlayTile targetTile){
        /*
        Description:
            Checks whether a jump from startTile to targetTile is valid

        Parameters: 
            PlayTile startTile: Origin PlayTile of the move
            PlayTile targetTile: PlayTile to move to

        Returns: bool moveValid
        */
        for(int i = 0; i < 8; i++)
        {
            if(startTile.Neighbors[i] != null)
            {
                if(!startTile.Neighbors[i].isBoat)
                {
                    if(startTile.Neighbors[i].Neighbors[i] != null)
                    {
                        if(startTile.Neighbors[i].currentFigure != null)
                        {
                            if(startTile.Neighbors[i].Neighbors[i] == targetTile)
                            {
                                if(targetTile.currentFigure == null){
                                    return true;
                                }
                            }
                        }
                    }
                }
                
            }
                    
        }
        return false;
    }

    public void SelectTile(PlayTile tile){
        /*
        Description:
            Selects a tile and executes a move if valid

        Parameters: 
            PlayTile tile: Tile the player/ai wants to select

        Returns: N/A
        */
        if(originTile == tile){
            originTile = null;
            return;
        } 

        //If no other tile has been selected before this one
        if(originTile == null){
            if(tile.currentFigure != null){
                if(lastJumpTarget != null){
                    if(tile == lastJumpTarget){
                        originTile = tile;
                    }
                    else{
                        originTile = null;
                    }
                }else{
                    originTile = tile;
                }
            }
        //If another tile has been selected
        }else{
            if(CheckMoveValid(originTile, tile)){
                if(CheckForJump(originTile, tile)){
                    lastJumpTarget = tile;
                    MakeMove(tile);
                }else{
                    MakeMove(tile);
                    originTile = null;
                    EndTurn();
                }
                
            }else{
                originTile = tile;
            }
        }
    }
    public void InitializeGame(bool[] playersActive){
        /*
        Description:
            Initializes all game parameters and players

        Parameters: 
            bool[] playersActive: Length 4, information on which players are active. index = player

        Returns: N/A
        */
        menu.SetPlayerActiveText(players[currentPlayer].nickname, players[currentPlayer].color);
        for (int i = 0; i < playersActive.Length; i++)
        {
            players[i].InitializePlayer(playersActive[i], playTiles);
        }
    }

    public void AddToFigures(Figure fig)
    {
        /*
        Description:
            Add a new Figure to the allFigures array

        Parameters: 
            Figure fig: The figure to add

        Returns: N/A
        */
        Array.Resize(ref allFigures, allFigures.Length + 1);
        allFigures[allFigures.Length-1] = fig;
    }

    public int getBoatPosition(int p){
        /*
        Description:
            Get the position of a players boat
            0 = north
            1 = east
            2 = south
            3 = west

        Parameters: 
            int p: playerNumber

        Returns: int boatPosition
        */

        return BoatPositions[p];
    }
}
