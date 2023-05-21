using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkByteDictionary
{
    private Dictionary<Vector3Int, byte> byteDict;
    private int CHUNK_SIZE = 32;

    public ChunkByteDictionary()
    {
        byteDict = new Dictionary<Vector3Int, byte>();
        setBoarderBytes();
    }


    public void setByte(Vector3Int position)
    {

        this.byteDict.Add(new Vector3Int(position.x, position.y, position.z), new byte());

    }

    public void setBoarderBytes()
    {

        for (int X = 0; X < CHUNK_SIZE; X++)
        {
            for (int Z = 0; Z < CHUNK_SIZE; Z++)
            {

                this.byteDict.Add(new Vector3Int(X, -1, Z), new byte()); 
                this.byteDict.Add(new Vector3Int(X, CHUNK_SIZE + 1, Z), new byte()); 
                

            }
        }
        
        for (int Y = 0; Y < CHUNK_SIZE; Y++)
        {
            for (int Z = 0; Z < CHUNK_SIZE; Z++)
            {
                this.byteDict.Add(new Vector3Int(-1, Y, Z), new byte()); 
                this.byteDict.Add(new Vector3Int(CHUNK_SIZE + 1, Y, Z), new byte()); 
                
            }
        }

        for (int Y = 0; Y < CHUNK_SIZE; Y++)
        {
            for (int X = 0; X < CHUNK_SIZE; X++)
            {
                this.byteDict.Add(new Vector3Int(X, Y, -1), new byte()); 
                this.byteDict.Add(new Vector3Int(X, Y, CHUNK_SIZE + 1), new byte()); 
                
            }
        }
        
    }


    public void makeList()
    {
        for (int X = 0; X < CHUNK_SIZE; X++)
        {
            for (int Z = 0; Z < CHUNK_SIZE; Z++)
            {
                for (int Y = 0; Y < CHUNK_SIZE; Y++)
                {
                    if (this.byteDict.ContainsKey(new Vector3Int(X, Y, Z)) == true)
                    {
                        activateTheJank(new Vector3Int(X, Y, Z));
                    }
                }
            }
        }

    }

    public Dictionary<Vector3Int, byte> listReference()
    {
        return byteDict;
    }

    ///////////////////////////////////////////////////////////////////////////////////////
    //                          YOU KNOW WHAT FUCKING TIME IT IS                         //
    ///////////////////////////////////////////////////////////////////////////////////////


    public void activateTheJank(Vector3Int position)
    {
        // to set a byte:    Byte |= (bit to put in) << (position in byte)
        this.byteDict[position] |= 1 << 1;









    }








}
