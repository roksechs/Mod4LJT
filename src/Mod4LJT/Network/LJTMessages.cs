using Modding;

namespace Mod4LJT.Network
{
    public static class LJTMessages
    {
        public static MessageType tankTypeMessage;

        public static void CreateMessageTypes()
        {
            //tankTypeMessage = ModNetworking.CreateMessageType(DataType.ByteArray, DataType.Boolean);
            //ModNetworking.Callbacks[tankTypeMessage] += LJTMachine.OnTankTypeMessageReceive;
        }
    }
}
