using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshVertices : MonoBehaviour
{
    public GameObject terrain;

    private Vector3[] vertices;

    // Start is called before the first frame update
    void Start()
    {
        vertices = terrain.GetComponent<Generateface>().mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}