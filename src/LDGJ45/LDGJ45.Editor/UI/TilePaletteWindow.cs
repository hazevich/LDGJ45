using System;
using System.Drawing;
using System.Numerics;
using ImGuiNET;
using LDGJ45.Core.Messaging;
using LDGJ45.Core.Persistence;
using LDGJ45.Core.TileMaps;
using LDGJ45.Core.World.Messages;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Vector2 = System.Numerics.Vector2;
using Vector4 = System.Numerics.Vector4;

namespace LDGJ45.Editor.UI
{
    public sealed class TilePaletteWindow : IWindow
    {
        private readonly TileSettings _tileSettings;
        private readonly IAssetsDatabase _assetsDatabase;
        private TileMap _tileMap;

        private Texture2D _tilesetTexture;
        private RectangleF _selectedRegion;

        public TilePaletteWindow(ISubscriber subscriber, TileSettings tileSettings, IAssetsDatabase assetsDatabase)
        {
            _tileSettings = tileSettings;
            _assetsDatabase = assetsDatabase;
            subscriber.Subscribe<ComponentAddedMessage>(Handle);
            subscriber.Subscribe<ComponentRemovedMessage>(Handle);
        }

        public TileLayer<Tile> SelectedTileLayer { get; private set; }

        public Texture2D TilesetTexture => _tilesetTexture;
        public ref RectangleF SelectedRegion => ref _selectedRegion;

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

            if (ImGui.CollapsingHeader("Tileset"))
            {
                if (ImGui.BeginCombo("Tilesets", TilesetTexture?.Name ?? "Select tileset"))
                {
                    foreach (var textureName in _tileSettings.TilesetTextures)
                    {
                        if (ImGui.Selectable(textureName))
                        {
                            _tilesetTexture = _assetsDatabase.Load<Texture2D>(textureName);
                            _selectedRegion = new RectangleF(
                                0,
                                0,
                                _tileMap.TileConfiguration.TileWidth,
                                _tileMap.TileConfiguration.TileHeight
                            );
                        }
                    }
                    ImGui.EndCombo();
                }
                var currentPos = ImGui.GetCursorScreenPos();

                if (TilesetTexture != null)
                {
                    ImGui.Image(
                        ImGuiTextureBinder.GetOrCreateBinding(TilesetTexture),
                        new Vector2(TilesetTexture.Width, TilesetTexture.Height)
                    );

                    if (ImGui.IsItemHovered())
                    {
                        var cellSize = new Vector2(
                            _tileMap.TileConfiguration.TileWidth,
                            _tileMap.TileConfiguration.TileHeight
                        );

                        if (ImGui.IsMouseClicked(0) && SelectedRegion != RectangleF.Empty)
                        {
                            var leftTop = Snap(ImGui.GetIO().MouseClickedPos[0] - currentPos, cellSize);
;
                            _selectedRegion.Location = new PointF(leftTop.X, leftTop.Y);

                            var rightBottom = Snap(new Vector2(SelectedRegion.Right, SelectedRegion.Bottom), cellSize);
                            var sizeVector = rightBottom - leftTop;

                            _selectedRegion.Size = new SizeF(sizeVector.X, sizeVector.Y);
                        }

                        if (ImGui.IsMouseDragging(0))
                        {
                            var startPos = ImGui.GetIO().MouseClickedPos[0] - currentPos;
                            var endPos = ImGui.GetMousePos() - currentPos;
                            var min = Snap(Vector2.Min(startPos, endPos), cellSize);
                            var max = Snap(Vector2.Max(startPos, endPos), cellSize);

                            _selectedRegion = RectangleF.FromLTRB(
                                min.X,
                                min.Y,
                                max.X,
                                max.Y
                            );
                        }
                    }

                    var windowDrawList = ImGui.GetWindowDrawList();
                    windowDrawList.AddRect(
                        currentPos + new Vector2(SelectedRegion.Left, SelectedRegion.Top),
                        currentPos + new Vector2(SelectedRegion.Right, SelectedRegion.Bottom),
                        ImGui.GetColorU32(Vector4.One)
                    );
                }

                
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

        private static Vector2 Snap(Vector2 vector, Vector2 cellSize) =>
            new Vector2(
                (int)Math.Floor(vector.X / cellSize.X) * cellSize.X,
                (int)Math.Floor(vector.Y / cellSize.Y) * cellSize.Y
            );
    }
}