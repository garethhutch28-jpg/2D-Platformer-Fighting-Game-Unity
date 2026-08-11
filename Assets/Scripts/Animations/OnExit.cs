using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimatorBrainBrute;

public class OnExit : StateMachineBehaviour
{
    [SerializeField] private Animations animation;
    [SerializeField] private bool lockLayer;
    [SerializeField] private float crossfade = 0.2f;
    [HideInInspector] public bool cancel = false;
    [HideInInspector] public int layerIndex = -1;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       /* this.layerIndex = layerIndex;
        cancel = false;
        PlatMovement.instance.StartCoroutine(Wait());

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(stateInfo.length - crossfade);

            if (cancel) yield break;

            AnimatorBrainBrute target = animator.GetComponent<AnimatorBrainBrute>();
            target.SetLocked(false, layerIndex);
            target.Play(animation, layerIndex, lockLayer, false, crossfade);
        }*/
    }
}
