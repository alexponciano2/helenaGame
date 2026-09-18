using UnityEngine;

namespace HelenaGame
{
    public sealed class CameraFollow2D : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float horizontalSmoothTime = 0.22f;
        [SerializeField] private float minimumX = -7.2f;
        [SerializeField] private float maximumX = 7.2f;

        private float horizontalVelocity;
        private float fixedY;

        public Transform Target
        {
            get => target;
            set => target = value;
        }

        private void Awake()
        {
            fixedY = transform.position.y;
        }

        private void LateUpdate()
        {
            if (target == null) return;
            float desiredX = Mathf.Clamp(target.position.x, minimumX, maximumX);
            float x = Mathf.SmoothDamp(transform.position.x, desiredX, ref horizontalVelocity, horizontalSmoothTime);
            transform.position = new Vector3(x, fixedY, transform.position.z);
        }
    }
}
