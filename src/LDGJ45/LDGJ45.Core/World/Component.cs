namespace LDGJ45.Core.World
{
    public abstract class Component
    {
        public GameObject GameObject { get; internal set; }

        public Transform2D Transform => GameObject.Transform;

        public void AddComponent(Component component)
        {
            GameObject.AddComponent(component);
        }

        protected internal virtual void Attached()
        {
        }

        protected internal virtual void Detached()
        {
        }
    }
}