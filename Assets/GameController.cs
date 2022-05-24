using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int[] origin = new int[2] {-176, -44};
    /*Field states:
     * null: no field
     * 0: free field
     * 1: player 1
     * 2: player 2
     * 3: player 3
     * 4: player 4
     */
    private int?[,] playing_field_states = new int?[18, 18]{ {null,null,null,null,null,null,null,   0,   0,   0,   0,null,null,null,null,null,null,null},
                                                             {null,null,null,null,null,null,null,   0,   0,   0,   0,null,null,null,null,null,null,null},
                                                             {null,null,null,null,null,null,null,null,   0,   0,null,null,null,null,null,null,null,null},
                                                             {null,null,null,null,null,null,null,null,   0,   0,null,null,null,null,null,null,null,null},
                                                             {null,null,null,null,null,null,null,null,   0,   0,null,null,   3,null,null,null,null,null},
                                                             {null,null,null,null,   2,   2,   0,   2,   0,   2,   0,   0,   3,null,null,null,null,null},
                                                             {null,null,null,null,null,   0,   0,   0,   0,   0,   0,   0,   0,null,null,null,null,null},
                                                             {   0,   0,null,null,null,   0,   0,null,null,null,null,   0,   3,null,null,null,   0,   0},
                                                             {   0,   0,   0,   0,   0,   1,   0,null,null,null,null,   0,   0,   0,   0,   0,   0,   0},
                                                             {   0,   0,   0,   0,   0,   0,   0,null,null,null,null,   0,   3,   0,   0,   0,   0,   0},
                                                             {   0,   0,null,null,null,   1,   0,null,null,null,null,   0,   0,null,null,null,   0,   0},
                                                             {null,null,null,null,null,   0,   0,   0,   0,   0,   0,   0,   0,null,null,null,null,null},
                                                             {null,null,null,null,null,   1,   0,   0,   4,   0,   4,   0,   4,   4,null,null,null,null},
                                                             {null,null,null,null,null,   1,null,null,   0,   0,null,null,null,null,null,null,null,null},
                                                             {null,null,null,null,null,null,null,null,   0,   0,null,null,null,null,null,null,null,null},
                                                             {null,null,null,null,null,null,null,null,   0,   0,null,null,null,null,null,null,null,null},
                                                             {null,null,null,null,null,null,null,   0,   0,   0,   0,null,null,null,null,null,null,null},
                                                             {null,null,null,null,null,null,null,   0,   0,   0,   0,null,null,null,null,null,null,null}};
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playing_field_states);
    }

    void UpdatePlacements()
    {

    }

    /*PlayTile getTileAtCoordinates(int x, int y)
    {

    }*/

    int[] convertCoordinates(int x, int y)
    {
        int[] realCoords = new int[2] {origin[0] + x * 22, origin[1] + y * 22 };
        return realCoords;
    } 

    void RotateBoats()
    {
        // boats array: [boat number, starting coordinates, direction]
        
        int[,,] boats = new int[,,] { { { 0, 7 }, { 0, 1 } },
                                      { { 7, 17}, { 1, 0 } },
                                      { {17, 10}, { 0, -1} },
                                      { {10,  0}, { -1, 0} }};
        int?[] swap = new int?[4];
        for (int boat = 0; boat < 4; boat++)
        {
            for (int seat = 0; seat < 4; seat++)
            {
                swap[seat] = playing_field_states[(boats[boat + 1, 0, 0] + boats[boat + 1, 1, 0] * seat), (boats[boat + 1, 0, 1] + boats[boat + 1, 1, 1] * seat)];
                if (boat == 0)
                {
                    playing_field_states[(boats[boat + 1, 0, 0] + boats[boat + 1, 1, 0] * seat), (boats[boat + 1, 0, 1] + boats[boat + 1, 1, 1] * seat)] = playing_field_states[(boats[boat, 0, 0] + boats[boat, 1, 0] * seat), (boats[boat, 0, 1] + boats[boat, 1, 1] * seat)];
                }
                else
                {
                    playing_field_states[(boats[boat + 1, 0, 0] + boats[boat + 1, 1, 0] * seat), (boats[boat + 1, 0, 1] + boats[boat + 1, 1, 1] * seat)] = swap[seat];
                }
            }
        }
    }

}

