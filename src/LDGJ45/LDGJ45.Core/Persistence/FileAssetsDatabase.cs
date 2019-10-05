using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LDGJ45.Core.Persistence
{
    public sealed class FileAssetsDatabase : IAssetsDatabase
    {
        private readonly Dictionary<Type, IAssetReader> _assetReaders;
        private readonly Dictionary<AssetId, object> _cachedAssets = new Dictionary<AssetId, object>();
        private readonly ISerializer _serializer;

        private readonly Settings _settings;

        public FileAssetsDatabase(
            Settings settings,
            ISerializer serializer,
            IEnumerable<IAssetReader> assetReaders
        )
        {
            _settings = settings;
            _serializer = serializer;

            _assetReaders = assetReaders.ToDictionary(ar => ar.AssetType);
        }

        public T Load<T>(AssetId assetId, bool cached = true)
        {
            var assetType = typeof(T);
            var assetPath = ConstructPath(assetId);

            if (cached && _cachedAssets.TryGetValue(assetId, out var assetObj))
                return (T) assetObj;

            if (!_assetReaders.TryGetValue(assetType, out var assetReader))
                throw new AssetReaderNotFoundException(assetType);

            var asset = (T) assetReader.Load(assetId, assetPath, this, _serializer);
            _cachedAssets[assetId] = asset;

            return asset;
        }

        private string ConstructPath(AssetId assetId)
        {
            return Path.Combine(_settings.AssetsBaseFolder, assetId);
        }
    }
}