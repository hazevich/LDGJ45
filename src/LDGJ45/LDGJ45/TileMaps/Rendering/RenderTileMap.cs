using System;
using System.Collections.Generic;
using System.Linq;

namespace LDGJ45.TileMaps.Rendering
{
    public sealed class RenderTileMap : IDisposable
    {
        private bool _isDisposed;
        private List<RenderTileLayer> _renderTileLayers = new List<RenderTileLayer>();

        public RenderTileMap(TileMap tileMap)
        {
            Map = tileMap;
            _renderTileLayers.AddRange(PlotMap(tileMap));
            Map.TileLayerAdded += OnTileLayerAdded;
            Map.TileLayerRemoved += OnTileLayerRemoved;
            Map.Resized += OnTileMapResized;
        }

        public TileMap Map { get; }
        
        public IReadOnlyList<RenderTileLayer> RenderTileLayers => _renderTileLayers;

        public void Dispose()
        {
            if (_isDisposed) return;

            Map.TileLayerAdded -= OnTileLayerAdded;
            Map.TileLayerRemoved -= OnTileLayerRemoved;
            _isDisposed = true;
        }


        private static IEnumerable<RenderTileLayer> PlotMap(TileMap tileMap)
        {
            return tileMap.TileLayers.Select(tileLayer => new RenderTileLayer(tileLayer, tileMap.TileConfiguration));
        }

        private void OnTileLayerAdded(TileLayer<Tile> tileLayer)
        {
            _renderTileLayers.Add(new RenderTileLayer(tileLayer, Map.TileConfiguration));
        }

        private void OnTileLayerRemoved(int index)
        {
            var tileLayer = _renderTileLayers.Single(x => x.TileLayer.Index == index);
            _renderTileLayers.Remove(tileLayer);
        }

        private void OnTileMapResized()
        {
            _renderTileLayers = PlotMap(Map).ToList();
        }
    }
}