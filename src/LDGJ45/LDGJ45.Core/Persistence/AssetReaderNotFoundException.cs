using System;

namespace LDGJ45.Core.Persistence
{
    public class AssetReaderNotFoundException : Exception
    {
        public AssetReaderNotFoundException(Type assetType)
            : base($"Asset reader for {assetType} could not be found.")
        {
        }
    }
}