using System;

namespace LDGJ45.Core.Utils
{
    public static class FloatExtensions
    {
        public static bool LessOrEqualTo(this float x, float y) => Math.Abs(x - y) < 0.0001f || x < y;
        public static bool GreaterOrEqualTo(this float x, float y) => Math.Abs(x - y) < 0.0001f || x > y;
    }
}