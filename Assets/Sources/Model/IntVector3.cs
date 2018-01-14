using System;
using UnityEngine;

[Serializable]
public struct IntVector3 {
    public int x;
    public int y;
    public int z;

    public IntVector3(int x, int y, int z){
        this.x = x;
        this.y = y;
        this.z = z;
    }

    #region Operator overloading

    public static IntVector3 operator + (IntVector3 a, IntVector3 b) {
        return new IntVector3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static IntVector3 operator - (IntVector3 a, IntVector3 b) {
        return new IntVector3(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public override bool Equals(object obj){
        if(obj is IntVector3){
            IntVector3 a = (IntVector3)obj;
            return x == a.x && y == a.y && z == a.z;
        }
        return false;
    }
    public bool Equals(IntVector3 a){
        return x == a.x && y == a.y && z == a.z;
    }

    public override int GetHashCode(){
        return x ^ y ^ z;
    }

    #endregion

    #region Implicit cast

    public static implicit operator Vector3(IntVector3 a){
        return new Vector3(a.x, a.y, a.z);
    }

    #endregion

    public override string ToString ()
    {
        return string.Format ("({0},{1},{2})", x, y, z);
    }
}