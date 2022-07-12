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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(playing_field_states);
    }

    void UpdatePlacements()
    {

    }

    /*PlayTile getTileAtCoordinates(int x, int y)
    {

    }*/

    int[] convertCoordinatesToBoard(int x, int y)
    {
        // realx  =  22boardx - 66
        // realy  =  22boardy - 198
        // Umstellen nach board-koordinaten
        // boardx = (realx + 66 )/22
        // boardy = (realy + 198)/22

        int[] boardCoords = new int[2] {(x - origin[0])/22, (y - origin[1])/22};
        return boardCoords;
    }

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

}
