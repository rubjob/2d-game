using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Ultimate : BaseEntityState
    {
        [Header("Dependecy")]
        public Animator animator;
        public ActionManager ActionManager;

        [Header("Constant")]
        public float SkillCooldown = 1f;
        public float AnimationDuration = 1f;
        public int MaxUltimatePoint = 7;

        public int CurrentUltimatePoint { private set; get; } = 0;
        public bool IsUsingUltimate { private set; get; } = false;

        public override float BaseAttackDamage => throw new System.NotImplementedException();
        public override float BaseAttackSpeed => 1f / AnimationDuration;
        public override float BaseCooldownDuration => 0;
        public override HitboxManager Hitbox => throw new System.NotImplementedException();
        public override void OnDealingDamage()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator OnPlayingAnimation()
        {
            if (CurrentUltimatePoint == MaxUltimatePoint)
            {
                animator.SetTrigger("Ultimate");
                yield return new WaitForSeconds(AnimationDuration);
                StartCoroutine(CoUltimating());
            }
        }

        public void AddUltimatePoint(int pt)
        {
            CurrentUltimatePoint = Mathf.Clamp(CurrentUltimatePoint + pt, 0, MaxUltimatePoint);

            Debug.Log($"({CurrentUltimatePoint}/{MaxUltimatePoint}) ultimate points");
        }

        public void ReduceUltimatePoint(int pt)
        {
            if (!IsUsingUltimate) return;

            CurrentUltimatePoint = Mathf.Clamp(CurrentUltimatePoint - pt, 0, MaxUltimatePoint);

            Debug.Log($"({CurrentUltimatePoint}/{MaxUltimatePoint}) ultimate points");
        }

        private IEnumerator CoUltimating()
        {
            IsUsingUltimate = true;

            ActionManager.SetCooldownByPass(true, SkillCooldown);

            while (IsUsingUltimate)
            {
                if (CurrentUltimatePoint == 0) IsUsingUltimate = false;
                yield return new WaitForFixedUpdate();
            }

            ActionManager.SetCooldownByPass(false);
        }
    }

}