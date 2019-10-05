using System;
using System.Collections.Generic;

namespace LDGJ45.World
{
    public sealed class World : IDisposable
    {
        private readonly List<GameObject> _gameObjects = new List<GameObject>();
        private bool _disposed;

        public void Dispose()
        {
            if (_disposed) return;

            for (var i = 0; i < _gameObjects.Count; i++)
            {
                var gameObject = _gameObjects[i];

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