using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class OceanGeneratorScript : MonoBehaviour
{

    [SerializeField]
    private Shader oceanShader;

    [SerializeField]
    private Material oceanMaterial;

    [SerializeField]
    private int xCount;

    [SerializeField]
    private int zCount;

    [SerializeField]
    private float xScale;

    [SerializeField]
    private float zScale;

    Vector3[] vertices;

    int[] triangles;

    Vector2[] uvs;

    // Start is called before the first frame update
    void Start()
    {
        if (xScale < 1)
        {
            xScale = 1f;
        }
        if (zScale < 1)
        {
            zScale = 1f;
        }
        if (xCount >= 2 && zCount >= 2)
        {
            CreateOcean();
        }

        Vector3 newPos = new Vector3(-xCount/2, -5, 0);

        transform.position = newPos;

        if(gameObject.GetComponent<MeshRenderer>() != null)
        {
            gameObject.GetComponent<MeshRenderer>().material = oceanMaterial;
        }
    }

    private void MeshMake(ref Vector3[] vector, ref int[] triangle, ref Vector2[] uv, Material material)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateBounds();
        mesh.Optimize();
        //mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        transform.gameObject.AddComponent<MeshFilter>();
        transform.gameObject.AddComponent<MeshRenderer>();
        transform.GetComponent<MeshFilter>().mesh = mesh;
        //transform.gameObject.GetComponent<MeshRenderer>().material = material;
    }

    private void CreateOcean()
    {
        vertices = new Vector3[xCount * zCount];
        for (int i = 0; i < zCount; i++)
        {
            for (int j = 0; j < xCount; j++)
            {
                int idx = i * xCount + j;
                vertices[idx] = new Vector3(j * xScale, 0f, i * zScale);
            }
        }
        triangles = new int[(xCount - 1) * (zCount - 1) * 6];
        int id = 0;
        for (int i = 0; i < zCount - 1; i++)
        {
            for (int j = 0; j < xCount - 1; j++)
            {
                triangles[id] = i * xCount + j;
                triangles[id + 1] = triangles[id] + xCount;
                triangles[id + 2] = triangles[id + 1] + 1;

                triangles[id + 3] = triangles[id];
                triangles[id + 4] = triangles[id + 2];
                triangles[id + 5] = triangles[id] + 1;

                id += 6;
            }
        }

        uvs = new Vector2[xCount * zCount];
        for (int i = 0; i < zCount; i++) 
        {
            for(int j = 0;j < xCount; j++)
            {
                int idx = i * xCount + j;
                uvs[idx] = new Vector2(j/(xCount - 1), i/(zCount));
            }
        }

        MeshMake(ref vertices, ref triangles, ref uvs, oceanMaterial);
    }
}
