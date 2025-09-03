using Unity;
using UnityEngine;
using UnityEngine.UI;


namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public enum ControlType
        {
            Keyboard,
            Joystick
        }

        [SerializeField] private float _speed;
        [SerializeField] private Animator _animator;
        private Vector3 _movement;
        [SerializeField] private Joystick Joystick;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Button KeyboardControl;
        [SerializeField] private Button JoystickControl;

        [SerializeField] private ControlType currentControlType;

        private void Start()
        {
            KeyboardControl.onClick.AddListener(EnableKeyboard);
            JoystickControl.onClick.AddListener(EnableJoystick);
        }

        private void Update()
        {
            Animation();
            if (currentControlType == ControlType.Keyboard)
            {
                KeyboardMove();
            }
            else
            {
                JoystickMove();
            }
        }

        public void EnableKeyboard()
        {
            currentControlType = ControlType.Keyboard;
        }

        public void EnableJoystick()
        {
            currentControlType = ControlType.Joystick;
        }

        private void KeyboardMove()
        {
            _movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            transform.position += _movement.normalized * (_speed * Time.deltaTime);
        }

        private void JoystickMove()
        {
            _movement = new Vector3(Joystick.Horizontal, Joystick.Vertical);
            transform.position += _movement.normalized * (_speed * Time.deltaTime);
        }

        private void Animation()
        {
            _animator.SetFloat("Horizontal", _movement.x);
            _animator.SetFloat("Vertical", _movement.y);
            _animator.SetFloat("Speed", _movement.sqrMagnitude);
        }

    }

}

