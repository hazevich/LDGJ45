namespace LDGJ45.Core.World.Messages
{
    public sealed class ComponentAddedMessage
    {
        public ComponentAddedMessage(Component component)
        {
            Component = component;
        }

        public Component Component { get; }
    }
}