using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EntityBehavior {
    public class StatusActor : MonoBehaviour
    {
        [Header("Dependency")]
        public HealthScript HealthScript;

        private Status Status;
        private Coroutine coDebuffEffect;

        private float EndStatus = 0f;

        public void ApplyStatus(Status Status) {
            this.Status = Status;
            EndStatus = Time.time + Status.Duration;

            if (coDebuffEffect == null) {
                coDebuffEffect = StartCoroutine(CoDebuffEffect());
            }
        }

        private IEnumerator CoDebuffEffect() {
            yield return new WaitForFixedUpdate();

            while (Time.time <= EndStatus) {
                HealthScript.TakeDamage(Status.Damage);
                yield return new WaitForSeconds(Status.TriggerEvery);
            }

            Status = null;
            coDebuffEffect = null;
        }

        public Status GetDebuff() {
            return Status;
        }

        public float GetStatusDuration() {
            return EndStatus - Time.time;
        }
    }
}