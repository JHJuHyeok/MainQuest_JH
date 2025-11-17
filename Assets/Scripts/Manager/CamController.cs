using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject CameraArm;
    [SerializeField] private Transform objectTofollow;
    [SerializeField] private Transform realCamera;
    private Vector3 FixedPos = new Vector3();
    private Vector3 dirNormalize;

    [Header("카메라 조작")]
    [SerializeField] private float followSpeed = 10f;
    [SerializeField] private float sensitivity = 100f;
    [SerializeField] private float clampAngle = 70f;

    [Header("카메라 수치")]
    [SerializeField] private float xRot;
    [SerializeField] private float yRot;
    [SerializeField] private float SpeedRot = 360f;
    [SerializeField] private float distance;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float smoothness = 10f;

    private void Start()
    {
        xRot = transform.localRotation.eulerAngles.x;
        yRot = transform.localRotation.eulerAngles.y;

        dirNormalize = realCamera.localPosition.normalized;
        distance = realCamera.localPosition.magnitude;
    }

    void Update()
    {
        xRot += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        yRot += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        xRot = Mathf.Clamp(xRot, -clampAngle, clampAngle);
        Quaternion rot = Quaternion.Euler(xRot, yRot, 0);
        transform.rotation = rot;
    }
    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, objectTofollow.position, followSpeed * Time.deltaTime);

        FixedPos = transform.TransformPoint(dirNormalize * maxDistance);

        RaycastHit hit;

        if (Physics.Linecast(transform.position, FixedPos, out hit))
            distance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        else
            distance = maxDistance;

        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalize * distance, Time.deltaTime * smoothness);
    }

    //private void CameraMove()
    //{
    //    FixedPos.Set(
    //        target.transform.position.x + offsetX,
    //        target.transform.position.y + offsetY,
    //        target.transform.position.z + offsetZ);

    //    camera.transform.position = FixedPos;
    //}
    //private void RotateCam()
    //{
    //    //회전
    //    //if (!Input.GetMouseButton(1)) return;

    //    var xMove = Input.GetAxis("Mouse X");
    //    var yMove = -Input.GetAxis("Mouse Y");

    //    _xRot += yMove * SpeedRot * Time.deltaTime;
    //    _yRot += xMove * SpeedRot * Time.deltaTime;

    //    _xRot = Mathf.Clamp(_xRot, -35f, 40f);

    //    CameraArm.transform.localRotation = Quaternion.Euler(_xRot, _yRot, 0f);
    //}
}
