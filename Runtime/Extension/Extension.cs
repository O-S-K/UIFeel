using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension  
{
    public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        return component != null ? component : gameObject.AddComponent<T>();
    }

    public static Vector3 RandomVector3(Vector3 min, Vector3 max)
    {
        return new Vector3(Random.Range(min.x, max.x + .0001f), Random.Range(min.y, max.y + .0001f), Random.Range(min.z, max.z + .0001f));
    }

    public static Vector3 WithY(this Vector3 vector3, float y)
    {
        return new Vector3(vector3.x, y, vector3.z);
    }
}
