using UnityEngine;
using UnityEngine.Rendering;

public class Boss_Walk : StateMachineBehaviour
{
    Transform playerToChase;
    Rigidbody2D bossRB;
    Vector3 directionToMoveIn;
    public float speed = 3f;
    public float attackRange = 3f;

    private float shotCounter;
    [SerializeField] float shotCooldown;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerToChase = FindAnyObjectByType<PlayerController>().transform;
        bossRB = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Vector2 newPosition = Vector2.MoveTowards(bossRB.transform.position, playerToChase.position, speed*Time.fixedDeltaTime);

       if(Vector2.Distance(playerToChase.position, bossRB.transform.position)<attackRange)
        {
            animator.SetTrigger("isAttacking");
        }

        bossRB.MovePosition(newPosition);

        shotCounter -= Time.fixedDeltaTime;
        if(shotCounter <= 0)
        {
            animator.SetTrigger("Shoot");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("isAttacking");
       animator.ResetTrigger("Shoot");
       shotCounter = shotCooldown;
    }
}
