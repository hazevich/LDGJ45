using LDGJ45.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace LDGJ45.Persistence.Graphics
{
    public sealed class SpriteReader : AssetReader<Sprite>
    {
        protected override Sprite CoreRead(
            AssetId assetId,
            byte[] content,
            IAssetsDatabase assetsDatabase,
            ISerializer serializer
        )
        {
            var spriteData = serializer.Deserialize<SpriteData>(content);
            var texture = assetsDatabase.Load<Texture2D>(spriteData.TextureName);
            var textureRegion = new TextureRegion2D(texture, spriteData.Region);

            return new Sprite(textureRegion);
        }
    }
}