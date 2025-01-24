using UnityEngine;

namespace Code.Scripts.Characters.Bubble
{
    public class BubbleCam : MonoBehaviour
    {
        public static BubbleCam Instance;
        public Camera cameraObject;
        public BubbleManager player;
        [SerializeField] Transform cameraPivotTransform;

        [Header("Camera Settings")] private float _cameraSmoothSpeed = 300; //kolma akber kol matabta camera bch tousel lel player

        [SerializeField] private float leftAndRightRotationSpeed = 100;
        [SerializeField] private float upAndDownRotationSpeed = 100;
        [SerializeField] private float maximumPivot = 60;
        [SerializeField] private float minimumPivot = -30;
        [SerializeField] private float cameraCollisionRadius = 0.2f;
        [SerializeField] LayerMask colliderLayerMask;

        [Header("Camera Values")] 
        private Vector3 _cameraVelocity;
        private Vector3 _cameraObjectPosition;
        [SerializeField] private float leftAndRightLookAngle;
        [SerializeField] private float upAndDownLookAngle;
        private float _cameraZposition;
        private float _targetCameraZposition;


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            _cameraZposition=cameraObject.transform.localPosition.z;
        }

        public void HandleAllCameraActions()
        {
            if (!player) return;
            FollowPlayer();
            HandleRotations();
            HandleCollisions();
        }

        public void FollowPlayer()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position,
                ref _cameraVelocity, _cameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPosition;
        }

        private void HandleRotations()
        {
            leftAndRightLookAngle += (PlayerInputManager.Instance.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
            upAndDownLookAngle -= (PlayerInputManager.Instance.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
           upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

            Vector3 cameraRotation = Vector3.zero;
            Quaternion targetRotation;

            cameraRotation.y = leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;

            cameraRotation = Vector3.zero;
            cameraRotation.x = upAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            cameraPivotTransform.localRotation = targetRotation;
        }

        private void HandleCollisions()
        {
            _targetCameraZposition = _cameraZposition;
            RaycastHit hit;
            Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
            direction.Normalize();
            
            if(Physics.SphereCast(cameraPivotTransform.position,cameraCollisionRadius,direction,out hit,Mathf.Abs(_targetCameraZposition),colliderLayerMask))
            {
                float distanceFromHitObject=Vector3.Distance(cameraPivotTransform.position, hit.point);
                _targetCameraZposition = -(distanceFromHitObject - cameraCollisionRadius);
            }

            if (Mathf.Abs(_targetCameraZposition) < cameraCollisionRadius)
            {
                _targetCameraZposition = -cameraCollisionRadius;
            }

            _cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, _targetCameraZposition, 0.2f);
            cameraObject.transform.localPosition = _cameraObjectPosition;
        }
    }
}