using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator
{
    public static Texture2D textureFromColorMap(Color[] colorMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heightMap)
    {
        //get the width and height using the given noise map.
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        //create a new texture the size of the noise map.
        Texture2D texture = new Texture2D(width, height);

        //create a 1d color array thats the length of the area of the noise map.
        Color[] colorMap = new Color[width * height];
          
        //loop through the width and height. 
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++)
            {
                //for each point in the array set the color.
                // y * width + x is the way to make a 2d array a 1d array.
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }
        return textureFromColorMap(colorMap, width, height);
    }
}
