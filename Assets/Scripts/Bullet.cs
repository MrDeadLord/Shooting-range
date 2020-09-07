using DeadLords.Shooter.Interface;
using UnityEngine;

namespace DeadLords.Shooter
{
    public class Bullet : Ammunition
    {
        [SerializeField] private float _dieTime = 10;
        [SerializeField] private float _mass = 0.1f;
        [SerializeField] private float _damage = 20;
        [SerializeField] private ParticleSystem _humanHitEffect;
        [SerializeField] private ParticleSystem _surfaceHitEffect;

        private float _currentDamage;

        private void Awake()
        {
            Destroy(gameObject, _dieTime);
            _currentDamage = _damage;
            GetComponent<Rigidbody>().mass = _mass;
        }

        private void Update()
        {
            if (_currentDamage > _damage / 2)
                Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Bullet") return;

            if(collision.collider.tag == "Enemy")
            {
                SetDamage(collision.gameObject.GetComponent<ISetDamage>());

                Instantiate(_humanHitEffect, collision.transform.position, gameObject.transform.rotation);

                Destroy(gameObject);
            }
            else if(collision.collider.tag == "thin surface")
            {
                _currentDamage /= 2;

                Instantiate(_surfaceHitEffect, collision.transform.position, gameObject.transform.rotation);
            }
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