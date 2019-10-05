using LDGJ45.World;
using Microsoft.Xna.Framework.Input;

namespace LDGJ45.Gameplay
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

            Transform.Position = position;
        }
    }
}