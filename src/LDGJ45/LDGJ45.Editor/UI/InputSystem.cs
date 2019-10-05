using ImGuiNET;
using LDGJ45.Core.GameSystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LDGJ45.Editor.UI
{
    public sealed class InputSystem : IGameSystem
    {
        private MouseState _currentState;
        private MouseState _previousState;

        public Vector2 MousePosition => _currentState.Position.ToVector2();

        public bool IsMouseLeftButtonDown() =>
            _currentState.LeftButton == ButtonState.Pressed;

        public bool HasMouseMoved() => _currentState.Position != _previousState.Position;

        public Vector2 DragDelta
        {
            get
            {
                if (HasMouseMoved() && _currentState.RightButton == ButtonState.Pressed &&
                    _previousState.RightButton == ButtonState.Pressed)
                    return (_currentState.Position - _previousState.Position).ToVector2();

                return Vector2.Zero;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (ImGui.GetIO().WantCaptureMouse)
                return;
            
            _previousState = _currentState;
            _currentState = Mouse.GetState();
        }
    }
}