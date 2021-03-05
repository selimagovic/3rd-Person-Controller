using UnityEngine;

namespace PowerGameStudio.Cameras
{
  public class PGS_AdvancedFollowCamera : PGS_BaseCamera
  {

    #region Variables
    [SerializeField] [Range(2.0f, 20.0f)] float _distance = 10f;
    [SerializeField] [Range(1f, 12f)] float _height = 5f;
    [SerializeField] [Range(0.25f, 10.0f)] float _smoothSpeed = 2f;

    private Vector3 _wantedPosition;
    private Vector3 smoothVelocity;
    #endregion
    #region Builtin Methods
    #endregion
    #region Custom Methods

    protected override void HandleCamera()
    {
      base.HandleCamera();
      _wantedPosition = m_Target.position + (-m_Target.forward * _distance) + (Vector3.up * _height);


      transform.position = Vector3.SmoothDamp(transform.position, _wantedPosition, ref smoothVelocity, _smoothSpeed);
      transform.LookAt(m_Target);
    }
    #endregion
  }
}
