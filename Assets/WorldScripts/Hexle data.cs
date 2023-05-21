using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexledata
{

    public static readonly Vector3[] vert = new Vector3[12]
    {
        new Vector3(0.5f ,0.75f ,0 ),new Vector3(0.06f ,0.75f ,0.25f ),new Vector3(0.06f ,0.75f ,0.75f ),new Vector3(0.5f ,0.75f ,1 ),new Vector3(0.94f ,0.75f ,0.75f ),new Vector3(0.94f ,0.75f ,0.25f ),

        new Vector3(0.5f ,0f ,0 ),new Vector3(0.06f ,0f ,0.25f ),new Vector3(0.06f ,0f ,0.75f ), new Vector3(0.5f ,0f ,1 ),new Vector3(0.94f ,0f ,0.75f ),new Vector3(0.94f ,0f ,0.25f )



        //0 1 2 3  4  5
        //6 7 8 9 10 11

    };

    public static readonly Vector2[] uv = new Vector2[12]
    {
        new Vector2(0.125f, 0.5f), new Vector2(0.375f, 0.5f), new Vector2( 0.5f, 0.25f),
         new Vector2(0.375f , 0), new Vector2(0.125f , 0), new Vector2(0, 0.25f),

          new Vector2(0.625f, 1f), new Vector2(0.875f, 1f), new Vector2( 1f, 0.75f),
         new Vector2(0.875f , 0.5f), new Vector2(0.625f , 0.5f), new Vector2(0.5f, 0.75f)


    };


    public static readonly Vector4[] AxialDirections = new Vector4[3]
    {

        //              S   (   )                   Q   (   )           R (    )
        new Vector4(0, -1, 1, 0), new Vector4(1, 0, -1, 0), new Vector4(-1, 1, 0, 0)
    };
  





}
