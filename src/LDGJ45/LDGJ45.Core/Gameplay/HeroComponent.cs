using LDGJ45.Core.World;
using Microsoft.Xna.Framework.Input;

namespace LDGJ45.Core.Gameplay
{
    public sealed class HeroComponent : UpdateableComponent
    {
        protected internal override void Update()
        {
            var keyboard = Keyboard.GetState();

            var position = Transform.Position;

            if (keyboard.IsKeyDown(Keys.A))
                position.X -= 10;
            else if (keyboard.IsKeyDown(Keys.D))
                position.X += 10;

            if (keyboard.IsKeyDown(Keys.W))
                position.Y -= 10;
            else if (keyboard.IsKeyDown(Keys.S))
                position.Y += 10;

            Transform.Position = position;
        }
    }
}