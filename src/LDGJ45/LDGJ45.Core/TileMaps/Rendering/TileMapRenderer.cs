﻿using LDGJ45.Core.Graphics;
using LDGJ45.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDGJ45.Core.TileMaps.Rendering
{
    public sealed class TileMapRenderer : RenderableComponent
    {
        private readonly RenderTileMap _renderTileMap;

        public TileMapRenderer(RenderTileMap renderTileMap)
        {
            _renderTileMap = renderTileMap;
        }
        public override void Render(SpriteBatch spriteBatch) => Render(spriteBatch, _renderTileMap);

        private static void Render(SpriteBatch spriteBatch, RenderTileMap renderTileMap)
        {
            for (var index = 0; index < renderTileMap.RenderTileLayers.Count; index++)
            {
                var renderTileLayer = renderTileMap.RenderTileLayers[index];

                if (renderTileLayer.TileLayer.IsVisible)
                    for (var i = 0; i < renderTileLayer.RenderTiles.Count; i++)
                    {
                        var tile = renderTileLayer.RenderTiles[i];
                        SpriteBatchExtensions.Render(spriteBatch, (Sprite) tile.Sprite, (Vector2) tile.Position);
                    }
            }

            if (renderTileMap.Map.CollisionLayer.IsVisible)
                foreach (var collisionTile in renderTileMap.Map.CollisionLayer)
                    spriteBatch.RenderFilledRectangle(collisionTile.Bounds, Color.Red * 0.3f);
        }
    }
}