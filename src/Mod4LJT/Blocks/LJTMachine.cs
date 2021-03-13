using System.Collections.Generic;
using UnityEngine;
using Modding;
using Modding.Blocks;
using Mod4LJT.Network;

namespace Mod4LJT.Blocks
{
    class LJTMachine : MonoBehaviour
    {
        PlayerMachine playerMachine;
        int tankTypeInt;
        //bool hasCompliance;
        GameObject weakPointObject;
        float unscaledElapsedTime;
        public static readonly Dictionary<PlayerMachine, LJTMachine> MachineDic = new Dictionary<PlayerMachine, LJTMachine>();

        public PlayerMachine PlayerMachine { get => this.playerMachine; set => this.playerMachine = value; }
        public int TankTypeInt { get => this.tankTypeInt; set => this.tankTypeInt = value; }
        //public bool HasCompliance { get => this.hasCompliance; set => this.hasCompliance = value; }
        public GameObject WeakPointObject { get => this.weakPointObject; set => this.weakPointObject = value; }

        void Start()
        {
            if (LJTMachine.MachineDic.ContainsKey(this.playerMachine))
            {
                LJTMachine.MachineDic[this.playerMachine] = this;
            }
            else
            {
                LJTMachine.MachineDic.Add(this.playerMachine, this);
            }
        }

        void SendTankTypeMessage()
        {
            byte[] data = new byte[1] { (byte)this.tankTypeInt };
            Message message = LJTMessages.tankTypeMessage.CreateMessage(data);
            ModNetworking.SendToAll(message);
        }

        public static void OnTankTypeMessageReceive(Message message)
        {
            if (message.Sender.Machine == null) return;
            if (LJTMachine.MachineDic.TryGetValue(message.Sender.Machine, out LJTMachine ljtMachine))
            {
                ljtMachine.TankTypeInt = ((byte[])message.GetData(0))[0];
                //ljtMachine.hasCompliance = (bool)message.GetData(1);
            }
            else
            {
                LJTMachine newLJTMachine = message.Sender.Machine.InternalObject.gameObject.GetComponent<LJTMachine>();
                if (!newLJTMachine)
                    newLJTMachine = message.Sender.Machine.InternalObject.gameObject.AddComponent<LJTMachine>();
                newLJTMachine.playerMachine = message.Sender.Machine;
            }
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
    }
}
