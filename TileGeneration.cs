using System;
using UnityEngine;

[System.Serializable]
public class TerrainType
{
    public string name;
    public float height;
    public Color color;
    
}
public class TileGeneration : MonoBehaviour
{

    [SerializeField] private TerrainType[] terrainTypes;
    [SerializeField] NoiseMapGeneration noiseMapGeneration;
    [SerializeField] private MeshRenderer tileRenderer;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private float mapScale;
    [SerializeField] private float heightMultiplier;
    [SerializeField] private AnimationCurve heightCurve;
    [SerializeField] private NoiseMapGeneration.Wave[] waves;

    private void UpdateMeshVerticies(float[,] heightMap)
    {
        int tileDepth = heightMap.GetLength(1);
        int tileWidth = heightMap.GetLength(0);

        //Get an array of Vector 3s from the mesh. 
        Vector3[] meshVerticies = this.meshFilter.mesh.vertices;
        
        //iterate through all heightMap coordinates, updating the vertex index
        int vertexIndex = 0;
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                //Get the height
                float height = heightMap[xIndex, zIndex];

                //Get the current Vector3 in the vertex array
                Vector3 vertex = meshVerticies[vertexIndex];

                //Change the height
                meshVerticies[vertexIndex] = new Vector3(vertex.x, height * this.heightCurve.Evaluate(height) * this.heightMultiplier, vertex.z);

                //LOOOP
                vertexIndex++;
            }
        }
        //update the vertices in the mesh and update its properties
        this.meshFilter.mesh.vertices = meshVerticies;
        this.meshFilter.mesh.RecalculateBounds();
        this.meshFilter.mesh.RecalculateNormals();
        //update the mesh collider
        this.meshCollider.sharedMesh = this.meshFilter.mesh;
    }
    
    TerrainType ChooseTerrainType(float height)
    {
        // for each type check if the height is lower than the one for the terrain type
        foreach (TerrainType terrainType in terrainTypes)
        {
            // return the first terrain type whose height is higher than the generated one 
            if (height < terrainType.height)
            {
                return terrainType;
            }
        }
        return terrainTypes[terrainTypes.Length - 1];
    }
    
    public void GenerateTile()
    {
        // calculate tile depth and width based on the mesh vertices
        Vector3[] meshVertices = this.meshFilter.mesh.vertices;
        int tileDepth = (int)Mathf.Sqrt(meshVertices.Length);
        int tileWidth = tileDepth;

        float offsetX = -this.gameObject.transform.position.x;
        float offsetZ = -this.gameObject.transform.position.z;

        // calculate the offsets based on the tile position
        float[,] heightMap = this.noiseMapGeneration.GenerateNoiseMap(tileDepth, tileWidth, this.mapScale, offsetX, offsetZ, waves);
        
        // generate a heightMap using noise
        Texture2D tileTexture = BuildTexture(heightMap);
        this.tileRenderer.material.mainTexture = tileTexture;
        
        UpdateMeshVerticies(heightMap);
    }

    private Texture2D BuildTexture(float[,] heightMap)
    {
        //Get the depth and width of the height map
        int tileDepth = heightMap.GetLength(1);
        int tileWidth = heightMap.GetLength(0);

        //Create an array that stores the colors you want
        Color[] colorMap = new Color[tileDepth * tileWidth];

        //loop through the whole tile
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                // transform the 2D map index into an Array index
                int colorIndex = zIndex * tileWidth + xIndex;
                
                //Get the height by selecting the corresponding value
                float height = heightMap[xIndex, zIndex];
                
                //Choose your terrain type given the current height value
                TerrainType terrainType = ChooseTerrainType(height);
                
                // wherever you are in the color map. make it the terrain color.
                colorMap[colorIndex] = terrainType.color;
            }
        }

        //make a new texture for the tile.
        Texture2D tileTexture = new Texture2D(tileWidth, tileDepth);
        tileTexture.wrapMode = TextureWrapMode.Clamp;
        
        //wrap it
        tileTexture.SetPixels(colorMap);
        tileTexture.Apply();

        return tileTexture;
    }
}