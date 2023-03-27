using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Vector4;

public class Chunk : MonoBehaviour
{
 
    private int CHUNK_SIZE = 16;
    private int CHUNK_SIZE_Y = 16;

    public Vector3Int chunkPosition;
    private Vector3 globalPosition;

    private Hex[] dict = new Hex[35 * 35 * 35 * 35];
    private bool[] dictB = new bool[35 * 35 * 35 * 35];


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


    public Chunk(Vector3Int chunkPos, MeshFilter mesh, Material material)
    {

        this.gameObject = new GameObject("Chunk" + chunkPos, typeof(MeshFilter), typeof(MeshRenderer));

        this.chunkPosition = new Vector3Int(chunkPosition.x, chunkPosition.y, chunkPosition.z);


        if(chunkPos.x % 2 == 0)
        {

                this.globalPosition = new Vector3(chunkPos.x * 0.645f, chunkPos.y * 0.5f, chunkPos.z * 0.795f);
        }
        else
        {

         this.globalPosition = new Vector3(chunkPos.x * 0.645f, chunkPos.y * 0.5f, (chunkPos.z + 0.5f) * 0.795f);

        }

        this.gameObject.transform.position = this.globalPosition * 32;

        this.isMeshGen = false;

        this.meshFilter = mesh;

        this.material = material;

    }

    public void Ok()
    {

        int test = 1347;

        Debug.Log("Testing");
        Debug.Log( V4toArr(ArrtoV4(test)));

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

    public int V4toArr(Vector4 pos)
    {
        pos = pos + new Vector4(17, 17, 17, 1);

        return (pos.s + pos.q * 35 + pos.r * 35 * 35 + pos.y * 35 * 35 * 35);


    }

    public Vector4 ArrtoV4(int pos)
    {

        int y = pos / (35 * 35 * 35);

        int pos2 = pos % (35 * 35 * 35);

        int r = pos2 / (35 * 35);

        int pos3 = pos2 % (35 * 35);

        int q = pos3 / 35;

        int s = pos3 % 35;

        return new Vector4(s, q, r, y) + new Vector4(-17, -17, -17, -1);
    }


    public Vector3 V4toV3(Vector4 v4)
    {
        return (new Vector3(-3f, 0, Mathf.Sqrt(3)) * 0.115f * v4.s) + (new Vector3(3f, 0, Mathf.Sqrt(3)) * 0.115f * v4.q) + (new Vector3(0, 0, -2 * Mathf.Sqrt(3)) * 0.12f * v4.r) + (new Vector3(0, 1, 0) * 0.615f * v4.y);
    }


    public void createBAllChunk()
    {



    }

    public void createAllChunk()
    {

        int counter = 0;

        int max = CHUNK_SIZE * 2 + 1;
        int min = 1;
        int zero = CHUNK_SIZE + 1;
        int total = zero * 3;

        int Q = max;
        int R = min;
        int S = zero;

        while (true)
        {

            counter++;

            int normQ = Q - zero;
            int normR = R - zero;
            int normS = S - zero;

            for(int i = 0; i <= CHUNK_SIZE_Y; i++)
            {
                voxelCreate(new Vector4(normQ, normR, normS, i));
            
            }

            if (R >= Mathf.Min(max, total - Q - min))
            {
                if(Q <= min){ break;}
                else
                {
                    Q = Q - 1;
                    R = Mathf.Max(min, 1 - (Q - zero));
                    S = total - Q - R;
                }

            }
            else
            {
                R += 1;
                S -= 1;
            }

        }

    }

    public void voxelCreate(Vector4 position)
    {
        // this.chunkDict.Add(position, new Hex());

        int ok = V4toArr(position);
        if(ok < dict.Length)
        {
            this.dict[ok] = new Hex();
            this.dictB[ok] = true;
        }
        else
        {
            Debug.Log(ok + " " + position);
        }
    }



    public void createVoxels()
    {

        int tc = 0;

        float blockDist = 1.22f;

        for (int X = 0; X < this.dict.Length; X++)
        {
            if (this.dictB[X] == false) { continue; }

            Vector4 bruh = ArrtoV4(X);


            if (this.dictB[V4toArr(bruh + new Vector4(0, 0, 0, 1))] == false)
            {

                for (int i = 0; i < 6; i++)
                {
                    this.vertices.Add(Hexledata.vert[i] + V4toV3(bruh) * blockDist);

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



            if (this.dictB[V4toArr(bruh + new Vector4(0, 0, 0, -1))] == false)
            {

                for (int i = 6; i < 12; i++)
                {
                    this.vertices.Add(Hexledata.vert[i] + V4toV3(bruh) * blockDist);

                    this.uv.Add(new Vector3(0.25f, 0.25f));

                }

                this.tris.Add(3 + tc);
                this.tris.Add(1 + tc);
                this.tris.Add(0 + tc);
                this.tris.Add(4 + tc);
                this.tris.Add(3 + tc);
                this.tris.Add(0 + tc);
                this.tris.Add(3 + tc);
                this.tris.Add(2 + tc);
                this.tris.Add(1 + tc);
                this.tris.Add(5 + tc);
                this.tris.Add(4 + tc);
                this.tris.Add(0 + tc);

                tc += 6;

            }



            if (this.dictB[V4toArr(bruh + new Vector4(-1, 1, 0, 0))] == false)
            {

                this.vertices.Add(Hexledata.vert[4] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[5] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[11] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[10] + V4toV3(bruh) * blockDist);

                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));

                this.tris.Add(2 + tc);
                this.tris.Add(1 + tc);
                this.tris.Add(0 + tc);
                this.tris.Add(3 + tc);
                this.tris.Add(2 + tc);
                this.tris.Add(0 + tc);

                tc += 4;

            }

            if (this.dictB[V4toArr(bruh + new Vector4(0, 1, -1, 0))] == false)
            {

                this.vertices.Add(Hexledata.vert[3] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[4] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[10] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[9] + V4toV3(bruh) * blockDist);

                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));

                this.tris.Add(2 + tc);
                this.tris.Add(1 + tc);
                this.tris.Add(0 + tc);
                this.tris.Add(3 + tc);
                this.tris.Add(2 + tc);
                this.tris.Add(0 + tc);

                tc += 4;

            }

            if ( this.dictB[V4toArr(bruh + new Vector4(1, 0, -1, 0))] == false)
            {

                this.vertices.Add(Hexledata.vert[2] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[3] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[9] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[8] + V4toV3(bruh) * blockDist);

                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));

