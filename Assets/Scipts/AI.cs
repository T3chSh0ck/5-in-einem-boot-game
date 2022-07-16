using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct WinningMove
{
    public bool canWin; 
    public int fieldIndex;
    public PlayTile Boat;
}

public class AI : MonoBehaviour
{
    public GameController controller; 
    // Start is called before the first frame update
    public PlayTile[] AIFigurePositions;
    private int playerNumber;
    private int boatDirection;
    private System.Random decider = new System.Random();
    void Start()
    {
        AIFigurePositions = new PlayTile[8];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeAI(int num, PlayTile[] gameBoard){
        playerNumber = num;
        int AIFigureIndex = 0;
            for(int idx = 0; idx < gameBoard.Length; idx++){
                if(gameBoard[idx].currentFigure != null){
                    if(playerNumber == gameBoard[idx].currentFigure.playerNr){
                        if(gameBoard[idx].isBase){
                            for(int i = 0; i < 5; i++){
                                AIFigurePositions[AIFigureIndex++] = gameBoard[idx];
                            }
                        }else{
                            AIFigurePositions[AIFigureIndex++] = gameBoard[idx];
                        }
                    }
                }
            }          
        CheckBoatDirection();
    }

    public void DecideMove(){
        /*
        Priority:
        1. Get into Boat
        2. Jump towards Boat
        3. Move towards Boat
        4. move either left or right
        5. move back
        6. pick another figure
        7. no figure left: end turn
        */
        
        WinningMove boatJump = CheckForBoat();
        if(boatJump.canWin){
            MoveFromTo(AIFigurePositions[boatJump.fieldIndex],boatJump.Boat);
            RemoveFigureFromList(boatJump.fieldIndex);
        }else{
            CheckBoatDirection();
            int figureToMoveIdx = PickRandomFigure();
            int attemptsMade = 0;
            while(attemptsMade <= 7){
                PlayTile figureToMove = AIFigurePositions[figureToMoveIdx];
                if(JumpTowardsBoat(figureToMove, figureToMoveIdx)){
                    controller.EndTurn();
                    return;
                }else if(MoveTowardsBoat(figureToMove, figureToMoveIdx)){
                    controller.EndTurn();
                    return;
                }else{
                    attemptsMade++;
                    if(figureToMoveIdx == AIFigurePositions.Length - 1){
                        figureToMoveIdx = 0;
                    }else{
                        figureToMoveIdx++;
                    }
                }
            }
            controller.EndTurn();
        }

    }

    bool MoveTowardsBoat(PlayTile tile, int FigureIdx){
        if(tile.Neighbors[boatDirection] != null){
            if(controller.CheckMoveValid(tile, tile.Neighbors[boatDirection])){
                MoveFromTo(tile, tile.Neighbors[boatDirection], FigureIdx);
                return true;
            }
        }else if(tile.Neighbors[boatDirection + 1] != null){                                    //Walk towards boat
            if(controller.CheckMoveValid(tile, tile.Neighbors[boatDirection + 1])){
                MoveFromTo(tile, tile.Neighbors[boatDirection + 1], FigureIdx);
                return true;
            }
        }else if(tile.Neighbors[(boatDirection - 1) & 7] != null){
            if(controller.CheckMoveValid(tile, tile.Neighbors[(boatDirection - 1) & 7])){
                MoveFromTo(tile, tile.Neighbors[(boatDirection - 1) & 7], FigureIdx);
                return true;
            }
        }else if(tile.Neighbors[(boatDirection + 2)  & 7] != null){                             //Walk Left or Right
            Debug.Log((boatDirection + 2)  & 7);
            if(controller.CheckMoveValid(tile, tile.Neighbors[(boatDirection + 2)  & 7])){
                MoveFromTo(tile, tile.Neighbors[(boatDirection + 2)  & 7], FigureIdx);
                return true;
            }
        }
        else if(tile.Neighbors[(boatDirection - 2)  & 7] != null){
            if(controller.CheckMoveValid(tile, tile.Neighbors[(boatDirection - 2)  & 7])){
                MoveFromTo(tile, tile.Neighbors[(boatDirection - 2)  & 7], FigureIdx);
                return true;
            }
        }else if(tile.Neighbors[(boatDirection - 4)  & 7] != null){                              //Walk backwards
            if(controller.CheckMoveValid(tile, tile.Neighbors[(boatDirection - 4)  & 7])){
                MoveFromTo(tile, tile.Neighbors[(boatDirection - 4)  & 7], FigureIdx);
                return true;
            }
        }else if(tile.Neighbors[(boatDirection - 3)  & 7] != null){
            if(controller.CheckMoveValid(tile, tile.Neighbors[(boatDirection - 3)  & 7])){
                MoveFromTo(tile, tile.Neighbors[(boatDirection - 3)  & 7], FigureIdx);
                return true;
            }
        }else if(tile.Neighbors[(boatDirection - 5)  & 7] != null){
            if(controller.CheckMoveValid(tile, tile.Neighbors[(boatDirection - 5)  & 7])){
                MoveFromTo(tile, tile.Neighbors[(boatDirection - 5)  & 7], FigureIdx);
                return true;
            }
        }
        return false;
    } 

    void RemoveFigureFromList(int idx){
        AIFigurePositions[idx] = AIFigurePositions[AIFigurePositions.Length-1];
        Array.Resize(ref AIFigurePositions, AIFigurePositions.Length-1);
    }
    
    bool JumpTowardsBoat(PlayTile tile, int FigureIdx){
        
        if(tile.Neighbors[boatDirection] != null){
            if(tile.Neighbors[boatDirection].Neighbors[boatDirection] != null){
                if(controller.CheckForJump(tile, tile.Neighbors[boatDirection].Neighbors[boatDirection])){
                    MoveFromTo(tile, tile.Neighbors[boatDirection].Neighbors[boatDirection], FigureIdx);
                    return true;
                }
            }
            
        }else if(tile.Neighbors[boatDirection + 1] != null){
            if(tile.Neighbors[boatDirection + 1].Neighbors[boatDirection + 1] != null){
                if(controller.CheckForJump(tile, tile.Neighbors[boatDirection + 1].Neighbors[boatDirection + 1])){
                    MoveFromTo(tile, tile.Neighbors[boatDirection + 1].Neighbors[boatDirection + 1], FigureIdx);
                    return true;
                }
            }
            
        }else if(tile.Neighbors[(boatDirection - 1) & 7] != null){
            if(tile.Neighbors[(boatDirection - 1) & 7].Neighbors[(boatDirection - 1) & 7] != null){
                if(controller.CheckForJump(tile, tile.Neighbors[(boatDirection - 1) & 7].Neighbors[(boatDirection - 1) & 7])){
                    MoveFromTo(tile, tile.Neighbors[(boatDirection - 1) & 7].Neighbors[(boatDirection - 1) & 7], FigureIdx);
                    return true;
                }
            }  
        }
        return false;
    }

    void CheckBoatDirection(){
        /*
            Boat direction relative to Tile is BoatDirection*2
            BoatPosition contains 4 Values (North, South, East, West), each corresponding to a player number
            Boatdirection is used to decide the direction of a move (8 possible)

            boatDirection values: 0 (North), 2 (East), 4 (South), 6 (West)
        */
        boatDirection = controller.getBoatPosition(playerNumber) * 2;
    }

    int PickRandomFigure(){
        //Picks a random index of the available figures
        return decider.Next(AIFigurePositions.Length);
    }

    WinningMove CheckForBoat(){
        /*
            Check whether the AI can move onto a boat this turn.
            If yes, return all relevant data
        */
        WinningMove result = new WinningMove();
        for(int i = 0; i < AIFigurePositions.Length; i++)
        {
            for(int j = 0; j < AIFigurePositions[i].Neighbors.Length; j++)
            {
                if(AIFigurePositions[i].Neighbors[j] != null){
                    if(AIFigurePositions[i].Neighbors[j].isBoat && AIFigurePositions[i].Neighbors[j].boatColor == playerNumber){
                        result.canWin = true;
                        result.fieldIndex = i;
                        result.Boat = AIFigurePositions[i].Neighbors[j];
                        return result;
                    }else if(AIFigurePositions[i].Neighbors[j].Neighbors[j] != null){
                        if(AIFigurePositions[i].Neighbors[j].Neighbors[j].isBoat && AIFigurePositions[i].Neighbors[j].Neighbors[j].boatColor == playerNumber){
                            if(AIFigurePositions[i].Neighbors[j].currentFigure != null){
                                result.canWin = true;
                                result.fieldIndex = i;
                                result.Boat = AIFigurePositions[i].Neighbors[j].Neighbors[j];
                                return result;
                            }
                            
                        }
                    }
                }
            }
        }
        return result;
    }

    void MoveFromTo(PlayTile fromTile, PlayTile toTile){
        Debug.Log("AI Player "+ playerNumber + " moving from " + fromTile + " to  Boat");
        controller.SelectTile(fromTile);
        controller.SelectTile(toTile);
    }

    void MoveFromTo(PlayTile fromTile, PlayTile toTile, int FigureIdx){
        Debug.Log("AI Player "+ playerNumber + " moving from " + fromTile + " to " + toTile + ", at Idx: " + FigureIdx);
        AIFigurePositions[FigureIdx] = toTile;
        controller.SelectTile(fromTile);
        controller.SelectTile(toTile);
    }
}
