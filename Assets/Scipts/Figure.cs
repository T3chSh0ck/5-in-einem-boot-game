using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    private float movementFramesRegular = 60;
    private float movementFramesJump = 240;
    private float performedFrames = 0;
    public int playerNr;
    public bool movingRegular = true;
    public bool movingJump = false;
    public float y_offset = 10;

    Vector3 newPosition;
    public bool movedToBoat;
    private Vector3[] offsetOnBoat;
    public int boatSeat;
    /*
    public Figure(Vector3 position, Color col, int playerNr)
    {
        this.playerNr = playerNr;
        this.transform.position = position;
        this.GetComponent<Renderer>().material.color = col;
    }*/
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -Vector3.up, out hit)) {
            PlayTile startTile = hit.transform.gameObject.GetComponent<PlayTile>();
            if(startTile.isBase){
                startTile.AddFigureToBase(this);
            }else{
                startTile.currentFigure = this;
            }
        }
        offsetOnBoat = new Vector3[]{
            new Vector3(-0.00139999995f,-0.0174499992f,0.0104f),
            new Vector3(0.00810000021f,-0.0105299996f,0.0104f),
            new Vector3(-0.0114000002f,-0.00488999998f,0.0104f),
            new Vector3(0.00989999995f,0.00987999979f,0.0104f),
            new Vector3(-0.00789999962f,0.0161899999f,0.0104f)
        };
    }

    // Update is called once per frame
    void Update()
    {
        if(movingRegular)
        {
            
            if(movementFramesRegular >= performedFrames)
            {
                transform.position = Vector3.Lerp(transform.position, newPosition, performedFrames/movementFramesRegular);
                performedFrames++;
                if(performedFrames == movementFramesRegular)
                {
                    performedFrames = 0;
                    movingRegular = false;
                    if(movedToBoat){
                        transform.localPosition = offsetOnBoat[boatSeat];
                        movedToBoat = false;
                    }
                }
                
            }
            
        }else if(movingJump){
            if(movementFramesJump >= performedFrames)
            {
                Vector3 JumpVector = new Vector3(0, Mathf.Sin((performedFrames/movementFramesJump) * Mathf.PI), 0);
                transform.position = Vector3.Lerp(transform.position, newPosition, performedFrames/movementFramesJump) + JumpVector * 20;
                performedFrames++;
                if(performedFrames == movementFramesJump)
                {
                    performedFrames = 0;
                    movingJump = false;
                    transform.position = newPosition;
                }
                
            }
        }
    }

    public void MoveRegular(PlayTile targetTile)
    {
        movingRegular = true;
        newPosition = new Vector3(targetTile.transform.position.x, targetTile.transform.position.y + y_offset, targetTile.transform.position.z);
    }
    public void MoveJump(PlayTile targetTile)
    {
        movingJump = true;
        newPosition = new Vector3(targetTile.transform.position.x, targetTile.transform.position.y + y_offset, targetTile.transform.position.z);
    }
}
