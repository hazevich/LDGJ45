using System;

namespace LDGJ45.Core.Persistence
{
    public class AssetWriterNotFoundException : Exception
    {
        public AssetWriterNotFoundException(Type assetType)
            : base($"Asset writer for {assetType} could not be found.")
        {
        }
    }
}