using UnityEngine;

namespace Source.Player
{
    public sealed class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public void Rotate(float horizontal, float vertical)
        {
            if (horizontal == 0 && vertical == 0)
                return;
                
            var moveDirection = new Vector3(horizontal, 0, vertical);

            if (Vector3.Angle(transform.forward, moveDirection) <= 0) 
                return;
                
            var newDirection = Vector3.RotateTowards(transform.forward, moveDirection,
                _speed * Time.deltaTime, 0);
            
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}