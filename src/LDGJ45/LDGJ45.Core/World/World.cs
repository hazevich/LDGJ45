using System;
using System.Collections.Generic;

namespace LDGJ45.Core.World
{
    public sealed class World : IDisposable
    {
        private readonly List<GameObject> _gameObjects = new List<GameObject>();
        private bool _disposed;

        public IReadOnlyList<GameObject> GameObjects => _gameObjects;

        public void Dispose()
        {
            if (_disposed) return;

            for (var i = 0; i < GameObjects.Count; i++)
            {
                var gameObject = GameObjects[i];

                gameObject.Dispose();
                RemoveGameObject(gameObject);
            }

            _disposed = true;
        }

        public void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
        }
    }
}