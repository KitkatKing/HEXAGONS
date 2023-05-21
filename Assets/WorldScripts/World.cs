using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Vector2Int WORLD_SIZE = new Vector2Int(256, 24);

    Dictionary<Vector3Int, Chunk> worldDict = new Dictionary<Vector3Int, Chunk>();

    public Vector3Int worldcheck;

    public Material material;

    private Vector3Int previous_player_chunk_pos;

    public GameObject ok;
 
    public bool firstspace = false;

    Queue ChunkGenQueue = new Queue();


    void Start()
    {
        bruh();
        bruh2();


        //worldDict.Add(new Vector3Int(0,0,0), new Chunk(new Vector3Int(0,0,0), gameObject.GetComponent<MeshFilter>(), material));

        //worldDict[new Vector3Int(0,0,0)].Ok();

    }



    public void bruh2()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int z = 0; z < 10; z++)
                {
                    if (worldDict.ContainsKey(new Vector3Int(x, y, z)))
                    {
                        if (worldDict[new Vector3Int(x, y, z)].isMeshGen == false)
                        {
                            SendChunkToQueue(worldDict[new Vector3Int(x, y, z)]);
                            worldDict[new Vector3Int(x, y, z)].isMeshGen = true;
                        }
                    }
                }
            }
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

                    worldDict.Add(new Vector3Int(x,y,z), new Chunk(new Vector3Int(x, y, z), gameObject.GetComponent<MeshFilter>(), material));

                }
            }
        }
    }




    // Update is called once per frame
    void Update()
    {


       // UpdateChunksAroundPlayer(PlayerToChunk(ok.transform.position));


        if (Input.GetKeyUp(KeyCode.Space) && firstspace == false)
        {
            firstspace = true;
            StartCoroutine(RenderChunks());
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
          GameObject.Find("Player").transform.position = new Vector3(22, 122, 28);
        }

    }

    public Vector3Int PlayerToChunk(Vector3 PlayerPos)
    {

        return new Vector3Int((int)(PlayerPos.x / 32 / 0.75), (int)(PlayerPos.y / 32 / 0.75), (int)(PlayerPos.z / 32 / 0.88));

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
                        if (worldDict.ContainsKey(new Vector3Int(x, y, z)))
                        {
                            if (worldDict[new Vector3Int(x, y, z)].isMeshGen == false)
                            {

                                SendChunkToQueue(worldDict[new Vector3Int(x, y, z)]);
                                worldDict[new Vector3Int(x, y, z)].isMeshGen = true;

                            }

                        }

                    }
                }
            }
        }

    }









    private void UpdateChunksAroundPlayer(Vector3Int ChunkPos)
    {
        if (!ChunkPos.Equals(previous_player_chunk_pos) && firstspace == true)
        {

            previous_player_chunk_pos = ChunkPos;
            PlayerLoad(ChunkPos, 5);

            StartCoroutine(RenderChunks());
        }

    }

    public void SendChunkToQueue(Chunk chunk) { ChunkGenQueue.Enqueue(chunk); }
    public IEnumerator RenderChunks()
    {
        Debug.Log("erm");

        if (ChunkGenQueue.Count != 0)
        {
            foreach (Chunk chunk in ChunkGenQueue.ToArray())
            {
                Debug.Log("WHYYYYYYYYYY");
                chunk.Ok();
                yield return null;
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
