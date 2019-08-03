using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Static_Functions : MonoBehaviour
{
    //FUNCTIONS
    public static float smoothFunc0to1 (float t, bool clamp = true, float init = 0f /*initial value*/)
    {
        if (clamp)
        {
            t = Mathf.Clamp(t, 0f, 1f);
            init = Mathf.Clamp(init, 0f, 1f);
        }
        return ((Mathf.Sin(Mathf.PI * (t - 0.5f)) / 2f) + 0.5f) * (1 - init) + init;
    }
    public static float smoothFunc1to0(float t, bool clamp = true, float init = 1f /*final value*/)
    {
        if (clamp)
        {
            t = Mathf.Clamp(t, 0f, 1f);
            init = Mathf.Clamp(init, 0f, 1f);
        }
        return ((Mathf.Sin(Mathf.PI * (t + 0.5f)) / 2f) + 0.5f) * init;
    }
    public static float smoothStartFunc0to1(float t, bool clamp = true, float init = 0f /*final value*/)
    {
        if (clamp)
        {
            t = Mathf.Clamp(t, 0f, 1f);
            init = Mathf.Clamp(init, 0f, 1f);
        }
        return Mathf.Pow(t, 3f) * (1f - init) + init;
    }
    public static float smoothStartFunc1to0(float t, bool clamp = true, float init = 1f /*final value*/)
    {
        if (clamp)
        {
            t = Mathf.Clamp(t, 0f, 1f);
            init = Mathf.Clamp(init, 0f, 1f);
        }
        return (-Mathf.Pow(t, 3f) + 1f) * init;
    }

    //VOLUMES
    public static float getSphereVolume(float r, Vector3 scale)
    {
        return (4f * Mathf.PI * Mathf.Pow(r, 3f)) / 3f * scale.x * scale.y * scale.z;
    }
    public static float getCubeVolume(Vector3 size, Vector3 scale)
    {
        return size.x * size.y * size.z * scale.x * scale.y * scale.z;
    }
    public static float getCylinderVolume(float r, float h, Vector3 scale)
    {
        return Mathf.PI* Mathf.Pow(r, 2) * h * scale.x * scale.y * scale.z;
    }
    public static float getSignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float v321 = p3.x * p2.y * p1.z;
        float v231 = p2.x * p3.y * p1.z;
        float v312 = p3.x * p1.y * p2.z;
        float v132 = p1.x * p3.y * p2.z;
        float v213 = p2.x * p1.y * p3.z;
        float v123 = p1.x * p2.y * p3.z;
        return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
    }

    public static float getMeshVolume(Mesh mesh, Vector3 scale)
    {
        float volume = 0;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        for (int i = 0;i < mesh.triangles.Length; i += 3)
        {
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            volume += getSignedVolumeOfTriangle(p1, p2, p3);
        }
        return Mathf.Abs(volume) * scale.x * scale.y * scale.z;
    }

    //GEOMETRY
    public static Vector3 getMeshCenter(Mesh mesh, Vector3 scale)
    {
        Vector3 point = Vector3.zero;

        for(int i = 0; i < mesh.vertexCount; i++)
        {
            point += mesh.vertices[i];
        }
        point /= mesh.vertexCount;
        point = new Vector3(point.x * scale.x, point.y * scale.y, point.z * scale.z);

        return point;
    }

    public static Vector3 getCapsuleBottom(Transform transform, CapsuleCollider capCollider)
    {
        Vector3 point = (transform.localToWorldMatrix * capCollider.center);
        point = transform.position + new Vector3(point.x, point.y, point.z) - transform.up.normalized * capCollider.height/2f;
        return point;
    }

    public static Vector3 getCapsuleTop(Transform transform, CapsuleCollider capCollider)
    {
        Vector3 point = (transform.localToWorldMatrix * capCollider.center);
        point = transform.position + new Vector3(point.x, point.y, point.z) + transform.up.normalized * capCollider.height / 2f;
        return point;
    }


    //DRAW
    public static void drawPoint(Vector3 point, float radius, Color color)
    {
        Debug.DrawLine(point - Vector3.up * radius / 2f, point + Vector3.up * radius / 2f, color);
        Debug.DrawLine(point - Vector3.forward * radius / 2f, point + Vector3.forward * radius / 2f, color);
        Debug.DrawLine(point - Vector3.right * radius / 2f, point + Vector3.right * radius / 2f, color);
        //Debug.Log("drawing");
    }
}
