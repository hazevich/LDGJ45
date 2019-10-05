using System.Numerics;
using ImGuiNET;
using LDGJ45.Core.Messaging;
using LDGJ45.Core.TileMaps;
using LDGJ45.Core.World.Messages;

namespace LDGJ45.Editor.UI
{
    public sealed class TilePaletteWindow : IWindow
    {
        private TileMap _tileMap;

        public TilePaletteWindow(ISubscriber subscriber)
        {
            subscriber.Subscribe<ComponentAddedMessage>(Handle);
            subscriber.Subscribe<ComponentRemovedMessage>(Handle);
        }

        public TileLayer<Tile> SelectedTileLayer { get; private set; }
        
        public void Render()
        {
            if (!ImGui.Begin("Tile palette"))
                return;

            if (_tileMap == null)
            {
                ImGui.Text("Please add tilemap");
                return;
            }

            if (ImGui.CollapsingHeader("Tile layers"))
            {
                ImGui.PushID(-1);
                var collisionLayer = _tileMap.CollisionLayer;
                var collisionLayerVisible = collisionLayer.IsVisible;

                if (ImGui.Checkbox("", ref collisionLayerVisible))
                {
                    collisionLayer.IsVisible = collisionLayerVisible;
                }

                ImGui.PopID();
                ImGui.SameLine();
                ImGui.Text("Collision layer");


                for (var i = 0; i < _tileMap.TileLayers.Count; i++)
                {
                    uint color;
                    if (SelectedTileLayer == _tileMap.TileLayers[i])
                    {
                        color = ImGui.GetColorU32(new Vector4(0, 255, 255, 1));
                    }
                    else
                    {
                        color = ImGui.GetColorU32(new Vector4(192, 192, 192, 1));
                    }


                    var tileLayer = _tileMap.TileLayers[i];
                    var visible = tileLayer.IsVisible;
                    ImGui.PushID(i);
                    if (ImGui.Checkbox("", ref visible))
                    {
                        tileLayer.IsVisible = visible;
                    }

                    ImGui.PopID();

                    ImGui.SameLine();
                    ImGui.PushStyleColor(ImGuiCol.Button, ImGui.GetColorU32(new Vector4(130, 0, 0, 1)));

                    ImGui.PushID(i);
                    if (ImGui.Button("X"))
                    {
                        _tileMap.RemoveTileLayer(tileLayer.Index);
                    }

                    ImGui.PopStyleColor();
                    ImGui.PopID();
                    ImGui.SameLine();

                    ImGui.PushStyleColor(ImGuiCol.Text, color);
                    if (ImGui.Selectable($"#{i} Tile layer"))
                        SelectedTileLayer = tileLayer;
                    ImGui.PopStyleColor();
                }

                ImGui.NewLine();
                if (ImGui.Button("+"))
                    SelectedTileLayer = _tileMap.CreateNewTileLayer();
            }

            ImGui.End();
        }

        private void Handle(ComponentAddedMessage msg)
        {
            if (msg.Component is TileMapComponent tileMapComponent)
                _tileMap = tileMapComponent.TileMap;
        }
        private void Handle(ComponentRemovedMessage msg)
        {
            if (msg.Component is TileMapComponent)
                _tileMap = null;
        }
    }
}