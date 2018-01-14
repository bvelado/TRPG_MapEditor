using System;
using UnityEngine;

[Serializable]
public struct IntVector2 {
    public int x;
    public int y;

    public IntVector2(int x, int y){
        this.x = x;
        this.y = y;
    }

    #region Operator overloading

    public static IntVector2 operator + (IntVector2 a, IntVector2 b) {
        return new IntVector2(a.x + b.x, a.y + b.y);
    }

    public static IntVector2 operator - (IntVector2 a, IntVector2 b) {
        return new IntVector2(a.x - b.x, a.y - b.y);
    }

    public static bool operator ==(IntVector2 a, IntVector2 b){
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator !=(IntVector2 a, IntVector2 b){
        return !(a == b);
    }

    public override bool Equals(object obj){
        if(obj is IntVector2){
            IntVector2 a = (IntVector2)obj;
            return x == a.x && y == a.y;
        }
        return false;
    }
    public bool Equals(IntVector2 a){
        return x == a.x && y == a.y;
    }

    public override int GetHashCode(){
        return x ^ y;
    }

    #endregion

    #region Implicit cast

    public static implicit operator Vector2(IntVector2 a){
        return new Vector2(a.x, a.y);
    }

    #endregion

    public override string ToString ()
    {
        return string.Format ("({0},{1})", x, y);
    }
}