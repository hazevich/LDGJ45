using LDGJ45.Core.World.Data;

namespace LDGJ45.Core.Persistence.World
{
    public sealed class WorldDataWriter : AssetWriter<WorldData>
    {
        protected override byte[] Serialize(WorldData asset, ISerializer serializer) => serializer.Serialize(asset);
    }
}