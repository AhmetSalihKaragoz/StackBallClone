using UnityEngine;

namespace ShapeScripts
{
    public class ShapeMovement : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;

        private void Update()
        {
            Rotate();
        }
        private void Rotate()
        {
            transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
            //transform.rotation = Quaternion.Euler(Vector3.up * (rotationSpeed * Time.deltaTime));
        }
    }
}
