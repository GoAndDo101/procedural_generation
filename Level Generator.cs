using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject tilePrefab;
    public int tileCountX;
    public int tileCountY;
    public List<GameObject> allObjects;
    public int OffsetX;
    public int OffsetZ;
    public void GenerateLevel()
    {

        Vector3 tileSize = tilePrefab.GetComponent<MeshRenderer>().bounds.size;
        int tileWidth = (int)tileSize.x;
        int tileDepth = (int)tileSize.z;
        
        for (int x = 0; x < tileCountX; x++)
        {
            for (int z = 0; z < tileCountY; z++)
            {
                Vector3 tilePosition = new Vector3(this.gameObject.transform.position.x + x * tileWidth + OffsetX,
                    this.gameObject.transform.position.y, this.gameObject.transform.position.z + z * tileDepth + OffsetZ);
                
                var tile_object = Instantiate (tilePrefab, tilePosition, Quaternion.identity);
                var tile_generator = tile_object.GetComponent<TileGeneration>();
                tile_generator.GenerateTile();
                allObjects.Add(tile_object);
            }
        }
    }

    public void DeleteLevel()
    {
        foreach (var objects in allObjects)
        {
            DestroyImmediate(objects);
        }
        allObjects.Clear();
        
    }

    
    
    
}
