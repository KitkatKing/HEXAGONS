using System;
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

    public override string ToString()
    {
        return String.Format("({0}, {1}, {2}, {3})", this.s, this.q, this.r, this.y);
    }

}
