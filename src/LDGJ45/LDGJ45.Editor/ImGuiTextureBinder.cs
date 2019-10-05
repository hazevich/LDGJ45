using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace LDGJ45.Editor
{
    public static class ImGuiTextureBinder
    {
        private static int _textureId;

        private static readonly Dictionary<IntPtr, Texture2D> LoadedTextures = new Dictionary<IntPtr, Texture2D>();

        public static IntPtr GetOrCreateBinding(Texture2D texture)
        {
            var existingBinding = LoadedTextures.SingleOrDefault(x => x.Value == texture);
            if (existingBinding.Equals(default(KeyValuePair<IntPtr, Texture2D>)))
            {
                var intPtr = new IntPtr(_textureId++);
                LoadedTextures.Add(intPtr, texture);
                return intPtr;
            }

            return existingBinding.Key;
        }

        public static bool ContainsKey(IntPtr texturePtr) => LoadedTextures.ContainsKey(texturePtr);
        public static Texture2D GetTexture(IntPtr texturePtr) => LoadedTextures[texturePtr];
    }
}