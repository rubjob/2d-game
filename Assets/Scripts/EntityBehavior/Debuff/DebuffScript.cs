using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityBehavior
{
    public class DebuffScript : MonoBehaviour
    {
        public Debuff ApplyingDebuff;

        public void ApplyDebuff(GameObject Target) {
            DebuffActor Actor;
            if ((Actor = Target.GetComponent<DebuffActor>()) == null) return;

            Actor.ApplyDebuff(ApplyingDebuff);
        }
    }
}
