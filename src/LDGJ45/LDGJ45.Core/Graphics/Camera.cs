using LDGJ45.Core.World;
using Microsoft.Xna.Framework;

namespace LDGJ45.Core.Graphics
{
    public class Camera
    {
        private Vector2 _center;

        public Camera(int width, int height, Transform2D transform)
        {
            Width = width;
            Height = height;
            Transform = transform;

            _center = new Vector2(Width / 2, Height / 2);
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public Transform2D Transform { get; }

        public Vector2 Center => _center;
        public Vector2 CenterPosition => Transform.Position + _center;

        public Matrix WorldMatrix => Transform.WorldToInverseTranslationMatrix;

        public void FocusOn(Vector2 position)
        {
            Transform.Position = position - _center;
        }

        public void Resize(int width, int height)
        {
            var position = Transform.Position + _center;

            Width = width;
            Height = height;

            _center = new Vector2(Width / 2, Height / 2);
            FocusOn(position);
        }
    }
}