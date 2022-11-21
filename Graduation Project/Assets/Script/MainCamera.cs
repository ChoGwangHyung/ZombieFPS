using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private static MainCamera mainCamera;
    public static MainCamera instance
    {
        get
        {
            if (mainCamera == null)
                mainCamera = FindObjectOfType<MainCamera>();
            return mainCamera;
        }
    }

    [SerializeField] private Transform playerPos;
    [SerializeField] private MouseSensitivity mouseSensitivityUi = null;

    //민감도
    private float sensitivity = 2.0f;
    public float _sensitivity { get { return sensitivity; } }

    //회전
    private float rotLimit = 45.0f;
    private float curRotX = 0.0f;
    private float originRotX = 0.0f;
    private float curRotY = 0.0f;

    private float shakeForce = 10.0f;

    private bool cameraStop = false;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; //커서 고정
    }

    void Update()
    {
        if (cameraStop == true || Player.instance._isDead == true) return;
        CameraMove();
    }

    private void CameraMove()
    {
        float x_rot = Input.GetAxisRaw("Mouse Y");

        curRotX -= x_rot * sensitivity;

        OriginPosOfRecoilAction(x_rot * sensitivity);

        curRotX = Mathf.Clamp(curRotX, -rotLimit, rotLimit);

        this.transform.localEulerAngles = new Vector3(curRotX, curRotY, 0.0f);
    }

    public void StopCamera()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; //커서 풀기

        cameraStop = true;
    }

    public void PlayCamera()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; //커서 고정

        cameraStop = false;
    }

    //weapon 관련함수
    public void DoRecoilAction(float value)
    {
        float recoilVal = value;
        if (originRotX > value / 2) recoilVal *= 1.5f;

        curRotY += Random.Range(-recoilVal / 2, recoilVal / 2);
        curRotX -= recoilVal;
        originRotX += recoilVal;
    }

    private void OriginPosOfRecoilAction(float moveMousePos)
    {
        if (originRotX > 0.0f)
        {
            originRotX -= moveMousePos;
            float recoilVal = originRotX / 30;
            originRotX -= recoilVal;
            curRotX += recoilVal;
        }

        if (curRotY <= 0.1f && curRotY >= -0.1f) curRotY = 0;
        else curRotY -= curRotY / 30;
    }

    //카메라 쉐이킹
    public void ShakeCam()
    {
        StopCoroutine(ShakeCouroutine());
        StartCoroutine(ShakeCouroutine());
    }

    private IEnumerator ShakeCouroutine()
    {
        float startTime = Time.time;
        while (Time.time - startTime < 0.7f)
        {
            curRotY += Random.Range(-shakeForce, shakeForce);
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    public void StartCameraZoom() { GetComponent<Camera>().fieldOfView = 30; }
    public void StopCameraZoom() { GetComponent<Camera>().fieldOfView = 90; }

    //마우스 민감도
    public void UpMouseSensitivity()
    {
        if (sensitivity > 3.0f) return;
        sensitivity += 0.1f;
        mouseSensitivityUi.ControlMouseSensitivity(sensitivity * 10.0f);
    }

    public void DownMouseSensitivity()
    {
        if (sensitivity < 0.2f) return;
        sensitivity -= 0.1f;
        mouseSensitivityUi.ControlMouseSensitivity(sensitivity * 10.0f);
    }
}
