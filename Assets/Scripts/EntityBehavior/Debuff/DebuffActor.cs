using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EntityBehavior {
    public class DebuffActor : MonoBehaviour
    {
        [Header("Dependency")]
        public HealthScript HealthScript;

        private Debuff Debuff;

        public void ApplyDebuff(Debuff Debuff) {
            this.Debuff = Debuff;

            StopAllCoroutines();
            StartCoroutine(CoDebuffEffect());
            StartCoroutine(CoDebuffDuration());
        }

        private IEnumerator CoDebuffEffect() {
            yield return new WaitForFixedUpdate();

            while (Debuff != null) {
                HealthScript.TakeDamage(Debuff.Damage);
                yield return new WaitForSeconds(Debuff.TriggerEvery);
            }
        }

        private IEnumerator CoDebuffDuration() {
            yield return new WaitForSeconds(Debuff.Duration);
            Debuff = null;
        }
    }
}