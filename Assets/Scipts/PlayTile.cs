using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTile : MonoBehaviour
{
    public Material HoverMaterial;
    public Material NormalMaterial;
    public Material SelectedMaterial;
    public GameController controller;
    public Vector2 position;
    public int state = 0;
    public PlayTile[] Neighbors; //Clockwise, starting "north" (-x)
    public bool isBase = false;
    public bool isBoat = false;
    public Figure currentFigure;
    private Renderer rend;
    private bool selected = false;
    private int[] origin = new int[2] {-44, -176};

    public int figuresOnBoat;
    public int boatColor;

    public Figure[] figuresOnBase;

    
    
    private void OnDrawGizmos() 
    {
        /*
        Description:
            Preset Method in Unity. Only called in Editor, left out when building
            Draws Lines between PlayTiles and their Neighbors

        Parameters: N/A

        Returns: N/A
        */
        Vector3 offset = new Vector3(0,5,0);
        Gizmos.color = Color.red;
        foreach(PlayTile neighbor in Neighbors)
        {
            if(neighbor != null)
            {              
                Gizmos.DrawLine(transform.position + offset, neighbor.transform.position + offset);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        rend = GetComponent<Renderer>();
        position = convertCoordinatesToBoard((int) transform.localPosition.z, (int) transform.localPosition.x);
    }

    void OnMouseEnter()
    {
        /*
        Description:
            Preset Method in Unity. Called when Mouse enters Objects hitbox.
            Highlights this tile.

        Parameters: N/A

        Returns: N/A
        */
        
        if(!isBase && !isBoat){
            if(!selected){
            rend.material = HoverMaterial;
            }
        }
        
    }

    void OnMouseExit()
    {
        /*
        Description:
            Preset Method in Unity. Called when Mouse exits Objects hitbox.
            Removes the Highlight on this tile.

        Parameters: N/A

        Returns: N/A
        */
        if(!isBase && !isBoat){
            if(!selected){
                rend.material = NormalMaterial;
            }
        }
    }

    void OnMouseDown(){
        /*
        Description:
            Preset Method in Unity. Called when Mouse clicks on the Object.
            Selects tile and changes Highlight color.

        Parameters: N/A

        Returns: N/A
        */
        if(controller.players[controller.currentPlayer].isAi){
            return;
        }
        if(!selected){
            if(currentFigure != null){
                if(currentFigure.playerNr != controller.currentPlayer){
                return;
                }
            }
            controller.ResetFieldStates();
            if(!isBase && !isBoat && currentFigure != null){
                rend.material = SelectedMaterial;
                selected = true;
            }
            
            controller.SelectTile(this);
        }else{
            ResetState();
        }
        
    }

    public void ResetState(){
        /*
        Description:
            Resets all highlights of this tile

        Parameters: N/A

        Returns: N/A
        */
        selected = false;
        if(!isBase && !isBoat)
        {
            rend.material = NormalMaterial;
        }
        
    }

    public void MakeMove(PlayTile targetTile){
        //if the target is a direct neighbor of this tile
        if(Neighbors.Contains(targetTile)){
            currentFigure.MoveRegular(targetTile);
            controller.moreJumpsPossibleThisTurn = false;
        }else{
            if(targetTile.isBoat){
                currentFigure.boatSeat = targetTile.figuresOnBoat;
                targetTile.figuresOnBoat++;
                currentFigure.MoveJump(targetTile);
                controller.moreJumpsPossibleThisTurn = false;
            }else{
                currentFigure.MoveJump(targetTile);
                controller.moreJumpsPossibleThisTurn = true;
            }
            
        }
        if(targetTile.isBoat){
            currentFigure.transform.SetParent(targetTile.transform,true);
            targetTile.currentFigure = null;
            currentFigure.movedToBoat = true;
        }else{
            
            targetTile.ResetState();
            targetTile.currentFigure = currentFigure;
        }
        ResetState();
        if(isBase){
            if(figuresOnBase.Length > 0){
                Array.Resize(ref figuresOnBase, figuresOnBase.Length - 1);
                if(figuresOnBase.Length == 0){
                    currentFigure = null;
                }else{
                    currentFigure = figuresOnBase[figuresOnBase.Length-1];
                }
            }
        }else{
            currentFigure = null;	
        }
        
    }

    public void AddFigureToBase(Figure fig){
        /*
        Description:
            Adds a new Figure to the FiguresOnBase Array

        Parameters: Figure fig: Figure to add to the Array

        Returns: N/A
        */
        Array.Resize(ref figuresOnBase, figuresOnBase.Length + 1);
        figuresOnBase[figuresOnBase.Length-1] = fig;
        currentFigure = fig;
    }

    private Vector2 convertCoordinatesToBoard(int x, int y)
    {
        /*
        Description:
            Converts world coordinates to board coordinates

        Parameters: 
            int x: X-Coordinate (relative to board)
            int y: Y-Coordinate (relative to board)

        Returns: Vector2 boardCoords: Board coordinates
        */
        // realx  =  22boardx - 66
        // realy  =  22boardy - 198
        // Umstellen nach board-koordinaten
        // boardx = (realx + 66 )/22
        // boardy = (realy + 198)/22

        Vector2 boardCoords = new Vector2((x - origin[0])/22, (y - origin[1])/22);
        return boardCoords;
    }
}
