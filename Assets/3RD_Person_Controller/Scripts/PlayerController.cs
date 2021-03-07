using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  #region Variables
  //[SerializeField]
  //private float _speed = 2f;
  public Transform hand;
  public Transform weapon;
  public Transform hip;
  private float _jumpDirection;
  [SerializeField]
  private float _maxFwdSpeed = 8f;
  [SerializeField]
  private float _turnSpeed = 100f;
  float _desiredSpeed;
  float _forwardSpeed;
  float _groundRayDist = 0.5f;
  const float _groundAccel = 5f;
  const float _groundDecel = 25f;

  float _jumpEffort = 0;

  [SerializeField]
  float _jumpSpeed = 30000f;
  [SerializeField]
  [Range(1, 4)]
  float _maxJumpEffort = 1;
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

  #region Builtin Methods

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
        _animator.SetFloat("LandingVelocity", _rb.velocity.magnitude);
        _animator.SetBool("Land", true);
        _animator.SetBool("Falling", false);
      }
    }
    else
    {
      _onGround = false;
      _animator.SetBool("Falling", true);
      _animator.applyRootMotion = false;
    }
    Debug.DrawRay(transform.position + Vector3.up * _groundRayDist * 0.5f, -Vector3.up * _groundRayDist, Color.red);
  }
  #endregion
  // Start is called before the first frame update

  #region Custom Methods

  public void PickupGun()
  {
    weapon.SetParent(hand);
    weapon.localPosition = new Vector3(0.0633f, -0.01940f, 0.0152f);
    weapon.localRotation = Quaternion.Euler(-106.712f, 122.92f, 66.23f);
    weapon.localScale = new Vector3(1, 1, 1);
  }

  public void PutDownGun()
  {
    weapon.SetParent(hip);
    weapon.localPosition = new Vector3(0.091f, 0.0262f, -0.0691f);
    weapon.localRotation = Quaternion.Euler(-1.6f, 104.3f, 101.3f);
    weapon.localScale = new Vector3(1, 1, 1);
  }
  public void OnMove(InputAction.CallbackContext context)
  {
    moveDirection = context.ReadValue<Vector2>();
  }
  public void OnJump(InputAction.CallbackContext context)
  {
    _jumpDirection = context.ReadValue<float>();
  }

  public void OnArmed(InputAction.CallbackContext context)
  {
    _animator.SetBool("Armed", !_animator.GetBool("Armed"));
  }
  public void OnFire(InputAction.CallbackContext context)
  {
    if (context.action != null && _animator.GetBool("Armed"))
      _animator.SetTrigger("Fire");
  }

  public void FixAimingWeaponPosRot()
  {
    weapon.localPosition = new Vector3(0.062f, 0.003f, -0.007f);
    weapon.localRotation = Quaternion.Euler(-101.32f, 136.898f, 40.051f);
    weapon.localScale = new Vector3(1, 1, 1);
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

    _forwardSpeed = Mathf.MoveTowards(_forwardSpeed, _desiredSpeed, accel * Time.deltaTime);

    _animator.SetFloat("ForwardSpeed", _forwardSpeed);

    transform.Rotate(0, turnAmount * _turnSpeed * Time.deltaTime, 0);
  }


  void Jump(float direction)
  {
    if (direction > 0)
    {
      _animator.SetBool("ReadyJump", true);
      readyJump = true;
      _jumpEffort += Time.deltaTime;
      Debug.Log(_jumpEffort);
    }
    else if (readyJump)
    {
      _animator.SetBool("Launch", true);
      readyJump = false;
      _animator.SetBool("ReadyJump", false);
    }

  }
  public void Launch()
  {
    _rb.AddForce(0, _jumpSpeed * Mathf.Clamp(_jumpEffort, 1, _maxJumpEffort), 0);
    _animator.SetBool("Launch", false);
    _animator.applyRootMotion = false;
  }
  public void Land()
  {
    _animator.SetBool("Land", false);
    _animator.applyRootMotion = true;
    _animator.SetBool("Launch", false);
    _jumpEffort = 0;
  }
  #endregion
}
