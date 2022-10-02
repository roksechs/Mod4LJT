using Localisation;
using UnityEngine;

namespace Mod4LJT.Localisation
{
    class LocalisationManager : MonoBehaviour, ILocalisationAware
    {
        public void OnLocalisationChange()
        {
            EntryPoint.Log("Localisation is changed");
        }
    }
}
