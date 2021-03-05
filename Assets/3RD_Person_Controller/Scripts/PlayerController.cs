using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  #region Variables
  //[SerializeField]
  //private float _speed = 2f;
  private float _jumpDirection;
  [SerializeField]

  private float _maxFwdSpeed = 8f;
  [SerializeField]
  private float _turnSpeed = 100f;
  float _desiredSpeed;
  float _forwardSpeed;
  float _groundRayDist = 2f;
  const float _groundAccel = 5f;
  const float _groundDecel = 25f;

  [SerializeField]
  float _jumpSpeed = 30000f;
  bool readyJump = false;
  bool _onGround = true;

  Animator _animator;
  Rigidbody _rb;

  bool isMoveInput
  {
    get { return !Mathf.Approximately(moveDirection.sqrMagnitude, 0f); }
  }
  Vector2 moveDirection;
  #endregion
  // Start is called before the first frame update
  void Start()
  {
    _animator = GetComponent<Animator>();
    if (_animator == null)
    {
      Debug.LogError("No Animator Component attached to Parent Game Object!!!");

    }

    _rb = GetComponent<Rigidbody>();
    if (_rb == null)
      Debug.LogError("No Rigidbody attached to Parent Game Object!!!");
  }

  // Update is called once per frame
  void Update()
  {
    CalculateMovement(moveDirection);
    Jump(_jumpDirection);
    RaycastHit hit;
    Ray ray = new Ray(transform.position + Vector3.up * _groundRayDist * 0.5f, -Vector3.up);
    if (Physics.Raycast(ray, out hit, _groundRayDist))
    {
      if (!_onGround)
      {
        _onGround = true;
        _animator.SetBool("Land", true);
      }
    }
    else
    {
      _onGround = false;
      _animator.SetBool("Land", false);
    }
    Debug.DrawRay(transform.position + Vector3.up * _groundRayDist * 0.5f, -Vector3.up * _groundRayDist, Color.red);
  }
  #region Custom Methods
  public void OnMove(InputAction.CallbackContext context)
  {
    moveDirection = context.ReadValue<Vector2>();
  }
  public void OnJump(InputAction.CallbackContext context)
  {
    _jumpDirection = context.ReadValue<float>();
  }

  void CalculateMovement(Vector2 direction)
  {
    float turnAmount = direction.x;
    float fDirection = direction.y;

    if (direction.sqrMagnitude > 1f)
    {
      direction.Normalize();
    }
    _desiredSpeed = direction.magnitude * _maxFwdSpeed * Mathf.Sin(fDirection);
    float accel = isMoveInput ? _groundAccel : _groundDecel;

    _forwardSpeed = Mathf.MoveTowards(_forwardSpeed, _desiredSpeed, accel *
    Time.deltaTime);

    _animator.SetFloat("ForwardSpeed", _forwardSpeed);

    transform.Rotate(0, turnAmount * _turnSpeed * Time.deltaTime, 0);
    //transform.Translate(direction.x * _speed * Time.deltaTime, 0, direction.y * _speed * Time.deltaTime);
  }


  void Jump(float direction)
  {
    if (direction > 0)
    {
      _animator.SetBool("ReadyJump", true);
      readyJump = true;
    }
    else if (readyJump)
    {
      _animator.SetBool("Launch", true);
      readyJump = false;
    }
  }
  public void Launch()
  {
    _rb.AddForce(0, _jumpSpeed, 0);
    _animator.SetBool("Launch", false);
    _animator.applyRootMotion = false;
  }
  public void Land()
  {
    _animator.SetBool("ReadyJump", false);
    _animator.SetBool("Land", false);
    _animator.applyRootMotion = true;
  }
  #endregion
}
