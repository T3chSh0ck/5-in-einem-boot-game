using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTile : MonoBehaviour
{
    public Material HoverMaterial;
    public Material NormalMaterial;
    public Material SelectedMaterial;
    public GameController controller;
    private Renderer rend;
    private bool selected = false;
    private int[] origin = new int[2] {-44, -176};
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private int[] convertCoordinatesToBoard(int x, int y)
    {
        // realx  =  22boardx - 66
        // realy  =  22boardy - 198
        // Umstellen nach board-koordinaten
        // boardx = (realx + 66 )/22
        // boardy = (realy + 198)/22

        int[] boardCoords = new int[2] {(x - origin[0])/22, (y - origin[1])/22};
        Debug.Log(boardCoords);
        return boardCoords;
    }
}
