using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexledata : MonoBehaviour
{

    public static readonly Vector3[] vert = new Vector3[12]
    {
        new Vector3(0.25f ,0.75f ,0.94f ), new Vector3(0.75f ,0.75f ,0.94f ), new Vector3(1 ,0.75f ,0.5f ),
        new Vector3(0.75f ,0.75f ,0.06f ), new Vector3(0.25f ,0.75f ,0.06f ), new Vector3(0 ,0.75f ,0.5f ),
        new Vector3(0.25f ,0 ,0.94f ), new Vector3(0.75f ,0 ,0.94f ), new Vector3(1 ,0 ,0.5f ),
        new Vector3(0.75f ,0 ,0.06f ), new Vector3(0.25f ,0 ,0.06f ), new Vector3(0 ,0 ,0.5f )
    };

    public static readonly Vector2[] uv = new Vector2[12]
    {
        new Vector2(0.125f, 0.5f), new Vector2(0.375f, 0.5f), new Vector2( 0.5f, 0.25f),
         new Vector2(0.375f , 0), new Vector2(0.125f , 0), new Vector2(0, 0.25f),

          new Vector2(0.625f, 1f), new Vector2(0.875f, 1f), new Vector2( 1f, 0.75f),
         new Vector2(0.875f , 0.5f), new Vector2(0.625f , 0.5f), new Vector2(0.5f, 0.75f)


    };


  





}
