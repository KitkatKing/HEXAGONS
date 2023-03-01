using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Vector4;

public class Chunk : MonoBehaviour
{
 
    private int CHUNK_SIZE = 16;
    private int CHUNK_SIZE_Y = 16;

    public Vector3Int chunkPosition;
    private Vector3 globalPosition;

    Dictionary<Vector4, Hex> chunkDict = new Dictionary<Vector4, Hex>(new Vector4EqualityComparer());



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
        return (new Vector3(-3f, 0, Mathf.Sqrt(3)) * 0.12f * v4.s) + (new Vector3(3f, 0, Mathf.Sqrt(3)) * 0.12f * v4.q) + (new Vector3(0, 0, -2 * Mathf.Sqrt(3)) * 0.12f * v4.r) + (new Vector3(0, 1, 0) * v4.y);
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

        foreach (KeyValuePair<Vector4, Hex> attachStat in this.chunkDict)
        {
            //Now you can access the key and value both separately from t$$anonymous$$s attachStat as:
            //Debug.Log(attachStat.Key);
           // Debug.Log(attachStat.Value);
        }

    }

    public void voxelCreate(Vector4 position)
    {
        if (this.chunkDict.ContainsKey(position) == false)
        {
            this.chunkDict.Add(position, new Hex());
          //  Debug.Log(position);
        }
    }



    public void createVoxels()
    {

        int tc = 0;

        for (int Y = 0; Y < CHUNK_SIZE_Y; Y++)
        {

            for (int S = -CHUNK_SIZE; S < CHUNK_SIZE; S++)
            {
                 for (int Q = -CHUNK_SIZE; Q < CHUNK_SIZE; Q++)
                 {
                      for (int R = -CHUNK_SIZE; R < CHUNK_SIZE; R++)
                      {
                   
                        Vector4 position = new Vector4(S, Q, R, Y);

                       if (chunkDict.ContainsKey(position))
                        {
                            
                            if (!chunkDict.ContainsKey(position + new Vector4(0, 0, 0, 1)))
                            {

                                for (int i = 0; i < 6; i++)
                                {
                                    this.vertices.Add(Hexledata.vert[i] + V4toV3(position) * 2);
                                    
                                    this.uv.Add(new Vector3(0.25f, 0.25f));

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


                            if (!chunkDict.ContainsKey(position + new Vector4(0, -1, 1, 0)))
                            {

                                for (int i = 0; i < 6; i++)
                                {
                                    this.vertices.Add(Hexledata.vert[i] + V4toV3(position) * 2);

                                    this.uv.Add(new Vector3(0.25f, 0.25f));

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
                            if (!chunkDict.ContainsKey(position + new Vector4(0, 0, 0, -1)))
                            {

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

                            

                            if (!chunkDict.ContainsKey(position + new Vector4(1, -1, 0, 0)))
                            {

                                
                                this.vertices.Add(Hexledata.vert[4] + V4toV3(position) * 2);
                                this.vertices.Add(Hexledata.vert[3] + V4toV3(position) * 2);
                                this.vertices.Add(Hexledata.vert[10] + V4toV3(position) * 2);
                                this.vertices.Add(Hexledata.vert[9] + V4toV3(position) * 2);

                                this.uv.Add(new Vector3(0.5f, 0.5f, 0.5f));
                                this.uv.Add(new Vector3(0.5f, 0.5f, 0.5f));
                                this.uv.Add(new Vector3(0.5f, 0.5f, 0.5f));
                                this.uv.Add(new Vector3(0.5f, 0.5f, 0.5f));



                                this.tris.Add(0 + tc);
                                this.tris.Add(1 + tc);
                                this.tris.Add(3 + tc);
                                this.tris.Add(0 + tc);
                                this.tris.Add(3 + tc);
                                this.tris.Add(2 + tc);


                                tc += 4;

                            }
                            */

                            Debug.Log(!chunkDict.ContainsKey(position + new Vector4(-1, 1, 0, 0)));

 
                       }
                      }
                 }
            }
        }
    }

}
