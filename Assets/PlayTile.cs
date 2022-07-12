using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTile : MonoBehaviour
{
    public Material HoverMaterial;
    public Material NormalMaterial;
    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        rend.material = HoverMaterial;
    }

    void OnMouseExit()
    {
        rend.material = NormalMaterial;
    }
}
