using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyVector4
{
    public float x, y, z, w; 
    
    public MyVector4(float x , float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public MyVector4()
    {
        this.x = 0.0f;
        this.y = 0.0f;
        this.z = 0.0f;
        this.w = 0.0f;
    }

    public static Vector4 zero => new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
}
