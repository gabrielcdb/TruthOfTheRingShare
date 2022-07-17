using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    public Mesh mesh;
    public GameObject node;
    private List<GameObject> nodes = new List<GameObject>();
    public GameObject Graph;

    //public GameObject showVertice;

    Vector3[] vertices;
    int[] triangles;
    Color[] colors;

    public int xSize = 20;
    public int zSize = 20;

    public float offsetX = 100f;
    public float offsetZ = 100f;

    public int textureWidth = 1024;
    public int textureHeight = 1024;

    public float noise01Scale = 2f;
    public float noise01Amp = 2f;

    public float noise03Scale = 6f;
    public float noise03Amp = 6f;
    
    float minTerrainHeight;
    float maxTerrainHeight;

    public Gradient gradient;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        offsetX = Random.Range(0f, 9999f);
        offsetZ = Random.Range(0f, 9999f);

        CreateShape();
        UpdateMesh();
        CreateVertices();
    }
    private void Update()
    {

    }
    private void drawVertices()
    {
        foreach (Vector3 vertice in vertices)
        {

        }
    }
    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = GetNoiseSample(x, z) * noise01Amp;
                vertices[i] = new Vector3(x, y, z);

                if (y > maxTerrainHeight)
                    maxTerrainHeight = y;
                if (y < minTerrainHeight)
                    minTerrainHeight = y;

                i++;
            }
        }
        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;
        for (int z = 0; z< zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }
        colors = new Color[vertices.Length];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[i].y);
                colors[i] = gradient.Evaluate(height);
                i++;
            }
        }
    }

    private float GetNoiseSample(int x, float z)
    {
        return Mathf.PerlinNoise(x *0.3f  + offsetX, z *0.3f + offsetZ) * 2f;
    }

    void CreateVertices()
    {
        int count = 0;
        foreach (Vector3 vertice in vertices)
        {
            count++;
            GameObject sph = Instantiate(node, vertice, transform.rotation);
            sph.transform.parent = Graph.transform;
            sph.name = "Node " + count.ToString();
            nodes.Add(sph);
        }

        RaycastHit hit;
        foreach (GameObject node in nodes)
        {
            for (int i = -1, z = 0; z <= 1; z++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    Vector3 nodeToHit = new Vector3(node.transform.position.x + x, -50f, node.transform.position.z + z);
                    if ((x, z) != (0, 0) && Physics.Raycast(nodeToHit, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity))
                    {
                        node.GetComponent<Node>().add_node(hit.collider.GetComponent<Node>());
                    }
                }
            }
        }
    }
}
