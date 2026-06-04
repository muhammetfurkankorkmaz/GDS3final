using UnityEngine;

public class CHAnimation : MonoBehaviour
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
        print("Playng  " + newState + "  " + Time.time);

        currentState = newState;
    }
}//Class
