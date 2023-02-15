using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Vector2Int WORLD_SIZE = new Vector2Int(256, 24);

    public Chunk[,,] world;

    public Vector3Int worldcheck;

    public Material material;

    private Vector3Int previous_player_chunk_pos;

    public GameObject ok;

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


        if (Input.GetKeyUp(KeyCode.Space))
        {

          //  PlayerLoad(PlayerToChunk(ok.transform.position), 10, 5, 3, 2);
           
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
          GameObject.Find("Player").transform.position = new Vector3(22, 122, 28);
        }

       // Debug.Log(PlayerToChunk(ok.transform.position));

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


    public Vector3Int PlayerToChunk(Vector3 PlayerPos)
    {

        return new Vector3Int((int)(PlayerPos.x / 32 / 0.75), (int)(PlayerPos.y / 32 / 0.75), (int)(PlayerPos.z / 32 / 0.88));

    }


    private void UpdateChunksAroundPlayer(Vector3Int ChunkPos)
    {
        if (!ChunkPos.Equals(previous_player_chunk_pos))
        {

                previous_player_chunk_pos = ChunkPos;
            PlayerLoad(ChunkPos, 5);
            

        }

    }



    public void PlayerLoad(Vector3Int chunkPosition, int totalSize)
    {
        for (int x = chunkPosition.x - totalSize; x < chunkPosition.x + totalSize; x++)
        {
            for (int y = chunkPosition.y - totalSize; y < chunkPosition.y + totalSize; y++)
            {
                for (int z = chunkPosition.z - totalSize; z < chunkPosition.z + totalSize; z++)
                {
                    if (x >= 0 && y >= 0 && z >= 0 && x < WORLD_SIZE.x && y < WORLD_SIZE.y && z < WORLD_SIZE.x)
                    {
                        if (world[x, y, z].isStartCreateGen == false)
                        {

                            world[x, y, z].VoxelCreationCall();

                            world[x, y, z].isStartCreateGen = true;

                        }

                        if (world[x, y, z].isVerticesGen == false)
                        {

                            world[x, y, z].VerticesGenerationCall();

                            world[x, y, z].isVerticesGen = true;

                        }

                        if (world[x, y, z].isTrisGen == false)
                        {

                            world[x, y, z].UvTriCreationCall();

                            world[x, y, z].isTrisGen = true;

                        }

                        if (world[x, y, z].isMeshGen == false)
                        {

                            world[x, y, z].MeshColliderCall();

                            world[x, y, z].isMeshGen = true;

                        }

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