                this.tris.Add(2 + tc);
                this.tris.Add(1 + tc);
                this.tris.Add(0 + tc);
                this.tris.Add(3 + tc);
                this.tris.Add(2 + tc);
                this.tris.Add(0 + tc);

                tc += 4;

            }

            if (this.dictB[V4toArr(bruh + new Vector4(1, -1, 0, 0))] == false)
            {

                this.vertices.Add(Hexledata.vert[1] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[2] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[8] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[7] + V4toV3(bruh) * blockDist);

                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));

                this.tris.Add(2 + tc);
                this.tris.Add(1 + tc);
                this.tris.Add(0 + tc);
                this.tris.Add(3 + tc);
                this.tris.Add(2 + tc);
                this.tris.Add(0 + tc);

                tc += 4;

            }

            if (this.dictB[V4toArr(bruh + new Vector4(0, -1, 1, 0))] == false)
            {

                this.vertices.Add(Hexledata.vert[0] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[1] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[7] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[6] + V4toV3(bruh) * blockDist);

                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));

                this.tris.Add(2 + tc);
                this.tris.Add(1 + tc);
                this.tris.Add(0 + tc);
                this.tris.Add(3 + tc);
                this.tris.Add(2 + tc);
                this.tris.Add(0 + tc);

                tc += 4;

            }

            if(this.dictB[V4toArr(bruh + new Vector4(-1, 0, 1, 0))] == false)
            {

                this.vertices.Add(Hexledata.vert[5] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[0] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[6] + V4toV3(bruh) * blockDist);
                this.vertices.Add(Hexledata.vert[11] + V4toV3(bruh) * blockDist);

                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));
                this.uv.Add(new Vector3(0.25f, 0.25f));

                this.tris.Add(2 + tc);
                this.tris.Add(1 + tc);
                this.tris.Add(0 + tc);
                this.tris.Add(3 + tc);
                this.tris.Add(2 + tc);
                this.tris.Add(0 + tc);

                tc += 4;

            }




        }

    }
}
