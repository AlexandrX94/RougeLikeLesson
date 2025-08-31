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
        public Vector3 Movement => _movement;
        public Joystick Joystick;
        public Rigidbody2D rb;
        public Button KeyboardControl;
        public Button JoystickControl;

        public ControlType currentControlType;

        private void Start()
        {
            
            KeyboardControl.onClick.AddListener(KeyboardMove);
            JoystickControl.onClick.AddListener(JoystickMove);

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

       

        public void KeyboardMove()
        {
            currentControlType = ControlType.Keyboard;
            _movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            transform.position += _movement.normalized * (_speed * Time.deltaTime);
        }

        public void JoystickMove()
        {
            currentControlType = ControlType.Joystick;
            _movement = new Vector3(Joystick.Horizontal, Joystick.Vertical) * _speed;
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

