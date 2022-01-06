using System;
using UnityEngine;

public static class Utils
{
    // Returns int between 0 and 255
    public static int HexToDec(string hex)
    {
        return Convert.ToInt32(hex, 16);
    }

    // Returns a float between 0 and1
    public static float HexToDec0To1(string hex)
    {
        return HexToDec(hex) / 255f;
    }

    public static Color GetColorFromString(string colour)
    {
        float red = HexToDec0To1(colour.Substring(0, 2));
        float green = HexToDec0To1(colour.Substring(2, 2));
        float blue = HexToDec0To1(colour.Substring(4, 2));
        float alpha = 1f;

        if (colour.Length >= 8)
        {
            // Color string contains alpha
            alpha = HexToDec0To1(colour.Substring(6, 2));
        }

        return new Color(red, green, blue, alpha);
    }
}
