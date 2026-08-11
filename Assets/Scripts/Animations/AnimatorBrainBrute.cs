using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBrainBrute : MonoBehaviour
{

    private readonly static int[] animations =
     {
        Animator.StringToHash("Idle"),
        Animator.StringToHash("Run"),
        Animator.StringToHash("Jump"),
        Animator.StringToHash("Fall"),
        Animator.StringToHash("Death"),
        Animator.StringToHash("Sidelight"),
        Animator.StringToHash("Downlight"),
        Animator.StringToHash("Neutrallight"),
        Animator.StringToHash("Sideheavy"),
        Animator.StringToHash("Neutralheavy"),
        Animator.StringToHash("Sideair"),
        Animator.StringToHash("Downair"),
        Animator.StringToHash("Neutralair"),
    };
    private Animator animator;
    private Animations[] currentAnimation;
    private bool[] layerLocked;
    public Action<int> DefaultAnimation;

    protected void Initialize(int layers, Animations startingAnimation, Animator animator, Action<int> DefaultAnimation)
    {
        layerLocked = new bool[layers];
        currentAnimation = new Animations[layers];
        this.animator = animator;
        this.DefaultAnimation = DefaultAnimation;

        for (int i = 0; i < layers; i++)
        {
            layerLocked[i] = false;
            currentAnimation[i] = startingAnimation;
        }
    }
    public Animations GetCurrentAnimation(int layer)
    {
        return currentAnimation[layer];
    }
    private void SetLocked(bool lockLayer, int layer)
    {
        layerLocked[layer] = lockLayer;

    }
    public void Play(Animations animation, int layer, bool lockLayer, bool bypassLock, float crossfade = 0.2f)
    {
        if (animation == Animations.NONE)
        {
            DefaultAnimation(layer);
            return;
        }

        if (layerLocked[layer] && !bypassLock) return;
        layerLocked[layer] = lockLayer;

       /* if (bypassLock)
            foreach (var item in animator.GetBehaviours<OnExit>())
                if (item.layerIndex == layer)
                    item.cancel = true;
       */
        if (currentAnimation[layer] == animation) return;

        currentAnimation[layer] = animation;
        animator.CrossFade(animations[(int)currentAnimation[layer]], crossfade, layer);
    }
    public enum Animations
    {
        IDLE,
        RUN,
        JUMP,
        FALL,
        DEATH,
        SIDELIGHT,
        DOWNLIGHT,
        NEUTRALLIGHT,
        SIDEHEAVY,
        NEUTRALHEAVY,
        SIDEAIR,
        DOWNAIR,
        NEUTRALAIR,
        NONE
    }
}
