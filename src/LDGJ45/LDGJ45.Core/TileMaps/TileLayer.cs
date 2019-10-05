using System;
using System.Collections;
using System.Collections.Generic;

namespace LDGJ45.Core.TileMaps
{
    public sealed class TileLayer<T> : IEnumerable<T>
        where T : ITile
    {
        private readonly Grid<T> _tiles;

        public TileLayer(int index, TileConfiguration tileConfiguration)
        {
            Index = index;

            _tiles = new Grid<T>(
                tileConfiguration.MapWidth,
                tileConfiguration.MapHeight,
                tileConfiguration.TileWidth,
                tileConfiguration.TileHeight
            );
        }

        public int Index { get; }

        public bool IsVisible { get; set; } = true;

        public IEnumerator<T> GetEnumerator()
        {
            return _tiles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event Action<T> TileAdded;
        public event Action<T> TileRemoved;

        public bool Add(T tile)
        {
            if (!_tiles.IsInRange(tile.Row, tile.Column) || _tiles[tile.Row, tile.Column] != null) return false;

            _tiles[tile.Row, tile.Column] = tile;

            OnTileAdded(tile);

            return true;
        }

        public bool Upsert(T tile)
        {
            return Remove(tile.Row, tile.Column) && Add(tile);
        }

        public bool Remove(int row, int column)
        {
            if (!_tiles.IsInRange(row, column)) return false;

            if (_tiles[row, column] != null)
            {
                var tile = _tiles[row, column];
                _tiles[tile.Row, tile.Column] = default;
                OnTileRemoved(tile);
            }


            return true;
        }

        public (bool tileExists, T tile) Get(int row, int column)
        {
            if (!_tiles.IsInRange(row, column)) return (false, default);

            var tile = _tiles[row, column];

            return (tile != null, tile);
        }

        public bool Exists(int row, int column)
        {
            if (!_tiles.IsInRange(row, column)) return false;

            var tile = _tiles[row, column];

            return tile != null;
        }

        public void Resize(int rows, int cols)
        {
            _tiles.Resize(rows, cols);
        }

        private void OnTileAdded(T tile)
        {
            TileAdded?.Invoke(tile);
        }

        private void OnTileRemoved(T tile)
        {
            TileRemoved?.Invoke(tile);
        }
    }
}