namespace LDGJ45.World
{
    public abstract class UpdateableComponent : Component
    {
        protected internal virtual void Awake()
        {
        }

        protected internal abstract void Update();
    }
}