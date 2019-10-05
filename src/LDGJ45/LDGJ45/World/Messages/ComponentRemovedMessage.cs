namespace LDGJ45.World.Messages
{
    public sealed class ComponentRemovedMessage
    {
        public ComponentRemovedMessage(Component component)
        {
            Component = component;
        }

        public Component Component { get; }
    }
}