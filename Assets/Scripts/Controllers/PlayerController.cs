using UnityEngine;

namespace DeadLords.Shooter.Controllers
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        #region Переменные
        [Header("Camera settings")]
        [SerializeField] private float sensitivity = 300; //Чувствительность мыши. Параметр умножения
        [SerializeField] private float maxAngle = 80, minAngle = -80;
        [SerializeField] private Transform _camera;
        private float mouseX, mouseY;
        private float rotX = 0;

        [Header("Movement settings")]
        [SerializeField] private float walkSpeed = 8;
        [SerializeField] private float runSpeed = 12;
        private Vector3 inputForce = Vector3.zero;
        private CharacterController _charContr;
        private bool canMove = true;

        [Header("Jump settings")]
        [SerializeField] private float jumpForce = 2;
        private float gravityForce;
        #endregion

        #region UnityTime
        void Awake()
        {
            //Disabling cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _charContr = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (!canMove)
                return;

            CameraMoving();
            
            if (Input.GetButton("Run"))
                CharacterMove(true);
            else
                CharacterMove(false);

            LocalGravity();
        }
        #endregion

        #region Методы

        private void CameraMoving()
        {
            mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            rotX -= mouseY;
            rotX = Mathf.Clamp(rotX, minAngle, maxAngle);

            _camera.localRotation = Quaternion.Euler(rotX, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }

        private void CharacterMove(bool isRunning)
        {
            if (_charContr.isGrounded)
            {
                float x = Input.GetAxis("Left/Right") * (isRunning ? runSpeed : walkSpeed);
                float z = Input.GetAxis("Forward/Back") * (isRunning ? runSpeed : walkSpeed);

                inputForce = transform.right * x + transform.forward * z;
            }

            inputForce.y = gravityForce;
            _charContr.Move(inputForce * Time.deltaTime);

        }

        private void LocalGravity()
        {
            if (!_charContr.isGrounded) gravityForce -= 20f * Time.deltaTime;
            else gravityForce = -1;

            if (Input.GetButtonDown("Jump") && _charContr.isGrounded)
                gravityForce = jumpForce;
        }

        #endregion
    }
}