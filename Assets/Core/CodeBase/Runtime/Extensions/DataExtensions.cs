using UnityEngine;
using WC.Runtime.Infrastructure.Data;

namespace WC.Runtime.Extensions
{
  public static class DataExtensions
  {
    public static Vector3Data ToVector3Data(this Vector3 vector3) => 
      new(vector3.x, vector3.y, vector3.z);
    
    public static Vector3 ToVector3(this Vector3Data vector3Data) => 
      new(vector3Data.X, vector3Data.Y, vector3Data.Z);

    public static Vector3 SetX(this Vector3 vector3, float x) => new(x, vector3.y, vector3.z);
    public static Vector3 SetY(this Vector3 vector3, float y) => new(vector3.x, y, vector3.z);
    public static Vector3 SetZ(this Vector3 vector3, float z) => new(vector3.x, vector3.y, z);

    public static Vector3 AddX(this Vector3 vector3, float x)
    {
      vector3.x += x;
      return vector3;
    }

    public static Vector3 AddY(this Vector3 vector3, float y)
    {
      vector3.y += y;
      return vector3;
    }

    public static Vector3 AddZ(this Vector3 vector3, float z)
    {
      vector3.z += z;
      return vector3;
    }

    public static string ToJson(this object obj) =>
      JsonUtility.ToJson(obj);

    public static T ToDeserialized<T>(this string json) => 
      JsonUtility.FromJson<T>(json);
  }
}