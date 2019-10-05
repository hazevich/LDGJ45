using Microsoft.Xna.Framework;

namespace LDGJ45.Core.GameSystems
{
    public sealed class GameClock : IGameSystem
    {
        public GameTime GameTime { get; private set; }

        public void Update(GameTime gameTime) => GameTime = gameTime;
    }
}