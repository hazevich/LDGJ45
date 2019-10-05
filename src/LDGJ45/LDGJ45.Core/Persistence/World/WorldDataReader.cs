using LDGJ45.Core.World.Data;

namespace LDGJ45.Core.Persistence.World
{
    public sealed class WorldDataReader : AssetReader<WorldData>
    {
        protected override WorldData CoreRead(
            AssetId assetId,
            byte[] content,
            IAssetsDatabase assetsDatabase,
            ISerializer serializer
        ) =>
            serializer.Deserialize<WorldData>(content);
    }
}