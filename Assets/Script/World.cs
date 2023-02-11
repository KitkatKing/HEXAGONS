using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Vector2Int WORLD_SIZE = new Vector2Int(256, 24);

    public Chunk[,,] world;

    public Vector3Int worldcheck = new Vector3Int(5, 5, 5);



    void Start()
    {
        world = new Chunk[WORLD_SIZE.x, WORLD_SIZE.x, WORLD_SIZE.y];

        
        for(int x = 0; x < worldcheck.x; x++)
        {
            for(int y = 0; y < worldcheck.y; y++)
            {
                for(int z = 0; z < worldcheck.x; z++)
                {

                    world[x, y, z] = new Chunk(Vector3.zero, gameObject.GetComponent<MeshFilter>()); ;

                }
            }
        }


      //  Chunk chunky = new Chunk(Vector3.zero, gameObject.GetComponent<MeshFilter>());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
