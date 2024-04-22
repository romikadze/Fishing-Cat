using System;
using System.Collections;
using Source.Scripts.Core.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.Fishing
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FishMovement : MonoBehaviour, IPause
    {
        [SerializeField] private float _directionChangeRate;
        [SerializeField] private float _speed;
        private Rigidbody2D _rigidbody;
        private Vector2 _lastDirection;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            StartCoroutine(DirectionChangeTick());
        }

        private IEnumerator DirectionChangeTick()
        {
            while (true)
            {
                ChangeDirection();
                yield return new WaitForSeconds(_directionChangeRate);
            }
        }

        private void ChangeDirection()
        {
            Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            if (Mathf.Abs(_lastDirection.x - direction.x) < 0.3)
                direction.x = -direction.x;
            if (Mathf.Abs(_lastDirection.y - direction.y) < 0.3)
                direction.y = -direction.y;
            _rigidbody.AddForce(direction * _speed, ForceMode2D.Impulse);
            _lastDirection = direction;
        }
        
        #region Pause
        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}