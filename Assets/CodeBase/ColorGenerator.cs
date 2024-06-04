using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace CodeBase
{
    public static class ColorGenerator
    {
        private static Dictionary<int, Color> colorCache = new Dictionary<int, Color>();

        public static Color GetColorForId(int id)
        {
            if (colorCache.ContainsKey(id))
            {
                return colorCache[id];
            }
            else
            {
                Color newColor = GenerateUniqueColor(id);
                colorCache[id] = newColor;
                return newColor;
            }
        }

        private static Color GenerateUniqueColor(int id)
        {
            Random random = new Random(id.GetHashCode());
            float r = (float)random.NextDouble();
            float g = (float)random.NextDouble();
            float b = (float)random.NextDouble();
            return new Color(r, g, b);
        }
    }
}