namespace LDGJ45.Core.World
{
    public abstract class UpdateableComponent : Component
    {
        protected internal virtual void Awake()
        {
        }

        protected internal abstract void Update();
    }
}