using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LDGJ45.Core.Persistence
{
    public sealed class FileAssetsDatabase : IAssetsDatabase
    {
        private readonly Dictionary<Type, IAssetReader> _assetReaders;
        private readonly Dictionary<Type, IAssetWriter> _assetWriters;
        
        private readonly Dictionary<AssetId, object> _cachedAssets = new Dictionary<AssetId, object>();
        
        private readonly ISerializer _serializer;

        private readonly Settings _settings;

        public FileAssetsDatabase(
            Settings settings,
            ISerializer serializer,
            IEnumerable<IAssetReader> assetReaders,
            IEnumerable<IAssetWriter> assetWriters
        )
        {
            _settings = settings;
            _serializer = serializer;

            _assetReaders = assetReaders.ToDictionary(ar => ar.AssetType);
            _assetWriters = assetWriters.ToDictionary(ar => ar.AssetType);
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

        public void Store<T>(T asset, AssetId assetId)
        {
            var assetType = asset.GetType();

            if (!_assetWriters.TryGetValue(assetType, out var assetWriter))
            {
                throw new AssetWriterNotFoundException(assetType);
            }

            var assetPath = ConstructPath(assetId);

            assetWriter.Write(assetPath, asset, _serializer);

            _cachedAssets[assetId] = asset;
        }

        private string ConstructPath(AssetId assetId) => Path.Combine(_settings.AssetsBaseFolder, assetId);
    }
}