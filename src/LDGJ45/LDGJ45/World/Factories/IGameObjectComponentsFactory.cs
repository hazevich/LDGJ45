using System.Collections.Generic;

namespace LDGJ45.World.Factories
{
    public interface IGameObjectComponentsFactory
    {
        GameObjectType GameObjectType { get; }
        IEnumerable<Component> Create();
    }
}