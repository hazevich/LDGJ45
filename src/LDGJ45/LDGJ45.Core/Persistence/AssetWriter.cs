using System;
using System.IO;

namespace LDGJ45.Core.Persistence
{
    public abstract class AssetWriter<T> : IAssetWriter
    {
        public Type AssetType { get; } = typeof(T);

        public void Write(string assetPath, object asset, ISerializer serializer)
        {
            var data = Serialize((T) asset, serializer);

            File.WriteAllBytes(assetPath, data);
        }

        protected abstract byte[] Serialize(T asset, ISerializer serializer);
    }
}