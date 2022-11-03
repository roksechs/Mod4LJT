using UnityEngine;

namespace Mod4LJT.Blocks
{
    class WeakPointBomb : MonoBehaviour
    {
        public MachineDamageController machineDamageController;
        private bool isExploded;

        public void Awake()
        {
            this.isExploded = false;
        }

        public void OnDisable()
        {
            if (!this.isExploded && (StatMaster.isHosting || StatMaster.isLocalSim))
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
