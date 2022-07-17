using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generateface: MonoBehaviour
{
    public int resolution = 1;
    public float size = 1;

    public Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    Color[] colors;

    public Gradient gradient;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
    }

    // Update is called once per frame
    void Update()
    {
        CreateShape();
        UpdateMesh();
    }
    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        transform.localScale = new Vector3((float)size / resolution, 0, (float)size / resolution) ;

        mesh.RecalculateNormals();
    }
    void CreateShape()
    {
        vertices = new Vector3[(resolution + 1) * (resolution + 1)];
        for (int i = 0, z = 0; z <= resolution; z++)
        {
            for (int x = 0; x <= resolution; x++)
            {
                float y = 1; //GetNoiseSample(x, z) * noise01Amp;
                vertices[i] = new Vector3(x, y, z);

                /*
                if (y > maxTerrainHeight)
                    maxTerrainHeight = y;
                if (y < minTerrainHeight)
                    minTerrainHeight = y;*/

                i++;
            }
        }
        triangles = new int[resolution * resolution * 6];
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < resolution; z++)
        {
            for (int x = 0; x < resolution; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + resolution + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + resolution + 1;
                triangles[tris + 5] = vert + resolution + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }
        colors = new Color[vertices.Length];
        /*
        for (int i = 0, z = 0; z <= resolution; z++)
        {
            for (int x = 0; x <= resolution; x++)
            {
                float height = Mathf.InverseLerp(0, 2, vertices[i].y);
                colors[i] = gradient.Evaluate(height);
                i++;
            }
        }*/
    }

}
