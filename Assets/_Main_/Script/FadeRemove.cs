using UnityEngine;

public class FadeRemove : StateMachineBehaviour
{
    public float fadeTime = 0.5f; // Time it takes to fade out
    private float timer; // Timer to track the fade time
    SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    Color startColor;
    GameObject objToRm;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f; // Reset the timer when the state is entered
        spriteRenderer = animator.GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        startColor = spriteRenderer.color; // Store the original color of the sprite
        objToRm = animator.gameObject; // Get the GameObject to remove
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime; // Increment the timer by the time elapsed since the last frame

        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(1f, 0f, timer / fadeTime)); // Fade out the sprite by adjusting its alpha value
        if (timer > fadeTime)
        {
            Destroy(objToRm); // Destroy the GameObject after the fade time has elapsed
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
