using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour{
	public int mapWidth;
	public int mapHeight;
	public float noiseScale;
	public int octaves;
	public float lacunarity;
	[Range(0,1)]
	public float persistence;
	public bool autoUpdate;
	public int seed;
	public Vector2 offset;
	
	
	public void GenerateMap(){
		float[,] noiseMap = Noise.generateNoiseMap(mapWidth, mapHeight, noiseScale, octaves, persistence, lacunarity, offset, seed);

		MapDisplay display = FindObjectOfType<MapDisplay>();
		display.DrawNoiseMap (noiseMap);
	}

	private void OnValidate()
	{
		if (mapWidth < 1) {
			mapWidth = 1;
		}

		if (mapHeight < 1) {
			mapHeight = 1;
		}

		if (octaves < 0) {
			octaves = 0;
		}

		if (lacunarity < 1) {
			lacunarity = 1;
		}
	}
}


