using System;

namespace LDGJ45.Core.Persistence
{
    public interface IAssetWriter
    {
        Type AssetType { get; }
        void Write(string assetPath, object asset, ISerializer serializer);
    }
}