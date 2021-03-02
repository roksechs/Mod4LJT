using Modding;
using Mod4LJT.Blocks;

namespace Mod4LJT.Network
{
    public static class LJTMessages
    {
        public static MessageType tankTypeMessage;

        public static void CreateMessageTypes()
        {
            tankTypeMessage = ModNetworking.CreateMessageType(DataType.ByteArray);
            ModNetworking.Callbacks[tankTypeMessage] += LJTMachine.OnTankTypeMessageReceive;
        }
    }
}
