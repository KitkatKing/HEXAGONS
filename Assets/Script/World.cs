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

    public GameObject ok;
    private Vector3Int previous_player_chunk_pos;

    void Start()
    {
        Logger.Log("world loaded");
        world = new Chunk[WORLD_SIZE.x, WORLD_SIZE.y, WORLD_SIZE.x];

        bruh();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateChunksAroundPlayer(PlayerToChunk(ok.transform.position));
        
        if(Input.GetKeyUp(KeyCode.Space))
        {

            PlayerLoad(PlayerToChunk(ok.transform.position));
           
        }
        if (Input.GetKeyDown(KeyCode.A)) {
          GameObject.Find("Player").transform.position = new Vector3(22, 122, 28);
        }

       // Debug.Log(PlayerToChunk(ok.transform.position));

    }

    private void UpdateChunksAroundPlayer(Vector3Int ChunkPos) {
        if (!ChunkPos.Equals(previous_player_chunk_pos)) {
            previous_player_chunk_pos = ChunkPos;
            PlayerLoad(ChunkPos);
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

    public Vector3Int PlayerToChunk(Vector3 PlayerPos)
    {

        return new Vector3Int((int)(PlayerPos.x / 32 / 0.75), (int)(PlayerPos.y / 32 / 0.75), (int)(PlayerPos.z / 32 / 0.88));

    }



    public void PlayerLoad(Vector3Int chunkPosition)
    {

        for (int x = ClampWorldX(chunkPosition.x - renderSize); x < ClampWorldX(chunkPosition.x + renderSize); x++)
        {
            for (int y = ClampWorldY(chunkPosition.y - renderSize / 2); y < ClampWorldY(chunkPosition.y + renderSize / 2); y++)
            {
                for (int z = ClampWorldX(chunkPosition.z - renderSize); z < ClampWorldX(chunkPosition.z + renderSize); z++)
                {
                    if(world[x, y, z].vertices.Count == 0)
                    {

                        Debug.Log(chunkPosition);
                        
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


    public Vector3Int ChunkBlockToWorld(Vector3Int chunk, Vector3Int block)
    {

        return (chunk * 32 + block);

    }

    public Vector3Int[] WorldToChunkBlock(Vector3Int world)
    {
        Vector3Int chunk = new Vector3Int(world.x / 32, world.y / 32, world.z / 32);

        Vector3Int block = new Vector3Int(world.x % 32, world.y % 32, world.z % 32);

        return new Vector3Int[] {chunk, block};
    }

}
