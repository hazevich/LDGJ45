using LDGJ45.Messaging;
using StructureMap;

namespace LDGJ45.Bootstrap
{
    public sealed class MessengerRegistry : Registry
    {
        public MessengerRegistry()
        {
            ForConcreteType<Messenger>().Configure.Singleton();

            Forward<Messenger, IPublisher>();
            Forward<Messenger, ISubscriber>();
        }
    }
}