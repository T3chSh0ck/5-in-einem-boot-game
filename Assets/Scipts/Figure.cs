using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    private float movementFramesRegular = 60;
    private float movementFramesJump = 90;
    private float performedFrames = 0;
    public int playerNr;
    public bool movingRegular = true;
    public bool movingJump = false;
    public float y_offset = 10;

    Vector3 newPosition;
    public bool movedToBoat;
    private Vector3[] offsetOnBoat;
    public int boatSeat;

    // Start is called before the first frame update
    void Start()
    {
        
        offsetOnBoat = new Vector3[]{
            new Vector3(-0.00139999995f,-0.0174499992f,0.0104f),
            new Vector3(0.00810000021f,-0.0105299996f,0.0104f),
            new Vector3(-0.0114000002f,-0.00488999998f,0.0104f),
            new Vector3(0.00989999995f,0.00987999979f,0.0104f),
            new Vector3(-0.00789999962f,0.0161899999f,0.0104f)
        };
    }

    public void InitializePosition(){
        /*
        Description:
            Initializes the currentFigure value of the Tile below the Figure

        Parameters: N/A

        Returns: N/A
        */
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -Vector3.up, out hit)) {
            PlayTile startTile = hit.transform.gameObject.GetComponent<PlayTile>();
            if(startTile.isBase){
                startTile.AddFigureToBase(this);
            }else{
                startTile.currentFigure = this;
            }
        }
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
                transform.position = Vector3.Lerp(transform.position, newPosition, performedFrames/movementFramesJump) + JumpVector * 15;
                performedFrames++;
                if(performedFrames == movementFramesJump)
                {
                    performedFrames = 0;
                    movingJump = false;
                    transform.position = newPosition;
                    if(movedToBoat){
                        transform.localPosition = offsetOnBoat[boatSeat];
                        movedToBoat = false;
                    }
                }
                
            }
        }
    }

    public void MoveRegular(PlayTile targetTile)
    {
        /*
        Description:
            Starts the animation for a regular move

        Parameters: PlayTile targetTile: target tile of the move

        Returns: N/A
        */
        movingRegular = true;
        newPosition = new Vector3(targetTile.transform.position.x, targetTile.transform.position.y + y_offset, targetTile.transform.position.z);
    }
    public void MoveJump(PlayTile targetTile)
    {
        /*
        Description:
            Starts the animation for a jump

        Parameters: PlayTile targetTile: target tile of the move

        Returns: N/A
        */
        movingJump = true;
        newPosition = new Vector3(targetTile.transform.position.x, targetTile.transform.position.y + y_offset, targetTile.transform.position.z);
    }
}
