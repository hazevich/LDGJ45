using System;

namespace LDGJ45.Persistence
{
    public interface IAssetReader
    {
        Type AssetType { get; }
        object Load(AssetId assetId, string assetPath, IAssetsDatabase assetsDatabase, ISerializer serializer);
    }
}