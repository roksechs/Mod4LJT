using Mod4LJT.Network;
using Modding;
using Modding.Blocks;
using System.Collections.Generic;
using UnityEngine;

namespace Mod4LJT.Blocks
{
    class LJTMachine : MonoBehaviour
    {
        PlayerMachine playerMachine;
        int tankTypeInt;
        bool hasCompliance;
        GameObject weakPointObject;
        float unscaledElapsedTime;
        public static readonly Dictionary<ushort, LJTMachine> MachineDic = new Dictionary<ushort, LJTMachine>();

        public PlayerMachine PlayerMachine { get => this.playerMachine; set => this.playerMachine = value; }
        public int TankTypeInt { get => this.tankTypeInt; set => this.tankTypeInt = value; }
        public bool HasCompliance { get => this.hasCompliance; set => this.hasCompliance = value; }
        public GameObject WeakPointObject { get => this.weakPointObject; set => this.weakPointObject = value; }

        public static void OnTankTypeMessageReceive(Message message)
        {
            if (message.Sender.Machine == null) return;
            if (LJTMachine.MachineDic.TryGetValue(message.Sender.Machine.InternalObjectServer.PlayerID, out LJTMachine ljtMachine))
            {
                ljtMachine.TankTypeInt = ((byte[])message.GetData(0))[0];
                ljtMachine.hasCompliance = (bool)message.GetData(1);
            }
            else
            {
                LJTMachine newLJTMachine = message.Sender.Machine.InternalObject.gameObject.GetComponent<LJTMachine>();
                if (!newLJTMachine)
                    newLJTMachine = message.Sender.Machine.InternalObject.gameObject.AddComponent<LJTMachine>();
                newLJTMachine.playerMachine = message.Sender.Machine;
            }
        }

        internal void Initialise(PlayerMachine playerMachine)
        {
            this.playerMachine = playerMachine;

            if (LJTMachine.MachineDic.ContainsKey(this.playerMachine.InternalObjectServer.PlayerID))
            {
                LJTMachine.MachineDic[this.playerMachine.InternalObjectServer.PlayerID] = this;
            }
            else
            {
                LJTMachine.MachineDic.Add(this.playerMachine.InternalObjectServer.PlayerID, this);
            }
        }

        void SendTankTypeMessage()
        {
            byte[] data = new byte[1] { (byte)this.tankTypeInt };
            Message message = LJTMessages.tankTypeMessage.CreateMessage(data, this.hasCompliance);
            ModNetworking.SendToAll(message);
        }

        void ApplyDamage()
        {
            this.playerMachine.InternalObjectServer.DamageController.ResetTotalDamage();
            this.playerMachine.InternalObjectServer.DamageController.AddTotalDamage(1f);
            this.playerMachine.InternalObjectServer.DamageController.ApplyJointDamage(100f);
            this.playerMachine.InternalObjectServer.DamageController.ResetTotalDamage();
            this.playerMachine.InternalObjectServer.DamageController.Toggle(false);
        }

        void Update()
        {
            if (this.playerMachine.InternalObject.isSimulating)
            {
                if (this.weakPointObject && (StatMaster.isHosting || StatMaster.isLocalSim))
                    if (!this.weakPointObject.activeSelf)
                        this.ApplyDamage();
            }
            else
            {
                if (this.unscaledElapsedTime > 1f)
                {
                    if (this.playerMachine.InternalObjectServer.isLocalMachine)
                        this.SendTankTypeMessage();
                    this.unscaledElapsedTime = 0f;
                }
                else
                {
                    this.unscaledElapsedTime += Time.unscaledDeltaTime;
                }
            }
        }

        void OnDestroy()
        {
            LJTMachine.MachineDic.Remove(this.playerMachine.InternalObjectServer.PlayerID);
        }
    }
}
