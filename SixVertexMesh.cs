using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make sure whatever you attach this to has a mesh filter.
[RequireComponent(typeof(MeshFilter))]
public class SixVertexMesh : MonoBehaviour
{
    private Mesh mesh;

    private Vector3[] verticies;
    private int[] triangles;
    
    //Whenever you start the game
    private void Awake()
    {
        //get the mesh component.
        mesh = GetComponent<MeshFilter>().mesh;
    }
    
    void Update()
    {
        MakeMeshData();
        CreateMesh();
    }
    
    void MakeMeshData()
    {
        //create an array of verticies
        verticies = new Vector3[]
        {
            new Vector3(0,YValue.ins.yValue,0), new Vector3(0,0,1), new Vector3(1,0,0),
            new Vector3(1,0,0),               new Vector3(0,0,1), new Vector3(1,0,1)
        };
        //create an array of integers
        triangles = new int[] { 0, 1, 2, 3, 4, 5 };
    }

    void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = verticies;
        mesh.triangles = triangles;
        
        mesh.RecalculateNormals();
    }
}