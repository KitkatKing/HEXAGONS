using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Scripting;

public class Vector4
{
    public int s;
    public int q;
    public int r;
    public int y;

    public Vector4(int s, int q, int r, int y)
    {
        this.s = s;
        this.q = q;
        this.r = r;
        this.y = y;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 operator +(Vector4 a, Vector4 b)
    {
        return new Vector4(a.s + b.s, a.q + b.q, a.r + b.r, a.y + b.y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 operator *(Vector4 a, int b)
    {
        return new Vector4(a.s * b, a.q * b, a.r * b, a.y * b);
    }

    public override string ToString()
    {
        return String.Format("({0}, {1}, {2}, {3})", this.s, this.q, this.r, this.y);
    }

    public class Vector4EqualityComparer : IEqualityComparer<Vector4>
    {
        public bool Equals(Vector4 x, Vector4 y)
        {
            return (x.s == y.s) && (x.q == y.q) && (x.r == y.r) && (x.y == y.y);
        }

        public int GetHashCode(Vector4 x)
        {
            return this.ToString().GetHashCode();
        }
    }

}
