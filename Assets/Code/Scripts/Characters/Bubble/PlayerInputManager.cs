using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts.Characters.Bubble
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager Instance;

        public BubbleManager player;

        private PlayerInput _playerInput;

        [SerializeField] public Vector2 movementInput;

        [Header("Character Input")] 
        [SerializeField] public float verticalInput;
        [SerializeField] public float horizontalInput;
        [SerializeField] public float moveAmount;


        [Header("Camera Input")] 
        [SerializeField] public Vector2 cameraInput;
        [SerializeField] public float cameraVerticalInput;
        [SerializeField] public float cameraHorizontalInput;


        [Header("Character Actions")] 
        [SerializeField] private bool interactInput;
        [HideInInspector] public bool dashInput;
        [HideInInspector] public bool jumpInput;


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            SceneManager.activeSceneChanged += OnSceneChange;
            Instance.enabled = true;
        }

        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            if (newScene.name == "level")
                Instance.enabled = true;
            else
                Instance.enabled = false;
        }

        private void OnEnable()
        {
            if (_playerInput == null)
            {
                _playerInput = new PlayerInput();
                _playerInput.Player.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                _playerInput.Camera.CameraMoving.performed += i => cameraInput = i.ReadValue<Vector2>();
                _playerInput.PlayerAction.Dash.performed += i => dashInput = true;
                _playerInput.PlayerAction.Jump.performed += i => jumpInput = true;
            }

            _playerInput.Enable();
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        private void Update()
        {
            HandleAllInputs();
        }

        private void HandleAllInputs()
        {
            HandleMovementInput();
            HandleCameraMovementInput();
        }

        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));
        }

        private void HandleCameraMovementInput()
        {
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;
        }
        
        
    }
}