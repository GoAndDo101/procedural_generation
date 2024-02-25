using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
     public Renderer textureRender;
     public MeshFilter meshFilter;
     public MeshRenderer meshRenderer;

     public void DrawTexture(Texture2D texture)
     {
          //whatever this texture renderer is attached to. Change that texture to this texture. 
          textureRender.sharedMaterial.mainTexture = texture;
          
          //set the scale of whatever were displaying to the dimensions of the color array.
          textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
     }

     public void DrawMesh(MeshData meshData, Texture2D texture)
     {
          meshFilter.sharedMesh = meshData.CreateMesh();
          meshRenderer.sharedMaterial.mainTexture = texture;
     }

}
