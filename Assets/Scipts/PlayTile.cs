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

    
    // Start is called before the first frame update
    private void OnDrawGizmos() 
    {
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
    
    void Start()
    {

        rend = GetComponent<Renderer>();
        position = convertCoordinatesToBoard((int) transform.localPosition.z, (int) transform.localPosition.x);
    }

    void OnMouseEnter()
    {
        if(!isBase && !isBoat){
            if(!selected){
            rend.material = HoverMaterial;
            }
        }
        
    }

    void OnMouseExit()
    {
        if(!isBase && !isBoat){
            if(!selected){
                rend.material = NormalMaterial;
            }
        }
    }

    void OnMouseDown(){
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
        selected = false;
        if(!isBase && !isBoat)
        {
            rend.material = NormalMaterial;
        }
        
    }

    public bool IsMovePossible(Vector2 Target){
        return true;
    }

    public void MakeMove(PlayTile targetTile){
        if(Mathf.Abs(targetTile.position.x-position.x) <=1 && Mathf.Abs(targetTile.position.y-position.y) <= 1){
            currentFigure.MoveRegular(targetTile);
        }else{
            if(targetTile.isBoat){
                currentFigure.boatSeat = targetTile.figuresOnBoat;
                targetTile.figuresOnBoat++;
                currentFigure.MoveRegular(targetTile);
            }else{
                currentFigure.MoveJump(targetTile);
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
        currentFigure = null;	
    }

    private Vector2 convertCoordinatesToBoard(int x, int y)
    {
        // realx  =  22boardx - 66
        // realy  =  22boardy - 198
        // Umstellen nach board-koordinaten
        // boardx = (realx + 66 )/22
        // boardy = (realy + 198)/22

        Vector2 boardCoords = new Vector2((x - origin[0])/22, (y - origin[1])/22);
        return boardCoords;
    }
}
