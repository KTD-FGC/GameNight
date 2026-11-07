using UnityEngine;

public class CharacterFixedMove : StateMachineBehaviour
{

    CharacterController _controller;
    [SerializeField]
    AnimationCurve _verticalCurve;
    Rigidbody _playerRigidbody;
    public float Velocity => _velocity;
    [SerializeField]
    private float _velocity = 100;
    float direction;

    Transform _startingTransform;
    Vector3 _startingPosition;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _startingTransform = animator.transform.parent;
        _startingPosition = _startingTransform.position;
         Debug.Log("Can't get controller from:"+ animator.transform.parent.gameObject);
        direction = -animator.transform.parent.localScale.x;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _controller = animator.GetComponentInParent<CharacterController>();
       //_playerRigidbody.AddForce(_playerRigidbody.transform.right * direction * Velocity, ForceMode.Force);
        animator.transform.parent.localPosition =  new Vector3(_startingPosition.x,_startingPosition.y +_verticalCurve.Evaluate(stateInfo.normalizedTime),0);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.transform.parent.localPosition =  _startingPosition;
    }

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
