using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDGJ45.Persistence.Graphics
{
    public sealed class TextureReader : IAssetReader
    {
        private readonly GraphicsDevice _graphicsDevice;

        public TextureReader(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public Type AssetType { get; } = typeof(Texture2D);

        public object Load(AssetId assetId, string assetPath, IAssetsDatabase assetsDatabase, ISerializer serializer)
        {
            using var fs = File.OpenRead(assetPath);

            var texture = Texture2D.FromStream(_graphicsDevice, fs);
            texture.Name = assetId;

            var data = new Color[texture.Width * texture.Height];
            texture.GetData(data);
            for (var i = 0; i != data.Length; ++i)
                data[i] = Color.FromNonPremultiplied(data[i].ToVector4());

            texture.SetData(data);

            return texture;
        }
    }
}