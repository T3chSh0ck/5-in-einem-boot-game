using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerNumber;
    public Color color;
    public SpawnPoint[] spawnpoints;
    public bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        if (active)
        {
            foreach(SpawnPoint p in spawnpoints)
            {
                p.Spawn();
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
