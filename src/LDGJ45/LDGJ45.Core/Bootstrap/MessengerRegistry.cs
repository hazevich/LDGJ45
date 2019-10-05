using LDGJ45.Core.Messaging;
using StructureMap;

namespace LDGJ45.Core.Bootstrap
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