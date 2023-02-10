using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{

    private Vector3Int CHUNK_SIZE = new Vector3Int(32, 32, 32);

    private Vector3 position;
    private Vector3 globalPosition;

    public Hexagon[,,] chunkData;



    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;

    public Material material;


    public List<Vector3> vertices = new List<Vector3>();

    public List<Vector2> uv = new List<Vector2>();

    public List<int> tris = new List<int>();




    public Chunk(Vector3 position, MeshFilter mesh)
    {
        this.position = position;

        this.chunkData = new Hexagon[CHUNK_SIZE.x, CHUNK_SIZE.y, CHUNK_SIZE.z];

        this.meshFilter = mesh;

        CreateAllHex();
        RemoveOneHex(new Vector3Int(16,16,16));
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




    public void EstablishChunkVertices()
    {
        for (int I = 0; I < CHUNK_SIZE.x; I++)
        {
            for (int J = 0; J < CHUNK_SIZE.y; J++)
            {
                for (int K = 0; K < CHUNK_SIZE.y; K++)
                {
                    if (this.chunkData != null)
                    {

                        if (I % 2 == 0)
                        {

                            vertices.Add(Hexledata.vert[0] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f));
                            vertices.Add(Hexledata.vert[1] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f));
                            vertices.Add(Hexledata.vert[2] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f));
                            vertices.Add(Hexledata.vert[3] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f));
                            vertices.Add(Hexledata.vert[4] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f));
                            vertices.Add(Hexledata.vert[5] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f));


                            vertices.Add(Hexledata.vert[6] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f));
                            vertices.Add(Hexledata.vert[7] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f));
                            vertices.Add(Hexledata.vert[8] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f));
                            vertices.Add(Hexledata.vert[9] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f));
                            vertices.Add(Hexledata.vert[10] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f));
                            vertices.Add(Hexledata.vert[11] + new Vector3(I * 0.75f, J * 0.75f, K * 0.88f));

                        }
                        else
                        {

                            vertices.Add(Hexledata.vert[0] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f));
                            vertices.Add(Hexledata.vert[1] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f));
                            vertices.Add(Hexledata.vert[2] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f));
                            vertices.Add(Hexledata.vert[3] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f));
                            vertices.Add(Hexledata.vert[4] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f));
                            vertices.Add(Hexledata.vert[5] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f));


                            vertices.Add(Hexledata.vert[6] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f));
                            vertices.Add(Hexledata.vert[7] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f));
                            vertices.Add(Hexledata.vert[8] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f));
                            vertices.Add(Hexledata.vert[9] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f));
                            vertices.Add(Hexledata.vert[10] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f));
                            vertices.Add(Hexledata.vert[11] + new Vector3(I * 0.75f, J * 0.75f, (K + 0.5f) * 0.88f));

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
                    if (this.chunkData != null)
                    {

                        uv.Add(Hexledata.uv[0]);
                        uv.Add(Hexledata.uv[1]);
                        uv.Add(Hexledata.uv[2]);
                        uv.Add(Hexledata.uv[3]);
                        uv.Add(Hexledata.uv[4]);
                        uv.Add(Hexledata.uv[5]);

                        uv.Add(Hexledata.uv[6]);
                        uv.Add(Hexledata.uv[7]);
                        uv.Add(Hexledata.uv[8]);
                        uv.Add(Hexledata.uv[9]);
                        uv.Add(Hexledata.uv[10]);
                        uv.Add(Hexledata.uv[11]);

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

                    if (this.chunkData != null)
                    {

                        if (testside(new Vector3Int(I, J, K), 0))
                        {
                            tris.Add(0 + tricount);
                            tris.Add(1 + tricount);
                            tris.Add(3 + tricount);
                            tris.Add(0 + tricount);
                            tris.Add(3 + tricount);
                            tris.Add(4 + tricount);
                            tris.Add(1 + tricount);
                            tris.Add(2 + tricount);
                            tris.Add(3 + tricount);
                            tris.Add(0 + tricount);
                            tris.Add(4 + tricount);
                            tris.Add(5 + tricount);
                        }


                        if (testside(new Vector3Int(I, J, K), 1))
                        {
                            tris.Add(4 + tricount);
                            tris.Add(3 + tricount);
                            tris.Add(9 + tricount);
                            tris.Add(10 + tricount);
                            tris.Add(4 + tricount);
                            tris.Add(9 + tricount);
                        }

                        if (testside(new Vector3Int(I, J, K), 2))
                        {
                            tris.Add(3 + tricount);
                            tris.Add(2 + tricount);
                            tris.Add(8 + tricount);
                            tris.Add(9 + tricount);
                            tris.Add(3 + tricount);
                            tris.Add(8 + tricount);
                        }


                        if (testside(new Vector3Int(I, J, K), 3))
                        {
                            tris.Add(2 + tricount);
                            tris.Add(1 + tricount);
                            tris.Add(7 + tricount);
                            tris.Add(8 + tricount);
                            tris.Add(2 + tricount);
                            tris.Add(7 + tricount);
                        }

                        if (testside(new Vector3Int(I, J, K), 4))
                        {

                            tris.Add(1 + tricount);
                            tris.Add(0 + tricount);
                            tris.Add(6 + tricount);
                            tris.Add(7 + tricount);
                            tris.Add(1 + tricount);
                            tris.Add(6 + tricount);

                        }

                        if (testside(new Vector3Int(I, J, K), 5))
                        {
                            tris.Add(0 + tricount);
                            tris.Add(5 + tricount);
                            tris.Add(11 + tricount);
                            tris.Add(6 + tricount);
                            tris.Add(0 + tricount);
                            tris.Add(11 + tricount);
                        }


                        if (testside(new Vector3Int(I, J, K), 6))
                        {
                            tris.Add(5 + tricount);
                            tris.Add(4 + tricount);
                            tris.Add(10 + tricount);
                            tris.Add(11 + tricount);
                            tris.Add(5 + tricount);
                            tris.Add(10 + tricount);
                        }


                        if (testside(new Vector3Int(I, J, K), 7))
                        {
                            tris.Add(9 + tricount);
                            tris.Add(7 + tricount);
                            tris.Add(6 + tricount);
                            tris.Add(9 + tricount);
                            tris.Add(6 + tricount);
                            tris.Add(10 + tricount);
                            tris.Add(8 + tricount);
                            tris.Add(7 + tricount);
                            tris.Add(9 + tricount);
                            tris.Add(6 + tricount);
                            tris.Add(11 + tricount);
                            tris.Add(10 + tricount);
                        }

                        tricount += 12;

                    }
                }
            }
        }
    }



    public bool testside(Vector3Int position, int side)
    {

        if(side == 0)
        {
            if(position.y + 1 >= CHUNK_SIZE.y)
            {
                Debug.Log("edge hex");
                return true;

            }
            else
            if (chunkData[position.x, position.y + 1, position.z] == null)
            {
                Debug.Log("bruh");
                return true;

            }
            else
            {
                Debug.Log("wh?");
                return false;
            }

        }


        if (side == 1)
        {

            if (position.z - 1 < 0)
            {
                Debug.Log("edge hex");
                return true;

            }
            else
              if (chunkData[position.x, position.y, position.z - 1] == null)
            {
                Debug.Log("bruh");
                return true;

            }
            else
            {
                Debug.Log("wh?");
                return false;
            }

        }


        if(side == 5)
        {

            if (position.x - 1 < 0 )
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
            else
            {
                Debug.Log("wh?");
                return false;
            }



        }

        if (side == 6)
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
            else
            {
                Debug.Log("wh?");
                return false;
            }



        }




        if (side == 4)
        {

            if (position.z + 1 >= CHUNK_SIZE.x)
            {
                Debug.Log("edge hex");
                return true;

            }
            else
              if (chunkData[position.x, position.y, position.z + 1] == null)
            {
                Debug.Log("bruh");
                return true;

            }
            else
            {
                Debug.Log("wh?");
                return false;
            }

        }


        if (side == 2)
        {

            if (position.x + 1 >= CHUNK_SIZE.x)
            {
                Debug.Log("edge hex");
                return true;

            }
            else
            if (chunkData[position.x + 1, position.y, position.z] == null)
            {
                Debug.Log("bruh");
                return true;

            }
            else
            {
                Debug.Log("wh?");
                return false;
            }



        }

        if (side == 3)
        {

            if (position.x + 1 >= CHUNK_SIZE.x)
            {
                Debug.Log("edge hex");
                return true;

            }
            else
            if (chunkData[position.x + 1, position.y, position.z] == null)
            {
                Debug.Log("bruh");
                return true;

            }
            else
            {
                Debug.Log("wh?");
                return false;
            }



        }

        if (side == 7)
        {
            if (position.y - 1 < 0)
            {

                return true;

            }
            else
            if (chunkData[position.x, position.y - 1, position.z] == null)
            {

                return true;

            }
            else
            {
                return false;
            }

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



        meshFilter.mesh = mesh;

        meshRenderer.material = material;


    }


}
