using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour
{
    private Mesh mesh;

    private Vector3[] verticies;

    private int[] triangles;
    
    //grid settings
    public float cellSize = 1;
    public Vector3 gridOffSet;
    public int gridSize;
    // Start is called before the first frame update
    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Start()
    {
        MakeProceduralGrid();
        UpdateMesh();
        
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void MakeProceduralGrid()
    {
        //Quad: 4 vertecies, 6 triangles.
        // set our array sizes
        verticies = new Vector3[4];
        triangles = new int[6];
        
        //set vertex offset
        float vertexOffset = cellSize * 0.5f;
        
        //populate the verticies and triangles of a quad. 
        verticies[0] = new Vector3(-vertexOffset, 0, -vertexOffset) + gridOffSet;
        verticies[1] = new Vector3(-vertexOffset, 0,  vertexOffset) + gridOffSet;
        verticies[2] = new Vector3( vertexOffset, 0, -vertexOffset) + gridOffSet;
        verticies[3] = new Vector3( vertexOffset, 0,  vertexOffset) + gridOffSet;

        //set the triangles array
        triangles[0] = 0;
        triangles[1] = triangles[4] = 1;
        triangles[2] = triangles[3] = 2;
        triangles[5] = 3;
    }
}
