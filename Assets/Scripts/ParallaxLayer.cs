using UnityEngine;

namespace HelenaGame
{
    [ExecuteAlways]
    public sealed class ParallaxLayer : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float movementFactor = 0.5f;
        [SerializeField] private Transform cameraTransform;

        private Vector3 layerStartPosition;
        private Vector3 cameraStartPosition;

        public float MovementFactor
        {
            get => movementFactor;
            set => movementFactor = Mathf.Clamp01(value);
        }

        public Transform CameraTransform
        {
            get => cameraTransform;
            set => cameraTransform = value;
        }

        private void OnEnable()
        {
            if (cameraTransform == null && Camera.main != null)
            {
                cameraTransform = Camera.main.transform;
            }

            layerStartPosition = transform.position;
            cameraStartPosition = cameraTransform != null ? cameraTransform.position : Vector3.zero;
        }

        private void LateUpdate()
        {
            if (cameraTransform == null)
            {
                if (Camera.main == null) return;
                cameraTransform = Camera.main.transform;
                cameraStartPosition = cameraTransform.position;
                layerStartPosition = transform.position;
            }

            Vector3 cameraDelta = cameraTransform.position - cameraStartPosition;
            transform.position = new Vector3(
                layerStartPosition.x + cameraDelta.x * movementFactor,
                layerStartPosition.y + cameraDelta.y * movementFactor,
                layerStartPosition.z);
        }
    }
}
