using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshVertices : MonoBehaviour
{
    public GameObject terrain;
    public GameObject node;
    public GameObject Graph;

    private Vector3[] vertices;
    private List<GameObject> nodes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        vertices = terrain.GetComponent<MeshGenerator>().mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}