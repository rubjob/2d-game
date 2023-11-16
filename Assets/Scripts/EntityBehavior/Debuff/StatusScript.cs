using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityBehavior
{
    public class DebuffScript : MonoBehaviour
    {
        public Status ApplyingStatus;

        public void ApplyDebuff(GameObject Target) {
            StatusActor Actor;
            if ((Actor = Target.GetComponent<StatusActor>()) == null) return;

            Actor.ApplyStatus(ApplyingStatus);

        }
    }
}
