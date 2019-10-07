namespace ItIron2019.VisualEffectGraphIntro
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.VFX;

    public class VfxCreator : MonoBehaviour
    {
        public GameObject vfxPrefab;

        private GameObject _instance;
        
        void Start()
        {
            _instance = GameObject.Instantiate(vfxPrefab);
            var ve = _instance.GetComponent<VisualEffect>();
            StartCoroutine(SimulateStartStop(ve));
        }

        IEnumerator SimulateStartStop(VisualEffect ve)
        {
            yield return new WaitForSeconds(3.0f);
            Debug.Log($"OnPlay");
            ve.SendEvent("OnPlay");
            yield return new WaitForSeconds(7.0f);
            Debug.Log($"OnStop");
            ve.SendEvent("OnStop");
        }
    }
}
