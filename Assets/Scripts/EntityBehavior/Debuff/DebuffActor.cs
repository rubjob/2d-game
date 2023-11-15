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
        private Coroutine coDebuffEffect;

        private float EndDebuff = 0f;

        public void ApplyDebuff(Debuff Debuff) {
            this.Debuff = Debuff;
            EndDebuff = Time.time + Debuff.Duration;

            if (coDebuffEffect == null) {
                coDebuffEffect = StartCoroutine(CoDebuffEffect());
            }
        }

        private IEnumerator CoDebuffEffect() {
            yield return new WaitForFixedUpdate();

            while (Time.time <= EndDebuff) {
                HealthScript.TakeDamage(Debuff.Damage);
                yield return new WaitForSeconds(Debuff.TriggerEvery);
            }

            Debuff = null;
            coDebuffEffect = null;
        }

        public Debuff GetDebuff() {
            return Debuff;
        }

        public float GetDebuffDuration() {
            return EndDebuff - Time.time;
        }
    }
}