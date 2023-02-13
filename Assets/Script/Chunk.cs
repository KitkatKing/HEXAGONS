using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
 
    private Vector3Int CHUNK_SIZE = new Vector3Int(32, 32, 32);

    private Vector3Int chunkPosition;
    private Vector3 globalPosition;

    public Hexagon[,,] chunkData;

    GameObject gameObject;

    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;

    public Material material;


    public List<Vector3> vertices = new List<Vector3>();

    public List<Vector2> uv = new List<Vector2>();

    public List<int> tris = new List<int>();




    public Chunk(Vector3Int chunkPosition, MeshFilter mesh, Material material)
    {

        this.gameObject = new GameObject("Chunk" + chunkPosition, typeof(MeshFilter), typeof(MeshRenderer));

        this.chunkPosition = chunkPosition;

        this.globalPosition = new Vector3(chunkPosition.x * 32 * 0.75f, chunkPosition.y * 32 * 0.75f, chunkPosition.z * 32 * 0.94f);

        this.chunkData = new Hexagon[CHUNK_SIZE.x, CHUNK_SIZE.y, CHUNK_SIZE.z];

        this.meshFilter = mesh;

        this.material = material;

        CreateAllHex();
        //CreateOneHex(new Vector3Int(16,16,16));
        EstablishChunkVertices();
        EstablishChunkuvs();
        EstablishChunktris();
        MakeMesh();
    }


    public void CreateAllHex()
    {
        for (int I = 0; I < CHUNK_SIZE.x; I++)
        {
            for (int J = 0; J < CHUNK_SIZE.y; J++)
            {
                for (int K = 0; K < CHUNK_SIZE.y; K++)
                {

                    this.chunkData[I, J, K] = new Hexagon();

                }
            }
        }
    }

    public void RemoveOneHex(Vector3Int pos)
    {

        this.chunkData[pos.x, pos.y, pos.z] = null;

    }


    public void CreateOneHex(Vector3Int pos)
    {

        this.chunkData[pos.x, pos.y, pos.z] = new Hexagon();

    }




    public void EstablishChunkVertices()
    {
        for (int I = 0; I < CHUNK_SIZE.x; I++)
        {
            for (int J = 0; J < CHUNK_SIZE.y; J++)
            {
                for (int K = 0; K < CHUNK_SIZE.y; K++)
                {
                    if (this.chunkData[I,J,K] != null)
                    {

                        if (I % 2 == 0)
                        {

                            this.vertices.Add(Hexledata.vert[0] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[1] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[2] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[3] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[4] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[5] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f) + this.globalPosition);


                            this.vertices.Add(Hexledata.vert[6] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[7] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[8] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[9] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[10] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[11] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f) + this.globalPosition);

                        }
                        else
                        {

                            this.vertices.Add(Hexledata.vert[0] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[1] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[2] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[3] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[4] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[5] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f) + this.globalPosition);


                            this.vertices.Add(Hexledata.vert[6] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[7] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[8] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[9] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[10] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f) + this.globalPosition);
                            this.vertices.Add(Hexledata.vert[11] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f) + this.globalPosition);

                        }

                    }
                 
                }
            }
        }
    }



    public void EstablishChunkuvs()
    {
        for (int I = 0; I < CHUNK_SIZE.x; I++)
        {
            for (int J = 0; J < CHUNK_SIZE.y; J++)
            {
                for (int K = 0; K < CHUNK_SIZE.y; K++)
                {
                    if (this.chunkData[I, J, K] != null)
                    {

                        this.uv.Add(Hexledata.uv[0]);
                        this.uv.Add(Hexledata.uv[1]);
                        this.uv.Add(Hexledata.uv[2]);
                        this.uv.Add(Hexledata.uv[3]);
                        this.uv.Add(Hexledata.uv[4]);
                        this.uv.Add(Hexledata.uv[5]);

                        this.uv.Add(Hexledata.uv[6]);
                        this.uv.Add(Hexledata.uv[7]);
                        this.uv.Add(Hexledata.uv[8]);
                        this.uv.Add(Hexledata.uv[9]);
                        this.uv.Add(Hexledata.uv[10]);
                        this.uv.Add(Hexledata.uv[11]);

                    }
                }
            }
        }
    }


    public void EstablishChunktris()
    {
        int tricount = 0;

        for (int I = 0; I < CHUNK_SIZE.x; I++)
        {
            for (int J = 0; J < CHUNK_SIZE.y; J++)
            {
                for (int K = 0; K < CHUNK_SIZE.y; K++)
                {

                    if (this.chunkData[I, J, K] != null)
                    {

                        if (TestSideOne(new Vector3Int(I, J, K)))
                        {
                            this.tris.Add(0 + tricount);
                            this.tris.Add(1 + tricount);
                            this.tris.Add(3 + tricount);
                            this.tris.Add(0 + tricount);
                            this.tris.Add(3 + tricount);
                            this.tris.Add(4 + tricount);
                            this.tris.Add(1 + tricount);
                            this.tris.Add(2 + tricount);
                            this.tris.Add(3 + tricount);
                            this.tris.Add(0 + tricount);
                            this.tris.Add(4 + tricount);
                            this.tris.Add(5 + tricount);
                        }


                        if (TestSideTwo(new Vector3Int(I, J, K)))
                        {
                            this.tris.Add(4 + tricount);
                            this.tris.Add(3 + tricount);
                            this.tris.Add(9 + tricount);
                            this.tris.Add(10 + tricount);
                            this.tris.Add(4 + tricount);
                            this.tris.Add(9 + tricount);
                        }

                        if (TestSideThree(new Vector3Int(I, J, K)))
                        {
                            this.tris.Add(3 + tricount);
                            this.tris.Add(2 + tricount);
                            this.tris.Add(8 + tricount);
                            this.tris.Add(9 + tricount);
                            this.tris.Add(3 + tricount);
                            this.tris.Add(8 + tricount);
                        }


                        if (TestSideFour(new Vector3Int(I, J, K)))
                        {
                            this.tris.Add(2 + tricount);
                            this.tris.Add(1 + tricount);
                            this.tris.Add(7 + tricount);
                            this.tris.Add(8 + tricount);
                            this.tris.Add(2 + tricount);
                            this.tris.Add(7 + tricount);
                        }

                        if (TestSideFive(new Vector3Int(I, J, K)))
                        {

                            this.tris.Add(1 + tricount);
                            this.tris.Add(0 + tricount);
                            this.tris.Add(6 + tricount);
                            this.tris.Add(7 + tricount);
                            this.tris.Add(1 + tricount);
                            this.tris.Add(6 + tricount);

                        }

                        if (TestSideSix(new Vector3Int(I, J, K)))
                        {
                            this.tris.Add(0 + tricount);
                            this.tris.Add(5 + tricount);
                            this.tris.Add(11 + tricount);
                            this.tris.Add(6 + tricount);
                            this.tris.Add(0 + tricount);
                            this.tris.Add(11 + tricount);
                        }


                        if (TestSideSeven(new Vector3Int(I, J, K)))
                        {
                            this.tris.Add(5 + tricount);
                            this.tris.Add(4 + tricount);
                            this.tris.Add(10 + tricount);
                            this.tris.Add(11 + tricount);
                            this.tris.Add(5 + tricount);
                            this.tris.Add(10 + tricount);
                        }


                        if (TestSideEight(new Vector3Int(I, J, K)))
                        {
                            this.tris.Add(9 + tricount);
                            this.tris.Add(7 + tricount);
                            this.tris.Add(6 + tricount);
                            this.tris.Add(9 + tricount);
                            this.tris.Add(6 + tricount);
                            this.tris.Add(10 + tricount);
                            this.tris.Add(8 + tricount);
                            this.tris.Add(7 + tricount);
                            this.tris.Add(9 + tricount);
                            this.tris.Add(6 + tricount);
                            this.tris.Add(11 + tricount);
                            this.tris.Add(10 + tricount);
                        }

                        tricount += 12;

                    }
                }
            }
        }
    }


    public bool TestSideOne(Vector3Int position)
    {
       // return true;

        if (position.y + 1 >= CHUNK_SIZE.y)
        {
            Debug.Log("edge hex1");
            return true;

        }
        else
        if (chunkData[position.x, position.y + 1, position.z] == null)
        {
            Debug.Log("bruh1");
            return true;

        }

            return false;

    }

    public bool TestSideTwo(Vector3Int position)
    {
       // return false;

        if (position.z - 1 < 0)
        {
            Debug.Log("edge hex2");
            return true;

        }
        else
        if (chunkData[position.x, position.y, position.z - 1] == null)
        {
            Debug.Log("bruh2");
            return true;

        }

            return false;
      
    }

    public bool TestSideThree(Vector3Int position)
    {
       // return false;

        if (position.x % 2 == 0)
        {
            if (position.x + 1 >= CHUNK_SIZE.x || position.z - 1 < 0)
            {
        
                return true;

            }
            else
            if (chunkData[position.x + 1, position.y, position.z - 1] == null)
            {

                return true;

            }

        }
        else
        {
            if (position.x + 1 >= CHUNK_SIZE.x)
            {

                return true;

            }
            else
            if (chunkData[position.x + 1, position.y, position.z] == null)
            {

                return true;

            }

        }

        return false;

    }

    public bool TestSideFour(Vector3Int position)
    {
       // return false;

        if (position.x % 2 == 0)
        {
            if (position.x + 1 >= CHUNK_SIZE.x)
            {
                Debug.Log("edge hex4");
                return true;

            }
            else
            if (chunkData[position.x + 1, position.y, position.z] == null)
            {
                Debug.Log("bruh4");
                return true;

            }

        }
        else
        {
            if (position.x + 1 >= CHUNK_SIZE.x || position.z + 1 >= CHUNK_SIZE.x)
            {
                Debug.Log("edge hex4");
                return true;

            }
            else
            if (chunkData[position.x + 1, position.y, position.z + 1] == null)
            {
                Debug.Log("bruh4");
                return true;

            }

        }

        return false;

    }

    public bool TestSideFive(Vector3Int position)
    {
       // return false;

        if (position.z + 1 >= CHUNK_SIZE.x)
        {
            Debug.Log("edge hex5");
            return true;

        }
        else
        if (chunkData[position.x, position.y, position.z + 1] == null)
        {
            Debug.Log("bruh");
            return true;

        }

        return false;

    }

    public bool TestSideSix(Vector3Int position)
    {
       // return false;

        if (position.x % 2 == 0)
        {
            if (position.x - 1 < 0)
            {
                Debug.Log("edge hex");
                return true;

            }
            else
            if (chunkData[position.x - 1, position.y, position.z] == null)
            {
                Debug.Log("bruh");
                return true;

            }

        }
        else
        {
            if (position.x - 1 < 0 || position.z + 1 >= CHUNK_SIZE.x)
            {
                Debug.Log("edge hex");
                return true;

            }
            else
            if (chunkData[position.x - 1, position.y, position.z + 1] == null)
            {
                Debug.Log("bruh");
                return true;

            }


        }

        return false;

    }

    public bool TestSideSeven(Vector3Int position)
    {
       // return false;

        if (position.x % 2 == 0)
        {
            if (position.x - 1 < 0 || position.z - 1 < 0)
            {
                Debug.Log("edge hex");
                return true;

            }
            else
            if (chunkData[position.x - 1, position.y, position.z - 1] == null)
            {
                Debug.Log("bruh");
                return true;

            }

        }
        else
        {
            if (position.x - 1 < 0)
            {
                Debug.Log("edge hex");
                return true;

            }
            else
            if (chunkData[position.x - 1, position.y, position.z] == null)
            {
                Debug.Log("bruh");
                return true;

            }

        }

        return false;

    }

    public bool TestSideEight(Vector3Int position)
    {
       // return false;

        if (position.y - 1 < 0)
        {

            return true;

        }
        else
        if (chunkData[position.x, position.y - 1, position.z] == null)
        {

            return true;

        }

            return false;
        
    }



    public void MakeMesh()
    {
        Mesh mesh = new Mesh();

        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        mesh.vertices = vertices.ToArray();

        mesh.uv = uv.ToArray();

        mesh.triangles = tris.ToArray();

        mesh.RecalculateNormals();



        this.gameObject.GetComponent<MeshFilter>().mesh = mesh;

        this.gameObject.GetComponent<MeshRenderer>().material = this.material;


    }


}
