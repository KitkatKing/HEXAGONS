using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
 
    private int CHUNK_SIZE = 3;
    private int CHUNK_SIZE_Y = 4;

    public Vector3Int chunkPosition;
    private Vector3 globalPosition;

    Dictionary<Vector4, Hex> chunkDict = new Dictionary<Vector4, Hex>();



    GameObject gameObject;

    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;

    public Material material;




    public List<Vector3> vertices = new List<Vector3>();

    public List<Vector2> uv = new List<Vector2>();

    public List<int> tris = new List<int>();

    public bool isStartCreateGen = false;

    public bool isVerticesGen = false;

    public bool isMeshGen = false;


    public Chunk(Vector3Int chunkPosition, MeshFilter mesh, Material material)
    {

        this.gameObject = new GameObject("Chunk" + chunkPosition, typeof(MeshFilter), typeof(MeshRenderer));

        this.chunkPosition = chunkPosition;

        this.globalPosition = new Vector3(chunkPosition.x, chunkPosition.y, chunkPosition.z);

        this.isMeshGen = false;

        this.meshFilter = mesh;

        this.material = material;

        Debug.Log("ok");

    }

    public void Ok()
    {
        createAllChunk();
        createVoxels();
        MakeMesh();
    }


    public void MakeMesh()
    {
       
        Mesh mesh = new Mesh();

        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        mesh.vertices = vertices.ToArray();
        Debug.Log(vertices.ToArray().Length + "vert");


        mesh.uv = uv.ToArray();


        Debug.Log(uv.ToArray().Length + "uv" + this.chunkPosition);

        mesh.triangles = tris.ToArray();


        mesh.RecalculateNormals();



        this.gameObject.GetComponent<MeshFilter>().mesh = mesh;

        this.gameObject.GetComponent<MeshRenderer>().material = this.material;

        ColliderCreate();
        

    }

    public void ColliderCreate()
    {

        ColliderRemove();

        this.gameObject.AddComponent<MeshCollider>();

    }

    //Deletes the previous collider WITHOUT making a new one
    public void ColliderRemove()
    {
        Destroy(this.gameObject.GetComponent<MeshCollider>());
    }

    public Vector3 V4toV3(Vector4 v4)
    {
        return (new Vector3(-0.5f, 0, 0) * v4.s) + (new Vector3(0.5f, 0, 0) * v4.q) + (new Vector3(0, 0, -0.88f) * v4.r) + (new Vector3(0, 1, 0) * v4.y);
    }




    public void createAllChunk()
    {
        if(this.chunkPosition.y < 4)
        {
            for (int S = -CHUNK_SIZE; S < CHUNK_SIZE; S++)
            {
                for (int Q = -CHUNK_SIZE; Q < CHUNK_SIZE; Q++)
                {
                    for (int R = -CHUNK_SIZE; R < CHUNK_SIZE; R++)
                    {
                        for (int Y = 0; Y < CHUNK_SIZE_Y; Y++)
                        {

                            voxelCreate(new Vector4(S,Q,R,Y));

                        }
                    }
                }
            }
        }

    }

    public void voxelCreate(Vector4 position)
    {
      //  if (this.chunkDict.ContainsKey(position) == false)
       // {
            this.chunkDict.Add(position, new Hex());
            Debug.Log(position);
      //  }
    }



    public void createVoxels()
    {

        int tc = 0;

        for (int S = -CHUNK_SIZE; S < CHUNK_SIZE; S++)
        {
            for (int Q = -CHUNK_SIZE; Q < CHUNK_SIZE; Q++)
            {
                for (int R = -CHUNK_SIZE; R < CHUNK_SIZE; R++)
                {
                    for (int Y = 0; Y < CHUNK_SIZE_Y; Y++)
                    {
                        Debug.Log("dingle");

                        Vector4 position = new Vector4(S, Q, R, Y);

                     //    Debug.Log(position);

                        //Debug.Log(V4toV3(position));

                       if (chunkDict.ContainsKey(position))
                        {

                            Vector3Int ok;

                            if (!chunkDict.ContainsKey(position + new Vector4(0, 0, 0, 1)))
                            {
                                Debug.Log("YOMAMA");

                                for (int i = 0; i < 6; i++)
                                {
                                    this.vertices.Add(Hexledata.vert[i] + V4toV3(position) * 2);
                                    
                                    this.uv.Add(new Vector3(0.5f, 0.5f, 0.5f));

                                }

                                this.tris.Add(0 + tc);
                                this.tris.Add(1 + tc);
                                this.tris.Add(3 + tc);
                                this.tris.Add(0 + tc);
                                this.tris.Add(3 + tc);
                                this.tris.Add(4 + tc);
                                this.tris.Add(1 + tc);
                                this.tris.Add(2 + tc);
                                this.tris.Add(3 + tc);
                                this.tris.Add(0 + tc);
                                this.tris.Add(4 + tc);
                                this.tris.Add(5 + tc);

                                tc += 6;

                            }
                            /*
                            if ((dict[position] & (1 << 2)) != 0)
                            {



                            }

                            if ((dict[position] & (1 << 3)) != 0)
                            {



                            }

                            if ((dict[position] & (1 << 4)) != 0)
                            {



                            }

                            if ((dict[position] & (1 << 5)) != 0)
                            {



                            }

                            if ((dict[position] & (1 << 6)) != 0)
                            {



                            }

                            if ((dict[position] & (1 << 7)) != 0)
                            {



                            }

                            if ((dict[position] & (1 << 8)) != 0)
                            {



                            }
                            */

                        }
                    }
                }
            }
        }
    }

}
