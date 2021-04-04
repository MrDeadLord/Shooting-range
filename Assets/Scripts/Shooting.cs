using UnityEngine;

namespace DeadLords.Shooter
{
    [RequireComponent(typeof(Weapon))]
    public class Shooting : MonoBehaviour
    {
        #region ========== Variables ========
        [SerializeField] [Tooltip("Стрелок")] ShooterType _shooter;
        [Space(10)]
        [SerializeField] [Tooltip("Точка вылета пули")] private Transform _barrel;
        [SerializeField] [Tooltip("Скорость ускорения пули")] private float _force = 500;        
        [SerializeField] [Tooltip("Кол-во патронов [в обойме, всего]")] private int[] _ammoCopacity = { 30, 30 };
        [SerializeField] [Tooltip("Урон")] private float _damage = 20;

        Weapon _weap;
        Animator _animator;

        #endregion ========== Variables ========

        #region Unity-time
        private void Start()
        {
            _weap = GetComponent<Weapon>();
            _animator = GetComponentInChildren<Animator>();
        }
        #endregion Unity-time

        #region ========== Methods ========

        public void Shoot(Ammunition ammunition)
        {            
            if (ammunition && _ammoCopacity[0] != 0)
            {
                Ammunition bullet = Instantiate(ammunition, _barrel) as Ammunition;

                if (bullet)
                {
                    //_shootEffect.Play();
                    _animator.SetTrigger("Shoot");

                    bullet.GetComponent<Rigidbody>().AddForce(_barrel.transform.forward * _force);
                    bullet.shooter = _shooter;
                    bullet.damage = _damage;

                    _ammoCopacity[0] -= 1;
                }
            }
            else if (_ammoCopacity[0] <= 0)
            {
                _weap.Reload();
            }   //Перезарядка
        }

        #endregion ========== Methods ========

        #region For editor
        /// <summary>
        /// Обойма
        /// </summary>
        public int[] AmmoCopacity { get { return _ammoCopacity; } set { _ammoCopacity = value; } }
        #endregion For editor
    }
}