using System;
using System.Collections.Generic;
using System.Linq;
using LDGJ45.Graphics;
using Microsoft.Xna.Framework;

namespace LDGJ45.TileMaps.Rendering
{
    public sealed class RenderTileLayer : IDisposable
    {
        private readonly List<RenderTile> _renderTiles;

        private readonly TileConfiguration _tileConfiguration;
        private bool _isDisposed;

        public RenderTileLayer(TileLayer<Tile> tileLayer, TileConfiguration tileConfiguration)
        {
            TileLayer = tileLayer;
            _tileConfiguration = tileConfiguration;

            _renderTiles = new List<RenderTile>();

            Plot();
            TileLayer.TileAdded += OnTileAdded;
            TileLayer.TileRemoved += OnTileRemoved;
        }

        public TileLayer<Tile> TileLayer { get; }

        public IReadOnlyList<RenderTile> RenderTiles => _renderTiles;

        public void Dispose()
        {
            if (_isDisposed) return;

            TileLayer.TileAdded -= OnTileAdded;
            TileLayer.TileRemoved -= OnTileRemoved;
            _isDisposed = true;
        }

        private void Plot()
        {
            foreach (var tile in TileLayer)
                AddRenderTile(tile);
        }

        private void OnTileAdded(Tile tile)
        {
            AddRenderTile(tile);
        }

        private void OnTileRemoved(Tile tile)
        {
            _renderTiles.Remove(RenderTiles.Single(t => t.Tile.Row == tile.Row && t.Tile.Column == tile.Column));
        }

        private void AddRenderTile(Tile tile)
        {
            var vectorPosition = new Vector2(
                tile.Column * _tileConfiguration.TileWidth,
                tile.Row * _tileConfiguration.TileHeight
            );

            var sprite = new Sprite(new TextureRegion2D(tile.Texture, tile.Region));
            var renderTile = new RenderTile(tile, sprite, vectorPosition);
            _renderTiles.Add(renderTile);
        }
    }
}