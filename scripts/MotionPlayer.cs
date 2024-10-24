using Live2D.Cubism.Framework.Motion;
using UnityEngine;

public class MotionPlayer : MonoBehaviour
{
    CubismMotionController _motionController;

    private void Awake()
    {
        _motionController = GetComponent<CubismMotionController>();
    }

    public void PlayMotion(AnimationClip animation)
    {
        if ((_motionController == null) || (animation == null))
        {
            return;
        }

        _motionController.PlayAnimation(animation, isLoop: false);
    }

    public void PlayMotionFromName(string animationName){
        if (_motionController == null){
            Debug.LogError("Motion controller is null.");
            return;
        }

        AnimationClip animation = Resources.Load<AnimationClip>("Motion/" + animationName);

        if (animation == null){
            Debug.LogError($"Animation {animationName} not found in Resources.");
            return;
        }

        Debug.Log($"MotionPlayer: playing animation {animationName}");
        Debug.Log("---------");
        _motionController.PlayAnimation(animation, isLoop: false);
    }
}
