using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EntityBehavior {
    public class StatusActor : MonoBehaviour
    {
        [Header("Dependency")]
        public HealthScript HealthScript;

        private readonly List<Status> StatusList = new();
        private readonly Dictionary<String, float> StatusEndTime = new();
        private readonly Dictionary<String, Coroutine> StatusCoroutine = new();

        public void ApplyStatus(Status Status) {
            StatusEndTime[Status.Name] = Time.time + Status.Duration;

            Predicate<Status> Finder = new(delegate (Status s) {
                return s.Name == Status.Name;
            });

            if (StatusList.Find(Finder) == null) {
                StatusList.Add(Status);
                StatusCoroutine[Status.Name] = StartCoroutine(CoStatusEffect(Status));
            }
        }

        private IEnumerator CoStatusEffect(Status s) {
            yield return new WaitForFixedUpdate();

            HealthScript.SetAdditionalMaxHealth(s.MaxHealthBoost);
            HealthScript.Heal(s.HealthBoost);

            while (Time.time <= StatusEndTime[s.Name]) {
                HealthScript.TakeDamage(s.Damage);

                if (s.Type == StatusApplyingType.Instant) break;

                yield return new WaitForSeconds(s.TriggerEvery);
            }

            HealthScript.SetAdditionalMaxHealth(0);

            StatusList.Remove(s);
            StatusCoroutine.Remove(s.Name);
        }

        public Status[] GetDebuff() {
            return StatusList.ToArray();
        }

        public float GetStatusDuration(Status s) {
            return StatusEndTime[s.Name] - Time.time;
        }
    }
}