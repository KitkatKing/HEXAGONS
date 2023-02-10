using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeHexagon : MonoBehaviour
{

    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;

    public Material material;


    public List<Vector3> vertices = new List<Vector3>();

    public List<Vector2> uv = new List<Vector2>();

    public List<int> tris = new List<int>();
 

    public Vector3[] vert = Hexledata.vert;


    private void Start()
    {





        vertices.Add(Hexledata.vert[0]);
        vertices.Add(Hexledata.vert[1]);
        vertices.Add(Hexledata.vert[2]);
        vertices.Add(Hexledata.vert[3]);
        vertices.Add(Hexledata.vert[4]);
        vertices.Add(Hexledata.vert[5]);


        vertices.Add(Hexledata.vert[6]);
        vertices.Add(Hexledata.vert[7]);
        vertices.Add(Hexledata.vert[8]);
        vertices.Add(Hexledata.vert[9]);
        vertices.Add(Hexledata.vert[10]);
        vertices.Add(Hexledata.vert[11]);



        
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



        tris.Add(0);
        tris.Add(1);
        tris.Add(3);
        tris.Add(0);
        tris.Add(3);
        tris.Add(4);
        tris.Add(1);
        tris.Add(2);
        tris.Add(3);
        tris.Add(0);
        tris.Add(4);
        tris.Add(5);


        tris.Add(4);
        tris.Add(3);
        tris.Add(9);
        tris.Add(10);
        tris.Add(4);
        tris.Add(9);
        
        tris.Add(3);
        tris.Add(2);
        tris.Add(8);
        tris.Add(9);
        tris.Add(3);
        tris.Add(8);

        tris.Add(2);
        tris.Add(1);
        tris.Add(7);
        tris.Add(8);
        tris.Add(2);
        tris.Add(7);

        tris.Add(1);
        tris.Add(0);
        tris.Add(6);
        tris.Add(7);
        tris.Add(1);
        tris.Add(6);

        tris.Add(0);
        tris.Add(5);
        tris.Add(11);
        tris.Add(6);
        tris.Add(0);
        tris.Add(11);

        tris.Add(5);
        tris.Add(4);
        tris.Add(10);
        tris.Add(11);
        tris.Add(5);
        tris.Add(10);




        tris.Add(9);
        tris.Add(7);
        tris.Add(6);
        tris.Add(9);
        tris.Add(6);
        tris.Add(10);
        tris.Add(8);
        tris.Add(7);
        tris.Add(9);
        tris.Add(6);
        tris.Add(11);
        tris.Add(10);



        MakeMesh();
    }


    public void MakeMesh()
    {
        Mesh mesh = new Mesh();

        mesh.vertices = vertices.ToArray();

        mesh.uv = uv.ToArray();

        mesh.triangles = tris.ToArray();

        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;

        meshRenderer.material = material;


    }






}
