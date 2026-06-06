using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    Animator animator;
    public string currentState { get; private set; }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ChangeState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }
}//Class
