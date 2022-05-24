using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTile : MonoBehaviour
{
    public Material material_inactive;
    public Material material_active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = material_active;
    }

    void OnMouseExit()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = material_inactive;
    }
}
