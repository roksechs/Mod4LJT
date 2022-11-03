using Mod4LJT.Regulation;
using Modding.Blocks;
using System.Collections.Generic;
using UnityEngine;

namespace Mod4LJT.Blocks
{
    class LJTMachine : MonoBehaviour
    {
        PlayerMachine playerMachine;
        private Machine machine;
        public MMenu tankTypeMenu;
        bool hasCompliance;
        public static readonly Dictionary<PlayerMachine, LJTMachine> MachineDic = new Dictionary<PlayerMachine, LJTMachine>();

        public PlayerMachine PlayerMachine { get => this.playerMachine; set => this.playerMachine = value; }
        public int TankTypeInt => this.tankTypeMenu.Value;
        // TODO Deal with a HasCompliance problem
        public bool HasCompliance { get => this.hasCompliance; set => this.hasCompliance = value; }

        void Awake()
        {
            this.machine = this.gameObject.GetComponent<Machine>();
        }

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

        void OnDestroy()
        {
            LJTMachine.MachineDic.Remove(this.playerMachine);
        }
    }
}
