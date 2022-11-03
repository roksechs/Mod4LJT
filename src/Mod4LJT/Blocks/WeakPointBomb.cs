using UnityEngine;

namespace Mod4LJT.Blocks
{
    class WeakPointBomb : MonoBehaviour
    {
        public MachineDamageController machineDamageController;
        public bool isActive;
        private bool isExploded = false;

        public void Start()
        {
            EntryPoint.Log(this.isActive.ToString());
        }

        public void OnDisable()
        {
            if (this.isActive && !this.isExploded && (StatMaster.isHosting || StatMaster.isLocalSim))
            {
                this.machineDamageController.ResetTotalDamage();
                this.machineDamageController.AddTotalDamage(1f);
                this.machineDamageController.ApplyJointDamage(1000f);
                this.machineDamageController.ResetTotalDamage();
                this.machineDamageController.Toggle(false);
            }
            this.isExploded = true;
        }
    }
}
