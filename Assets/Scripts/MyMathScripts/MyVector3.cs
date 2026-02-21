using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyVector3
{
    public float x, y, z;
    
    public MyVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y; 
        this.z = z;
    }

    public static Vector3 zero => new Vector3(0.0f, 0.0f, 0.0f);


    public static Vector3 AddVector(Vector3 vect1, Vector3 vect2)
    {
        Vector3 rv = new Vector3(0, 0, 0);
       
        rv.x = vect1.x + vect2.x;
        rv.y = vect1.y + vect2.y;
        rv.z = vect1.z + vect2.z;

        return rv;        
    }

    public static Vector3 SubtractVector(Vector3 vect1, Vector3 vect2)
    {
        Vector3 rv = new Vector3(0, 0, 0);

        rv.x = vect1.x - vect2.x;
        rv.y = vect1.y - vect2.y;
        rv.z = vect1.z - vect2.z;

        return rv;
    }

    public static Vector3 InverseVector(Vector3 vect)
    {
        Vector3 rv = new Vector3(-vect.x, -vect.y, -vect.z);
        return rv;
    }

    public static float Magnitude(Vector3 v)
    {
        float magnitude = Mathf.Sqrt((v.x*v.x) + (v.y*v.y) + (v.z*v.z));
       
        return magnitude;
    }

    public static float VectorSqrd(Vector3 v)
    {
        float rv = (v.x * v.x) + (v.y * v.y) + (v.z * v.z);

        return rv;
    }

    public static Vector3 ScaleVector(Vector3 vector, float scalar)
    {
        Vector3 rv = new Vector3(0, 0, 0);

        rv.x = vector.x * scalar;
        rv.y = vector.y * scalar;
        rv.z = vector.z * scalar;

        return rv;
    }

    public static Vector3 DivideVector(Vector3 vector, float divisor)
    {
        Vector3 rv = new Vector3(0, 0, 0);

        rv.x = vector.x / divisor;
        rv.y = vector.y / divisor;
        rv.z = vector.z / divisor;

        return rv;
    }

    public static Vector3 NormalizeVector(Vector3 v)
    {
        Vector3 rv = new Vector3(0, 0, 0);
        
        rv = DivideVector(v, MyVector3.Magnitude(v));
        
        return rv;

    }

    public static float VectorDot(Vector3 v1, Vector3 v2, bool shouldNormalize = true)
    {
        float rv = 0.0f;

        if (shouldNormalize)
        {
            Vector3 normv1 = MyVector3.NormalizeVector(v1);
            Vector3 normv2 = MyVector3.NormalizeVector(v2);

            rv = normv1.x * normv2.x + normv1.y * normv2.y + normv1.z * normv2.z;
        }

        else
        {
            rv = v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }

        return rv;
    }

    public static Vector3 LERP(Vector3 a, Vector3 b, float t)
    {
        Vector3 rv = AddVector(a * (1 - t), b * t);

        return rv;
    }
}
