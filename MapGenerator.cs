using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour{

	public enum DrawMode
	{
		NoiseMap,
		ColorMap,
		Mesh
	}

	public AnimationCurve meshHeightCurve;
	public float meshHeightMultiplier;
	public DrawMode drawMode;
	
	const int MapChunkSize = 241;
	[Range(0,6)]
	public int levelOfDetail;
	public float noiseScale;
	public int octaves;
	public float lacunarity;
	[Range(0,1)]
	public float persistence;
	public bool autoUpdate;
	public int seed;
	public Vector2 offset;

	public TerrainType[] regions;
	// calls our generate noise map function
	public void GenerateMap(){
		float[,] noiseMap = Noise.generateNoiseMap(MapChunkSize, MapChunkSize, noiseScale, octaves, persistence, lacunarity, offset, seed);
		
		Color[] colorMap = new Color[MapChunkSize * MapChunkSize];
		for (int y = 0; y < MapChunkSize; y++)
		{
			for (int x = 0; x < MapChunkSize; x++)
			{
				float currentHeight = noiseMap[x, y];
				for (int i = 0; i < regions.Length; i++)
				{
					if (currentHeight <= regions[i].height)
					{
						colorMap[y * MapChunkSize + x] = regions[i].color;
						break;
					}
				}
			}
		}
		//creates a new display class.
		MapDisplay display = FindObjectOfType<MapDisplay>();

		if (drawMode == DrawMode.NoiseMap)
		{
			display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
		}
		else if (drawMode == DrawMode.ColorMap)
		{
			display.DrawTexture(TextureGenerator.textureFromColorMap(colorMap, MapChunkSize, MapChunkSize));
		}
		else if (drawMode == DrawMode.Mesh)
		{
			display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail),
				TextureGenerator.textureFromColorMap(colorMap, MapChunkSize, MapChunkSize));
		}
		//draws the noise map.
		
	}

	//clamp the octaves, map width, map height, and lacunarity. 
	void OnValidate()
	{

		if (octaves < 0) {
			octaves = 0;
		}

		if (lacunarity < 1) {
			lacunarity = 1;
		}
	}

	[System.Serializable]
	public struct TerrainType
	{
		public string name;
		public float height;
		public Color color;
	}
}


