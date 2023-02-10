using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Vector2Int WORLD_SIZE = new Vector2Int(256, 24);

    public Chunk[,,] world;


    void Start()
    {
        world = new Chunk[WORLD_SIZE.x, WORLD_SIZE.x, WORLD_SIZE.y];


        Chunk chunky = new Chunk(Vector3.zero, gameObject.GetComponent<MeshFilter>());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
