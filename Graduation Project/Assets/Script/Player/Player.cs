using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase
{
    private static Player player;
    public static Player instance
    {
        get
        {
            if (player == null)
                player = FindObjectOfType<Player>();
            return player;
        }
    }

    [SerializeField] private PlayerWeaponController weaponController = null;
    public PlayerWeaponController _weaponController { get { return weaponController; } }
    [SerializeField] private GameObject arm = null;

    [SerializeField] private GameObject deadCam = null;
    [SerializeField] private GameObject deadCharacter = null;

    //이동
    private float curSpeed;
    private float runSpeed;
    private float crouchSpeed;
    private bool isRun = false;
    private bool isWalk = false;

    //점프
    private float jumpForce = 7.0f;
    private bool isGround = true;

    //앉기
    private bool isCrouch = false;

    //콜라이더 크기 조절
    private float colOriginHeight;
    private float colCrouchHeight = 1.0f;

    private bool playerStop = false;
    public void StopPlayer() { playerStop = true; CrossHair.instance.gameObject.SetActive(false); }
    public void PlayPlayer() { playerStop = false; CrossHair.instance.gameObject.SetActive(true); }

    void Start()
    {
        ani = arm.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        colOriginHeight = col.height;

        curSpeed = stat._walkSpeed;
        runSpeed = curSpeed * 1.5f;
        crouchSpeed = curSpeed * 0.6f;

        deadCam.SetActive(false);
        deadCharacter.SetActive(false);
}

    void Update()
    {
        if (playerStop == true || isDead == true) return;

        //플레이어 이동      
        PlayerRotation();
        PlayerMove();
        IsGround();
        PlayerJump();
        PlayerRun();
        PlayerCrouch();

        //사격
        IsTargetInCrossHair();
        WeaponChange();
        Fire();
        Reload();
        Zoom();

        ControlMouseSensitivity();
    }

    //이동
    private void PlayerMove()
    {
        float xDir = Input.GetAxisRaw("Horizontal");
        float zDir = Input.GetAxisRaw("Vertical");

        Vector3 horizontal = transform.right * xDir;
        Vector3 vertical = transform.forward * zDir;

        Vector3 velocity = (horizontal + vertical).normalized * curSpeed;

        rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);

        if (velocity.magnitude == 0) isWalk = false;
        else isWalk = true;
    }

    private void PlayerRotation()
    {
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 playerRot = new Vector3(0.0f, yRot, 0.0f) * MainCamera.instance.GetComponent<MainCamera>()._sensitivity;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(playerRot));
    }

    private void PlayerRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && weaponController._curRangedWeapon._isReload == false) Running();
        if (Input.GetKeyUp(KeyCode.LeftShift) || weaponController._curRangedWeapon._isReload == true) StopRunning();

        ani.SetBool("IsRunning", isRun);
    }

    private void Running()
    {
        isRun = true;
        curSpeed = runSpeed;
    }

    private void StopRunning()
    {
        isRun = false;
        curSpeed = stat._walkSpeed;
    }

    //점프
    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true) Jump();
    }

    private void Jump()
    {
        rb.velocity = transform.up * jumpForce;
    }

    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }

    //앉기
    private void PlayerCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouch = true;
            Crouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouch = false;
            Crouch();
        }
    }

    private void Crouch()
    {
        if (isCrouch)
        {
            curSpeed = crouchSpeed;
            col.height = colCrouchHeight;
        }
        else
        {
            curSpeed = stat._walkSpeed;
            col.height = colOriginHeight;
        }
    }

    //무기관련
    private void WeaponChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Hands|draw"))
            {
                ani.Play("Hands|draw", -1, 0f);
                ani.ResetTrigger("WeaponChange");
            }
            else ani.SetTrigger("WeaponChange");
            if (IsInvoking("WaitWeaponChange")) CancelInvoke("WaitWeaponChange");
            Invoke("WaitWeaponChange", 1.5f);
            weaponController.WeaponChange();
        }
    }

    private void WaitWeaponChange() { weaponController.FinishWeaponChange(); }

    private void Fire()
    {
        if (Input.GetMouseButton(0) && weaponController.CanFire() == true && isRun == false)
        {
            weaponController.Fire();
            CrossHair.instance.StartFireAnimation();
            ani.SetBool("IsFire", true);
        }
        if (Input.GetMouseButtonUp(0) || weaponController._curRangedWeapon._isReload == true || isRun == true || playerStop == true)
        {
            CrossHair.instance.StopFireAnimation();
            ani.SetBool("IsFire", false);
        }
    }

    private void Reload()
    {
        if ((Input.GetKeyDown(KeyCode.R) || weaponController._curRangedWeapon._curBulletInMagazine <= 0) && weaponController._curRangedWeapon._isReload == false)
            weaponController.Reload();

        ani.SetBool("IsReloading", weaponController._curRangedWeapon._isReload);
    }

    private void Zoom()
    {
        if (Input.GetMouseButtonDown(1) && isRun == false)
            weaponController.Zoom();
        if (Input.GetMouseButtonUp(1) || isRun == true)
            weaponController.StopZoom();
    }

    //크로스헤어
    private void CrossHairState()
    {
        CrossHair.instance.SetCrouchAnimation(isCrouch);
        CrossHair.instance.SetRunAnimation(isRun);
        CrossHair.instance.SetWalkAnimation(isWalk);
    }

    private void ControlMouseSensitivity()
    {
        if (Input.GetKeyDown(KeyCode.RightBracket)) MainCamera.instance.GetComponent<MainCamera>().UpMouseSensitivity();
        if (Input.GetKeyDown(KeyCode.LeftBracket)) MainCamera.instance.GetComponent<MainCamera>().DownMouseSensitivity();
    }

    private void IsTargetInCrossHair()
    {
        CrossHair.instance.IsEnemyLocateCrosshair(weaponController._curRangedWeapon.IsTargerPointOfSight(MainCamera.instance.gameObject, new List<string> { "Enemy", "Dummy", "Boss"}));
    }

    override protected void LifeFiguresCheck()
    {
        if (curLife / stat._maxLife <= 0.2f)
        {
            UiController.instance._playerStateUi.StartConditionState(Condition.Moribund);
        }
        else
        {
            UiController.instance._playerStateUi.EndConditionState(Condition.Moribund);
        }
    }

    override protected void Dead()
    {
        base.Dead();

        StartCoroutine(PlayerDieCameraMotionCoroutine());
    }

    private IEnumerator PlayerDieCameraMotionCoroutine()
    {
        GameController.instance.StopGame();
        arm.SetActive(false);
        deadCam.SetActive(true);
        deadCharacter.SetActive(true);
        deadCam.transform.position = gameObject.transform.position + new Vector3(0.0f, 2.0f, -0.5f);
        deadCam.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(new Vector3(60.0f, 0.0f, 0.0f));

        while (deadCam.transform.position.y < 3.0f)
        {
            deadCam.transform.position += new Vector3(0.0f, 0.002f, 0.0f);
            yield return null;
        }

        UiController.instance._playerDeadUi.PlayerDeadUiOn();
    }
}