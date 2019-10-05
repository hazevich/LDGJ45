using System;
using System.IO;

namespace LDGJ45.Core.Persistence
{
    public abstract class AssetReader<T> : IAssetReader
    {
        public Type AssetType { get; } = typeof(T);

        public object Load(AssetId assetId, string assetPath, IAssetsDatabase assetsDatabase, ISerializer serializer)
        {
            var fileContents = File.ReadAllBytes(assetPath);

            return CoreRead(assetId, fileContents, assetsDatabase, serializer);
        }

        protected abstract T CoreRead(
            AssetId assetId,
            byte[] content,
            IAssetsDatabase assetsDatabase,
            ISerializer serializer
        );
    }
}