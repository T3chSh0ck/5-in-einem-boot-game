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
    private Renderer rend;
    private bool selected = false;
    private int[] origin = new int[2] {-44, -176};
    
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
        if(!selected){
            rend.material = HoverMaterial;
        }
    }

    void OnMouseExit()
    {
        if(!selected){
            rend.material = NormalMaterial;
        }
    }

    void OnMouseDown(){
        if(!selected){
            controller.ResetFieldStates();
            rend.material = SelectedMaterial;
            selected = true;
            //var coords = convertCoordinatesToBoard(transform.position.z, transform.position.x);
            //controller.MakeMove(coords[0],coords[1]);
            //convertCoordinatesToBoard(transform.position.z, transform.position.x);
        }else{
            ResetState();
        }
        
    }

    public void ResetState(){
        selected = false;
        rend.material = NormalMaterial;
    }

    public bool IsMovePossible(Vector2 Target){
        
    }

    private Vector2 convertCoordinatesToBoard(int x, int y)
    {
        // realx  =  22boardx - 66
        // realy  =  22boardy - 198
        // Umstellen nach board-koordinaten
        // boardx = (realx + 66 )/22
        // boardy = (realy + 198)/22

        Vector2 boardCoords = new Vector2((x - origin[0])/22, (y - origin[1])/22);
        Debug.Log(boardCoords);
        return boardCoords;
    }
}
