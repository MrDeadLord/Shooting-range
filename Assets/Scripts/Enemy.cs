using DeadLords.Shooter.Interface;
using System.Linq;
using UnityEngine;

namespace DeadLords
{
    public class Enemy : MonoBehaviour, ISetDamage, IPlaySound
    {
        [SerializeField] float _hp;
        [SerializeField] [Tooltip("Время анимации смерти")] float _dieTime;
        [SerializeField] Collider _head;
        [SerializeField] Collider _torso;
        [SerializeField] Collider[] _limbs;

        [Space(10)]
        [Header("Звуки")]
        [SerializeField] AudioSource _hitHead;
        [SerializeField] AudioSource _hitTorso;
        [SerializeField] AudioSource _hitLimbs;

        private void Update()
        {
            if (_hp <= 0)
                Death();
        }

        void Death()
        {
            Destroy(gameObject, _dieTime);
        }

        public void ApplyDamage(float damage)
        {
            _hp -= damage;
        }

        public void PlaySound(Collider col)
        {
            if (col == _head)
                _hitHead.Play();
            else if (col == _torso)
                _hitTorso.Play();
            else if (_limbs.Contains(col))
                _hitLimbs.Play();
        }
    }
}