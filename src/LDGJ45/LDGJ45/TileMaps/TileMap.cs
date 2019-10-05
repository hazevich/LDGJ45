using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;

namespace LDGJ45.TileMaps
{
    public sealed class TileMap
    {
        private readonly List<TileLayer<Tile>> _tileLayers = new List<TileLayer<Tile>>();

        public TileMap(TileConfiguration tileConfiguration)
        {
            TileConfiguration = tileConfiguration;
            CollisionLayer = new TileLayer<CollisionTile>(0, TileConfiguration)
            {
                IsVisible = false
            };
        }

        public TileConfiguration TileConfiguration { get; private set; }
        public IReadOnlyList<TileLayer<Tile>> TileLayers => _tileLayers;

        public TileLayer<CollisionTile> CollisionLayer { get; private set; }

        public event Action<TileLayer<Tile>> TileLayerAdded;
        public event Action<int> TileLayerRemoved;
        public event Action Resized;

        public TileLayer<Tile> CreateNewTileLayer()
        {
            var tileLayer = new TileLayer<Tile>(GetNextTileLayerIndex(), TileConfiguration);
            _tileLayers.Add(tileLayer);
            OnTileLayerAdded(tileLayer);
            return tileLayer;
        }

        public TileLayer<Tile> GetTileLayer(int index)
        {
            return _tileLayers.SingleOrDefault(x => x.Index == index);
        }

        public bool RemoveTileLayer(int index)
        {
            var tileLayer = GetTileLayer(index);
            if (tileLayer != null)
            {
                _tileLayers.Remove(tileLayer);
                OnTileLayerRemoved(index);
                return true;
            }

            return false;
        }

        public void Resize(int rows, int cols)
        {
            if (cols < 0 || rows < 0) return;

            var tileWidth = TileConfiguration.TileWidth;
            var tileHeight = TileConfiguration.TileHeight;
            TileConfiguration = new TileConfiguration(tileWidth * cols, rows * tileHeight, tileWidth, tileHeight);

            for (var i = 0; i < _tileLayers.Count; i++) _tileLayers[i].Resize(rows, cols);

            CollisionLayer.Resize(rows, cols);

            Resized?.Invoke();
        }

        public void ResizeTiles(int tileWidth, int tileHeight)
        {
            var cols = TileConfiguration.MapWidth / TileConfiguration.TileWidth;
            var rows = TileConfiguration.MapHeight / TileConfiguration.TileHeight;

            TileConfiguration = new TileConfiguration(cols * tileWidth, rows * tileHeight, tileWidth, tileHeight);


            var newCollisionLayer = new TileLayer<CollisionTile>(0, TileConfiguration);

            foreach (var collisionTile in CollisionLayer)
            {
                var bounds = new RectangleF(
                    collisionTile.Column * TileConfiguration.TileWidth,
                    collisionTile.Row * TileConfiguration.TileHeight,
                    TileConfiguration.TileWidth,
                    TileConfiguration.TileHeight
                );

                newCollisionLayer.Add(new CollisionTile(collisionTile.Row, collisionTile.Column, bounds));
            }

            CollisionLayer = newCollisionLayer;

            Resized?.Invoke();
        }

        public void AddCollision(int row, int column)
        {
            var bounds = new RectangleF(
                column * TileConfiguration.TileWidth,
                row * TileConfiguration.TileHeight,
                TileConfiguration.TileWidth,
                TileConfiguration.TileHeight
            );

            var collisionTile = new CollisionTile(row, column, bounds);
            CollisionLayer.Add(collisionTile);
        }

        public void RemoveCollision(int row, int column)
        {
            CollisionLayer.Remove(row, column);
        }

        private void OnTileLayerAdded(TileLayer<Tile> tileLayer)
        {
            TileLayerAdded?.Invoke(tileLayer);
        }

        private void OnTileLayerRemoved(int index)
        {
            TileLayerRemoved?.Invoke(index);
        }

        private int GetNextTileLayerIndex()
        {
            return _tileLayers.LastOrDefault()?.Index + 1 ?? 0;
        }
    }
}