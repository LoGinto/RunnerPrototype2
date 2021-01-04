using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
   public void PlayTargetedAnim(Animator animator,string animName,bool noInterruption,bool rootMotion)
    {
        if (animator.GetBool("Interuptable")) { return; }
        animator.CrossFade(animName, 0.2f);
        animator.applyRootMotion = rootMotion;
        animator.SetBool("Interuptable", noInterruption);
    }
}
