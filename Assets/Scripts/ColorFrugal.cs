using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFrugal :IComparable {

    //
    // Summary:
    //     Red component of the color.
    public byte r;
    //
    // Summary:
    //     Green component of the color.
    public byte g;
    //
    // Summary:
    //     Blue component of the color.
    public byte b;
    //
    // Summary:
    //     Alpha component of the color.
    public byte a;

    //
    // Summary:
    //     Constructs a new Color32 with given r, g, b, a components.
    //
    // Parameters:
    //   r:
    //
    //   g:
    //
    //   b:
    //
    //   a:
    public ColorFrugal()
    {
        this.r = 255;
        this.g = 255;
        this.b = 255;
        this.a = 255;
    }

    public ColorFrugal(byte r, byte g, byte b, byte a)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }

    public bool Equals(ColorFrugal c)
    {
        return c.r.Equals(r) && c.g.Equals(g) && c.b.Equals(b) && c.a.Equals(a);
    }

    public override int GetHashCode()
    {
        return r.GetHashCode() ^ g.GetHashCode() ^ b.GetHashCode() ^ a.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return this.Equals(obj as ColorFrugal);
    }

    public int CompareTo(ColorFrugal other)
    {
        if (other == null)
        {
            return 1;
        }

        //Return the difference in power.
        return this.a - other.a;
    }

    public int CompareTo(object obj)
    {
        ColorFrugal other = (ColorFrugal)obj;
        if (other == null)
        {
            return 1;
        }

        //Return the difference in power.
        return this.a - other.a;
    }

    public Color32 ToColor32()
    {
        Color32 color = new Color32();
        color.r = this.r;color.g = this.g;color.b = this.b;color.a = this.a;
        return color;
    }
}
