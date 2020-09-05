using DeadLords.Shooter.Interface;
using UnityEngine;

namespace DeadLords.Shooter
{
    public class Bullet : Ammunition
    {
        [SerializeField] private float _dieTime = 10;
        [SerializeField] private float _mass = 0.1f;
        [SerializeField] private float _damage = 20;
        [SerializeField] private ParticleSystem _hitEffect;

        private float _currentDamage;

        protected override void Awake()
        {
            base.Awake();
            Destroy(_gameObj, _dieTime);
            _currentDamage = _damage;
            GetRigidBody.mass = _mass;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Bullet") return;

            if(collision.collider.tag == "Enemy")
            {
                SetDamage(collision.gameObject.GetComponent<ISetDamage>());

                Instantiate(_hitEffect, collision.transform.position, gameObject.transform.rotation);
            }
            
            Destroy(_gameObj);
        }

        private void SetDamage(ISetDamage obj)
        {
            if (obj != null)
            {
                obj.ApplyDamage(_currentDamage);
            }
        }
    }
}