using UnityEngine;

public class NoiseMapGeneration : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public float seed;
        public float frequency;
        public float amplitude;
    }
    
    public float[,] GenerateNoiseMap(int mapHeight, int mapWidth, float scale, float offsetX, float offsetZ, Wave[] waves) {
        
        //Creates a map of integers between zero and one given a perlin noise value.
        float[,] noiseMap = new float[mapHeight,mapWidth];
        
        for (int x = 0; x < mapHeight; x++) {
            for (int z = 0; z < mapWidth; z++) {
                //get sample values to create this noise map
                float sampleX = (x + offsetX) / scale;
                float sampleZ = (z + offsetZ) / scale;

                float noise = 0f;
                float normalization = 0f;

                foreach (var wave in waves)
                {
                    //generate noise value using perlin noise for a given wave
                    noise += wave.amplitude * Mathf.PerlinNoise(sampleX * wave.frequency + wave.seed,
                        sampleZ * wave.frequency + wave.seed);
                    normalization += wave.amplitude;
                }

                noise /= normalization;
                noiseMap[x, z] = noise;
            }
        }
        return noiseMap;
    }
}
