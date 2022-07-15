using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    private float movementFramesRegular = 60;
    private float performedFrames = 0;
    public int playerNr;
    public bool movingRegular = true;
    bool movingJump = false;

    Vector3 newPosition;
    Animator anim;
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
        anim = GetComponent<Animator>();
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
                }
                
            }
            
        }else if(movingJump){

        }
    }

    void MoveRegular(PlayTile targetTile)
    {
        movingRegular = true;
        newPosition = new Vector3(targetTile.transform.position.x, transform.position.y, targetTile.transform.position.z);
        /*
        anim.SetTarget(newPosition);
        anim.StartPlayback();
        */
    }
}
