using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Vector2Int WORLD_SIZE = new Vector2Int(256, 24);

    public Chunk[,,] world;

    public Vector3Int worldcheck;

    public Material material;

    public int renderSize;

    public Vector3Int ok;

    void Start()
    {
        world = new Chunk[WORLD_SIZE.x, WORLD_SIZE.y, WORLD_SIZE.x];

        bruh();

    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyUp(KeyCode.Space))
        {

            PlayerLoad(ok);
           

        }

        


    }


    public void bruh()
    {
        for (int x = 0; x < worldcheck.x; x++)
        {
            for (int y = 0; y < worldcheck.y; y++)
            {
                for (int z = 0; z < worldcheck.x; z++)
                {

                    world[x, y, z] = new Chunk(new Vector3Int(x, y, z), gameObject.GetComponent<MeshFilter>(), material);

                }
            }
        }
    }

    public int ClampWorldX(int number)
    {

        return Mathf.Clamp(number, 0, WORLD_SIZE.x - 1);

    }

    public int ClampWorldY(int number)
    {

        return Mathf.Clamp(number, 0, WORLD_SIZE.y - 1);

    }


    public void PlayerLoad(Vector3Int chunkPosition)
    {

        for (int x = ClampWorldX(chunkPosition.x - renderSize); x < ClampWorldX(chunkPosition.x + renderSize); x++)
        {
            for (int y = ClampWorldX(chunkPosition.y - renderSize / 2); y < ClampWorldX(chunkPosition.y + renderSize / 2); y++)
            {
                for (int z = ClampWorldX(chunkPosition.z - renderSize); z < ClampWorldX(chunkPosition.x + renderSize); z++)
                {
                    if(world[x, y, z].vertices.Count == 0)
                    {

                        Debug.Log(world[x, y, z].chunkPosition);
                        
                        world[x, y, z].DoShit();

                    }
                    else
                    {
                        Debug.Log("Nah");
                    }


                }
            }
        }



    }









}
