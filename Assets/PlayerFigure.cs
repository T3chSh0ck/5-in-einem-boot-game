using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFigure : MonoBehaviour
{
    public int player = 0;
    public Material p0;
    public Material p1;
    public Material p2;
    public Material p3;
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer meshRenderer = this.GetComponentsInChildren<MeshRenderer>()[0];
        switch (player)
        {
            case 0: //Rot
                meshRenderer.material = p0;
                break;
            case 1:
                meshRenderer.material = p1;
                break;
            case 2:
                meshRenderer.material = p2;
                break;
            case 3:
                meshRenderer.material = p3;
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
