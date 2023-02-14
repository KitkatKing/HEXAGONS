using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
 
    private Vector3Int CHUNK_SIZE = new Vector3Int(32, 32, 32);

    public Vector3Int chunkPosition;
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

        this.globalPosition = new Vector3(chunkPosition.x * 32 * 0.75f, chunkPosition.y * 32 * 0.75f, chunkPosition.z * 32 * 0.88f);

        this.chunkData = new Hexagon[CHUNK_SIZE.x, CHUNK_SIZE.y, CHUNK_SIZE.z];

        this.meshFilter = mesh;

        this.material = material;


    }

    public void DoShit()
    {
        if(chunkPosition.y < 3)
        {
            Logger.Log("did something");
            CreateAllHex();
        }

        //CreateOneHex(new Vector3Int(16,16,16));
        EstablishChunkVertices();
        EstablishChunkuvs();
        generate_tris();
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



    // adds vertices for each hexagon in the chunk to the list of vertices
    // NOTE: DO NOT CALL SUCCESSIVELY WITHOUT CLEARING VERTICES
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



    // like the version for vertices, but generates the proper uvs for each of them
    // NOTE: ALSO DO NOT CALL THIS TWICE SUCCESSIVELY WITHOUT CLEARING UVS
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


    private void generate_tris() {
      // each hexagon is aligned to 12 vertices, and will be
      // incremented each iteration by said amount to ensure
      // the correct vertices are used.
      int tc = 0;

      for (int x = 0; x < CHUNK_SIZE.x; x++) {
        for (int y = 0; y < CHUNK_SIZE.y; y++) {
          for (int z = 0; z < CHUNK_SIZE.z; z++) {
            if (chunkData[x, y, z] != null) {
              if (does_hex_y_plus_exist(x, y, z)) {
                generate_y_plus_tris(tc);
              }
              if (does_hex_y_minus_exist(x, y, z)) {
                generate_y_minus_tris(tc);
              }
              if (does_hex_z_plus_exist(x, y, z)) {
                generate_z_plus_tris(tc);
              }
              if (does_hex_z_minus_exist(x, y, z)) {
                generate_z_minus_tris(tc);
              }

              int test = (x % 2);
              if (test == 0) {
                if (does_hex_x_plus_z_plus_exist(x, y, z)) {
                  generate_x_plus_z_plus_tris(tc);
                }
              } else if (test == 1) {
                if (does_hex_x_plus_z_plus_exist(x, y, z + 1)) {
                  generate_x_plus_z_plus_tris(tc);
                }
              } else {
                Logger.LogErrorFormat("x % 2 somehow didn't return either 0 or 1 in generate_tris(): {0}", test);
              }

              // neat trick that works because:
              // A: the value of "x % 2" will always be either 0 or 1, and
              // B: if it's 0 the z value is left alone, otherwise we add 1 to it.
              // this did require adding a check within the generate_x_*_tris functions however
              // to ensure the z value doesn't go out of bounds.
              if (does_hex_x_minus_z_plus_exist(x, y, z + (x % 2))) {
                generate_x_minus_z_plus_tris(tc);
              }
              // necessary to subtract 1 from "x % 2" so 0 -> -1 and 1 -> 0
              // effectively reversing the condition's effect in addition to
              // subtracting from the z coordinate.
              if (does_hex_x_plus_z_minus_exist(x, y, z + (x % 2) - 1)) {
                generate_x_plus_z_minus_tris(tc);
              }
              if (does_hex_x_minus_z_minus_exist(x, y, z + (x % 2) - 1)) {
                generate_x_minus_z_minus_tris(tc);
              }

              tc += 12;
            }
          }
        }
      }
    }

    // +==================+
    // | DOES_HEX_*_EXIST |
    // +==================+

    // reason for using 3 integers rather than a vector is nothing
    // specific to vectors is in this function or returned by it
    private bool does_hex_y_plus_exist(int x, int y, int z) {
      // double bars are necessary to avoid an IndexOutOfBounds exception by checking the left side first
      if ((y + 1 >= CHUNK_SIZE.y) || (chunkData[x, y + 1, z] == null)) {
        return true;
      } else {
        return false;
      }
    }
    private bool does_hex_y_minus_exist(int x, int y, int z) {
      // further reduction of the previous because:
      // "if condition { return true; } else { return false; }" == "return condition"

      // note the double bars "||" are necessary because it evaluates the
      // right condition only if the left condition evaluates to false,
      // compared to single bar "|" which checks both.
      return ((y <= 0) ||
              (chunkData[x, y - 1, z] == null));
    }

    private bool does_hex_z_plus_exist(int x, int y, int z) {
      return ((z + 1 >= CHUNK_SIZE.z) ||
              (chunkData[x, y, z + 1] == null));
    }
    private bool does_hex_z_minus_exist(int x, int y, int z) {
      return ((z <= 0) ||
              (chunkData[x, y, z - 1] == null));
    }

    private bool does_hex_x_plus_z_plus_exist(int x, int y, int z) {
      return (((x + 1 >= CHUNK_SIZE.x) ||
              // this check is necessary because we add to the value
              // of z, depending on which x hexagon layer it is in,
              // in the generate_tris function.
               (z >= CHUNK_SIZE.z)) ||
              (chunkData[x + 1, y, z] == null));
    }
    private bool does_hex_x_minus_z_plus_exist(int x, int y, int z) {
      return (((x <= 0) ||
               (z >= CHUNK_SIZE.z)) ||
              (chunkData[x - 1, y, z] == null));
    }
    private bool does_hex_x_plus_z_minus_exist(int x, int y, int z) {
      return (((x + 1 >= CHUNK_SIZE.x) ||
               (z <= 0)) ||
              (chunkData[x + 1, y, z] == null));
    }
    private bool does_hex_x_minus_z_minus_exist(int x, int y, int z) {
      return (((x <= 0) ||
               (z < 0)) ||
              (chunkData[x - 1, y, z] == null));
    }


    private void generate_y_plus_tris(int tc) { 
      tris.Add(0 + tc);
      tris.Add(1 + tc);
      tris.Add(3 + tc);
      tris.Add(0 + tc);
      tris.Add(3 + tc);
      tris.Add(4 + tc);
      tris.Add(1 + tc);
      tris.Add(2 + tc);
      tris.Add(3 + tc);
      tris.Add(0 + tc);
      tris.Add(4 + tc);
      tris.Add(5 + tc);
    }
    private void generate_y_minus_tris(int tc) { 
      // practically equivalent to the above, minus the contents of the array,
      // but shorter to write and change.
      int[] tlist = {9, 7, 6,
                     9, 6, 10,
                     8, 7, 9,
                     6, 11, 10};
      foreach (int i in tlist) {
        tris.Add(i + tc);
      }
    }

    private void add_tris(int i) {
      tris.Add(i);
    }
    private void generate_z_plus_tris(int tc) {
      // discovered how to declare anonymous arrays with predefined elements
      foreach (int i in new int[] {1, 0, 6,
                                   7, 1, 6}) {
        add_tris(i + tc);
      }
    }
    private void generate_z_minus_tris(int tc) {
      foreach (int i in new int[] {4, 3, 9,
                                   10, 4, 9}) {
        add_tris(i + tc); } }
    private void generate_x_plus_z_plus_tris(int tc) {
      foreach (int i in new int[] {2, 1, 7,
                                   8, 2, 7}) {
        add_tris(i + tc); } }
    private void generate_x_plus_z_minus_tris(int tc) {
      foreach (int i in new int[] {3, 2, 8,
                                   9, 3, 8}) {
        add_tris(i + tc); } }
    private void generate_x_minus_z_plus_tris(int tc) {
      foreach (int i in new int[] {0, 5, 11,
                                   6, 0, 11}) {
        add_tris(i + tc); } }
    private void generate_x_minus_z_minus_tris(int tc) {
      foreach (int i in new int[] {5, 4, 10,
                                   11, 5, 10}) {
        add_tris(i + tc); } }


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

        ColliderCreate();


    }

    public void ColliderCreate()
    {

        Destroy(this.gameObject.GetComponent<MeshCollider>());

        this.gameObject.AddComponent<MeshCollider>();

    }

    //Deletes the previous collider WITHOUT making a new one
    public void ColliderRemove()
    {
        Destroy(this.gameObject.GetComponent<MeshCollider>());
    }



}
