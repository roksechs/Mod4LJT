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
        GameObject weakPointObject;
        float unscaledElapsedTime;
        public static readonly Dictionary<PlayerMachine, LJTMachine> MachineDic = new Dictionary<PlayerMachine, LJTMachine>();

        public PlayerMachine PlayerMachine { get => this.playerMachine; set => this.playerMachine = value; }
        public int TankTypeInt { get => this.tankTypeInt; set => this.tankTypeInt = value; }
        public GameObject WeakPointObject { get => this.weakPointObject; set => this.weakPointObject = value; }

        void Start()
        {
            Events.OnMachineSimulationToggle += OnMachineSimulationToggle;
            if (LJTMachine.MachineDic.ContainsKey(this.playerMachine))
                LJTMachine.MachineDic[this.playerMachine] = this;
            else
                LJTMachine.MachineDic.Add(this.playerMachine, this);
        }

        void OnMachineSimulationToggle(PlayerMachine playerMachine, bool toggle)
        {
            if (this.playerMachine == playerMachine)
            {
                this.SendTankTypeMessage();
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
            if(LJTMachine.MachineDic.TryGetValue(message.Sender.Machine, out LJTMachine ljtMachine))
                ljtMachine.TankTypeInt = ((byte[])message.GetData(0))[0];
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
