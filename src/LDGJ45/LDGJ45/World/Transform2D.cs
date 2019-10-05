using System;
using Microsoft.Xna.Framework;

namespace LDGJ45.World
{
    public sealed class Transform2D
    {
        private Vector2 _position;
        private bool _positionChanged = true;

        private float _rotation;
        private bool _rotationChanged = true;

        private Vector2 _scale = Vector2.One;
        private bool _scaleChanged = true;

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                _positionChanged = true;
                RecalculateWorldMatrix();
                OnPositionChanged();
            }
        }

        public Vector2 Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                _scaleChanged = true;
                RecalculateWorldMatrix();
                OnScaleChanged();
            }
        }

        public float Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                _rotationChanged = true;
                RecalculateWorldMatrix();
                OnRotationChanged();
            }
        }

        public bool FlipX
        {
            get => _scale.X < 0;
            set
            {
                var scaleX = Math.Abs(_scale.X);
                Scale = new Vector2(value ? -scaleX : scaleX, Scale.Y);
            }
        }

        public Matrix TranslationMatrix { get; private set; } = Matrix.Identity;

        public Matrix InverseTranslationMatrix { get; private set; } = Matrix.Identity;

        public Matrix ScaleMatrix { get; private set; } = Matrix.Identity;

        public Matrix RotationMatrix { get; private set; } = Matrix.Identity;

        public Matrix WorldMatrix { get; private set; } = Matrix.Identity;

        public Matrix WorldToInverseTranslationMatrix { get; private set; } = Matrix.Identity;

        public event Action<Transform2D> PositionChanged;
        public event Action<Transform2D> ScaleChanged;
        public event Action<Transform2D> RotationChanged;

        private void ParentOnPositionChanged(Transform2D obj)
        {
            OnPositionChanged();
        }

        private void RecalculateTranslationMatrix()
        {
            TranslationMatrix = Matrix.CreateTranslation(_position.X, _position.Y, 0);
        }

        private void RecalculateInverseTranslationMatrix()
        {
            InverseTranslationMatrix = Matrix.Invert(TranslationMatrix);
        }

        private void RecalculateScaleMatrix()
        {
            ScaleMatrix = Matrix.CreateScale(_scale.X, _scale.Y, 1);
        }

        private void RecalculateRotationMatrix()
        {
            RotationMatrix = Matrix.CreateRotationZ(_rotation);
        }

        private void RecalculateWorldMatrix()
        {
            if (_positionChanged)
            {
                RecalculateTranslationMatrix();
                RecalculateInverseTranslationMatrix();
                _positionChanged = false;
            }

            if (_scaleChanged)
            {
                RecalculateScaleMatrix();
                _scaleChanged = false;
            }

            if (_rotationChanged)
            {
                RecalculateRotationMatrix();
                _rotationChanged = false;
            }

            WorldMatrix = TranslationMatrix * RotationMatrix * ScaleMatrix;
            WorldToInverseTranslationMatrix = InverseTranslationMatrix * RotationMatrix * ScaleMatrix;
        }

        private void OnPositionChanged()
        {
            PositionChanged?.Invoke(this);
        }

        private void OnScaleChanged()
        {
            ScaleChanged?.Invoke(this);
        }

        private void OnRotationChanged()
        {
            RotationChanged?.Invoke(this);
        }
    }
}