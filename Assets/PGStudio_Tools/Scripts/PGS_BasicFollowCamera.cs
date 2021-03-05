using UnityEngine;

namespace PowerGameStudio.Cameras
{
  public class PGS_BasicFollowCamera : PGS_BaseCamera
  {
    #region Variables
    [SerializeField] [Range(2.0f, 15.0f)] float _distance = 6f;
    [SerializeField] [Range(1f, 15f)] float _height = 5f;
    [SerializeField] [Range(0.5f, 10.0f)] float _smoothSpeed = 2f;

    private Vector3 _wantedPosition;
    private float _wantedYAngle;
    #endregion
    #region Custom Methods
    protected override void HandleCamera()
    {
      base.HandleCamera();
      _wantedYAngle = Mathf.LerpAngle(_wantedYAngle, m_Target.eulerAngles.y, Time.deltaTime * _smoothSpeed);

      Vector3 back = Vector3.back;
      back = Quaternion.AngleAxis(_wantedYAngle, Vector3.up) * back;

      _wantedPosition = (back * _distance) + (Vector3.up * _height) + m_Target.position;

      transform.position = _wantedPosition;

      transform.LookAt(m_Target);
    }
    #endregion

  }

}