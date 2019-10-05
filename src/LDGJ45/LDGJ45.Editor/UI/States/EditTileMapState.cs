using System;
using System.Drawing;
using LDGJ45.Core.Graphics;
using LDGJ45.Core.TileMaps;
using LDGJ45.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace LDGJ45.Editor.UI.States
{
    public sealed class EditTileMapState : IEditorAppState, IRenderable
    {
        private readonly TilePaletteWindow _tilePalette;
        private readonly InputSystem _inputSystem;
        private readonly Renderer _renderer;

        public EditTileMapState(TilePaletteWindow tilePalette, InputSystem inputSystem, Renderer renderer)
        {
            _tilePalette = tilePalette;
            _inputSystem = inputSystem;
            _renderer = renderer;

            _renderer.Add(this);
        }


        public void Update()
        {
            if (_tilePalette.SelectedTileLayer == null)
                return;

            if (_inputSystem.IsMouseLeftButtonDown())
            {
                var worldPosition = Vector2.Transform(
                    _inputSystem.MousePosition,
                    Matrix.Invert(_renderer.Camera.WorldMatrix)
                );

                var tilePosition = worldPosition / new Vector2(
                                       _tilePalette.SelectedTileLayer.TileConfiguration.TileWidth,
                                       _tilePalette.SelectedTileLayer.TileConfiguration.TileHeight
                                   );

                _tilePalette.SelectedTileLayer.Upsert(
                    new Tile(
                        (int) tilePosition.Y,
                        (int) tilePosition.X,
                        _tilePalette.TilesetTexture,
                        new Rectangle(
                            (int) _tilePalette.SelectedRegion.X,
                            (int) _tilePalette.SelectedRegion.Y,
                            (int) _tilePalette.SelectedRegion.Width,
                            (int) _tilePalette.SelectedRegion.Height
                        )
                    )
                );
                _tilePalette.Map.AddCollision((int) tilePosition.Y, (int) tilePosition.X);
            }

            _renderer.Camera.Transform.Position -= _inputSystem.DragDelta;
        }

        public int Order { get; } = 999;

        public void Render(SpriteBatch spriteBatch)
        {
            if (_tilePalette.TilesetTexture == null) return;
            

            var rectangle = _tilePalette.SelectedRegion;
            var worldPosition = Vector2.Transform(
                _inputSystem.MousePosition,
                Matrix.Invert(_renderer.Camera.WorldMatrix)
            );

            worldPosition = Snap(
                worldPosition,
                new Vector2(
                    _tilePalette.SelectedTileLayer.TileConfiguration.TileWidth,
                    _tilePalette.SelectedTileLayer.TileConfiguration.TileHeight
                )
            );

            rectangle.Location = new PointF(worldPosition.X, worldPosition.Y);
            spriteBatch.Render(
                new Sprite(
                    new TextureRegion2D(
                        _tilePalette.TilesetTexture,
                        new Rectangle(
                            (int) _tilePalette.SelectedRegion.X,
                            (int) _tilePalette.SelectedRegion.Y,
                            (int) _tilePalette.SelectedRegion.Width,
                            (int) _tilePalette.SelectedRegion.Height
                        )
                    )
                ),
                worldPosition
            );
        }

        private static Vector2 Snap(Vector2 vector, Vector2 cellSize) =>
            new Vector2(
                (int)Math.Floor(vector.X / cellSize.X) * cellSize.X,
                (int)Math.Floor(vector.Y / cellSize.Y) * cellSize.Y
            );
    }
}