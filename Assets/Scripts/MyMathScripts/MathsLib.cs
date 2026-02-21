using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathsLib
{
    public static Vector3 CrossProduct(Vector3 v1, Vector3 v2)
    {
        Vector3 rv = new Vector3(0, 0, 0);

        rv.x = v1.y * v2.z - v1.z * v2.y;
        rv.y = v1.z * v2.x - v1.x * v2.z;
        rv.z = v1.x * v2.y - v1.y * v2.x;

        return rv;
    }

    public static Vector3 RotateVertex(float angle, Vector3 axis, Vector3 vertex)
    {
        Vector3 rv = vertex * Mathf.Cos(angle) +
            axis * MyVector3.VectorDot(axis, vertex) * (1.0f - Mathf.Cos(angle));

        return rv;
    }


    public static Vector3 EulerAnglesToDirection(Vector3 v)
    {
        Vector3 rv = new Vector3();
        
        rv.x = Mathf.Cos(v.x) * Mathf.Sin(v.y);
        rv.y = -Mathf.Sin(v.x);
        rv.z = Mathf.Cos(v.y) * Mathf.Cos(v.x);

        return rv;
    }

}

[System.Serializable]
public class Matrix4by4
{
    public float[,] values;

    public Matrix4by4(Vector4 column1, Vector4 column2, Vector4 column3, Vector4 column4)
    {
        values = new float[4,4];

        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = column1.w;

        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = column2.w;

        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = column3.w;

        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = column4.w;
    }

    public Matrix4by4(Vector3 column1, Vector3 column2, Vector3 column3, Vector3 column4)
    {
        values = new float[4, 4];

        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = 0;

        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = 0;

        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = 0;

        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = 1;
    }

    public static Matrix4by4 Identity
    {
        get
        {
            return new Matrix4by4
                (new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(0, 0, 0, 1));
        }
    }

    public static Vector4 operator *(Matrix4by4 lhs, MyVector4 rhs)
    {
        Vector4 rv = new Vector4(0, 0, 0, 0);

        rv.x = lhs.values[0, 0] * rhs.x + lhs.values[0, 1] * rhs.x + lhs.values[0, 2] * rhs.x + lhs.values[0, 3] * rhs.x;
        rv.y = lhs.values[1, 0] * rhs.y + lhs.values[1, 1] * rhs.y + lhs.values[1, 2] * rhs.y + lhs.values[1, 3] * rhs.y;
        rv.z = lhs.values[2, 0] * rhs.z + lhs.values[2, 1] * rhs.z + lhs.values[2, 2] * rhs.z + lhs.values[2, 3] * rhs.z;
        rv.w = lhs.values[3, 0] * rhs.w + lhs.values[3, 1] * rhs.w + lhs.values[3, 2] * rhs.w + lhs.values[3, 3] * rhs.w;

        return rv;
    }

    public static Matrix4by4 operator *(Matrix4by4 lhs, Matrix4by4 rhs)
    {
        Matrix4by4 rv = new Matrix4by4(MyVector4.zero, MyVector4.zero, MyVector4.zero, MyVector4.zero);

        for (int i = 0; i < 4; i++)
        {
            rv.values[i, 0] = lhs.values[0, 0] * rhs.values[i, 0] + lhs.values[1, 0] * rhs.values[i, 1] + lhs.values[2, 0] * rhs.values[i, 2] + lhs.values[3, 0] * rhs.values[i, 3];
            rv.values[i, 1] = lhs.values[0, 1] * rhs.values[i, 0] + lhs.values[1, 1] * rhs.values[i, 1] + lhs.values[2, 1] * rhs.values[i, 2] + lhs.values[3, 1] * rhs.values[i, 3];
            rv.values[i, 2] = lhs.values[0, 2] * rhs.values[i, 0] + lhs.values[1, 2] * rhs.values[i, 1] + lhs.values[2, 2] * rhs.values[i, 2] + lhs.values[3, 2] * rhs.values[i, 3];
            rv.values[i, 3] = lhs.values[0, 3] * rhs.values[i, 0] + lhs.values[1, 3] * rhs.values[i, 1] + lhs.values[2, 3] * rhs.values[i, 2] + lhs.values[3, 3] * rhs.values[i, 3];
        }

        return rv;
    }

    public Matrix4by4 TranslationInverse()
    {
        Matrix4by4 rv = Identity;
        rv.values[0, 3] = -values[0, 3];
        rv.values[1, 3] = -values[1, 3];
        rv.values[2, 3] = -values[2, 3];

        return rv;
    }

    public Matrix4by4 ScaleInverse()
    {
        Matrix4by4 rv = Identity;



        return rv;
    }
}


public class MyQuat
{

    public static Quaternion MultiplyQuats(Quaternion q1, Quaternion q2)
    {
        Vector3 v1 = new Vector3(q1.x, q1.y, q1.z);
        Vector3 v2 = new Vector3(q2.x, q2.y, q2.z);

        Vector3 cross = MathsLib.CrossProduct(v1, v2);

        Quaternion rv = new Quaternion(q2.w * q1.x + q1.w * q2.x + cross.x,
            q2.w * q1.y + q1.w * q2.y + cross.y,
            q2.w * q1.z + q1.w * q2.z + cross.z,
            q2.w * q1.w - MyVector3.VectorDot(v1, v2, false));

        return rv;
    }

    public static float QuatMagnitude(Quaternion q)
    {
        float mag = Mathf.Sqrt((q.w * q.w) + (q.x * q.x) + (q.y * q.y) + (q.z * q.z));
        return mag;
    }

    public static Quaternion Inverse(Quaternion q)
    {
        Quaternion rv = new Quaternion(0, 0, 0, 0);
        rv.w = q.w;
        rv.x = -q.x;
        rv.y = -q.y;
        rv.z = -q.z;

        return rv;
    }

    public static Quaternion Normalize(Quaternion quat)
    {
        Quaternion rv = new Quaternion(0, 0, 0, 0);

        float mag = Mathf.Sqrt(quat.w * quat.w + quat.x * quat.x + quat.y * quat.y + quat.z * quat.z);
        rv.w /= mag;
        rv.x = quat.x * (1.0f / mag);
        rv.y = quat.y * (1.0f / mag);
        rv.z = quat.z * (1.0f / mag);

        return rv;
    }

    public static Quaternion ScaleQuat(Quaternion q,  float scale)
    {
        Quaternion rv = new Quaternion();
        rv.w = q.w * scale;
        rv.x = q.x * scale;
        rv.y = q.y * scale;
        rv.z = q.z * scale;
        return rv;
    }

    public static Quaternion QuatToPow(Quaternion q, float p)
    {
        Quaternion rv = new Quaternion(0, 0, 0, 1);

        Vector3 v = new Vector3(q.x, q.y, q.z);

        Vector3 vNorm = MyVector3.NormalizeVector(v);

        float qMag = QuatMagnitude(q);
        float theta = Mathf.Acos(q.w / qMag) * p;
        float norm = Mathf.Pow(qMag, p);

        float w = Mathf.Cos(theta);
        v = MyVector3.ScaleVector(vNorm, Mathf.Sin(theta));

        rv = ScaleQuat(new Quaternion(v.x, v.y, v.z, w), norm);
        return rv;
    }

    public static Quaternion SLERP(Quaternion q, Quaternion r, float t)
    {

        Quaternion rv = MultiplyQuats(q, QuatToPow(MultiplyQuats(Inverse(q), r), t));

        return rv;
    }
}