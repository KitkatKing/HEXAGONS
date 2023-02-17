using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
 
    private Vector3Int CHUNK_SIZE = new Vector3Int(32, 32, 32);

    public Vector3Int chunkPosition;
    private Vector3 globalPosition;

    public int[,,] chunkData;

    GameObject gameObject;

    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;

    public Material material;


    public List<Vector3> vertices = new List<Vector3>();

    public List<Vector2> uv = new List<Vector2>();

    public List<int> tris = new List<int>();

    public bool isStartCreateGen = false;

    public bool isVerticesGen = false;

    public bool isTrisGen = false;

    public bool isMeshGen = false;


    public Chunk(Vector3Int chunkPosition, MeshFilter mesh, Material material)
    {

        this.gameObject = new GameObject("Chunk" + chunkPosition, typeof(MeshFilter), typeof(MeshRenderer));

        this.chunkPosition = chunkPosition;

        this.globalPosition = new Vector3(chunkPosition.x * 32 * 0.75f, chunkPosition.y * 32 * 0.75f, chunkPosition.z * 32 * 0.88f);

        this.chunkData = new int[CHUNK_SIZE.x, CHUNK_SIZE.y, CHUNK_SIZE.z];
        createIntChunk();

        this.meshFilter = mesh;

        this.material = material;

    }


    public void createIntChunk()
    {
        for (int I = 0; I < CHUNK_SIZE.x; I++)
        {
            for (int J = 0; J < CHUNK_SIZE.y; J++)
            {
                for (int K = 0; K < CHUNK_SIZE.y; K++)
                {

                    this.chunkData[I, J, K] = 0;

                }
            }
        }
    }



    public void VoxelCreationCall()
    {
        if (chunkPosition.y < 3)
        {
            CreateAllHex();
        }

        //CreateOneHex(new Vector3Int(16,16,16));
    }

    public void VerticesGenerationCall()
    {
        generate_vertices();
    }

    public void UvTriCreationCall()
    {
        generate_uvs();
        generate_tris();
    }

    public void MeshColliderCall()
    {
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

                    this.chunkData[I, J, K] = 1;

                }
            }
        }
    }

    public void RemoveOneHex(Vector3Int pos)
    {

        this.chunkData[pos.x, pos.y, pos.z] = 0;

    }


    public void CreateOneHex(Vector3Int pos)
    {

        this.chunkData[pos.x, pos.y, pos.z] = 1;

    }


    // +============+
    // | GENERATE_* |
    // +============+

    private void add_vertex(Vector3 v) {
      vertices.Add(v); }


    private void generate_vertices() {
      Vector3 x_a = new Vector3(0.75f, 0.75f, 0.88f);

      for (int x = 0; x < CHUNK_SIZE.x; x++) {
        for (int y = 0; y < CHUNK_SIZE.y; y++) {
          for (int z = 0; z < CHUNK_SIZE.z; z++) {
            if (chunkData[x, y, z] != 0) {
              for (int i = 0; i < 12; i++) {
                add_vertex(Hexledata.vert[i] +
                           Vector3.Scale(x_a, new Vector3(x, y, (z + ((x % 2) * 0.5f)))) +
                           globalPosition);
    } } } } } }


    private void add_uv(Vector2 a) {
      uv.Add(a); }


    private void generate_uvs() {
      for (int x = 0; x < CHUNK_SIZE.x; x++) {
        for (int y = 0; y < CHUNK_SIZE.y; y++) {
          for (int z = 0; z < CHUNK_SIZE.z; z++) {
            if (chunkData[x, y, z] != 0) {
              for (int i = 0; i < 12; i++) {
                add_uv(Hexledata.uv[i]);
    } } } } } }


    private void generate_tris() {
      // each hexagon is aligned to 12 vertices, and will be
      // incremented each iteration by said amount to ensure
      // the correct vertices are used.
      int tc = 0;

      for (int x = 0; x < CHUNK_SIZE.x; x++) {
        for (int y = 0; y < CHUNK_SIZE.y; y++) {

          for (int z = 0; z < CHUNK_SIZE.z; z++) {
            if (chunkData[x, y, z] != 0) {
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

       // if ((y + 1 >= CHUNK_SIZE.y)) { return false; }

        // double bars are necessary to avoid an IndexOutOfBounds exception by checking the left side first
        if ((y + 1 >= CHUNK_SIZE.y) || (chunkData[x, y + 1, z] == 0)) {
        return true;
      } else {
        return false;
      }
    }
    private bool does_hex_y_minus_exist(int x, int y, int z) {

        if ((y <= 0)) { return false; }

        // further reduction of the previous because:
        // "if condition { return true; } else { return false; }" == "return condition"

        // note the double bars "||" are necessary because it evaluates the
        // right condition only if the left condition evaluates to false,
        // compared to single bar "|" which checks both.
        return ((y <= 0) ||
              (chunkData[x, y - 1, z] == 0));
    }

    private bool does_hex_z_plus_exist(int x, int y, int z) {

        if((z + 1 >= CHUNK_SIZE.z)) { return false; }

      return ((z + 1 >= CHUNK_SIZE.z) ||
              (chunkData[x, y, z + 1] == 0));
    }
    private bool does_hex_z_minus_exist(int x, int y, int z) {

           if((z <= 0)) { return false; }

      return ((z <= 0) ||
              (chunkData[x, y, z - 1] == 0));
    }

    private bool does_hex_x_plus_z_plus_exist(int x, int y, int z) {

        if ((x + 1 >= CHUNK_SIZE.x)) { return false; }

        return (((x + 1 >= CHUNK_SIZE.x) ||
              // this check is necessary because we add to the value
              // of z, depending on which x hexagon layer it is in,
              // in the generate_tris function.
               (z >= CHUNK_SIZE.z)) ||
              (chunkData[x + 1, y, z] == 0));
    }
    private bool does_hex_x_minus_z_plus_exist(int x, int y, int z) {

        if ((x <= 0) || (z >= CHUNK_SIZE.z)) { return false; }

        return (((x <= 0) ||
               (z >= CHUNK_SIZE.z)) ||
              (chunkData[x - 1, y, z] == 0));
    }
    private bool does_hex_x_plus_z_minus_exist(int x, int y, int z) {

        if ((x + 1 >= CHUNK_SIZE.x) || (z <= 0)) { return false; }

        return (((x + 1 >= CHUNK_SIZE.x) ||
               (z <= 0)) ||
              (chunkData[x + 1, y, z] == 0));
    }
    private bool does_hex_x_minus_z_minus_exist(int x, int y, int z) {

        if ((x <= 0) || (z < 0)) { return false; }

        return (((x <= 0) ||
               (z < 0)) ||
              (chunkData[x - 1, y, z] == 0));
    }

    // +=================+
    // | GENERATE_*_TRIS |
    // +=================+

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

        ColliderRemove();

        this.gameObject.AddComponent<MeshCollider>();

    }

    //Deletes the previous collider WITHOUT making a new one
    public void ColliderRemove()
    {
        Destroy(this.gameObject.GetComponent<MeshCollider>());
    }



}
