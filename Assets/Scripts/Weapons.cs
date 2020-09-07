using UnityEngine;

namespace DeadLords.Shooter
{
    /// <summary>
    /// Базовый клас для всего оружия
    /// </summary>
    public class Weapons : BaseEnvoirment
    {
        #region Переменные
        [SerializeField] [Tooltip("Точка вылета пули")] private Transform _barrel;

        [Space(10)] [SerializeField] [Tooltip("Скорость ускорения пули")] private float _force = 500;
        [SerializeField] [Tooltip("Время между выстрелами. Скорострельность")] private float _rechargeTime = 0.2f;
        [SerializeField] [Tooltip("Време перезарядки")] private float _changeMagTime = 2f;
        [SerializeField] [Tooltip("Кол-во патронов в обойме")] private int[] _ammoCopacity = { 30, 30 };

        [Space(10)]
        [SerializeField] [Tooltip("Время удара ближнего боя")] private float _meleeTime = 2;
        [SerializeField] [Tooltip("Урон в ближнем бою")] private float _meleeDamage = 50;
        [Space(10)]
        [SerializeField] [Tooltip("Видимость оружия при старте")] private bool _isVisibleOnStart;

        protected ParticleSystem _shootEffect;
        protected Animator _animator;

        protected bool _canFire = true;
        protected Timer _timer = new Timer();   //Таймер, определяющий скорострельность
        #endregion

        #region Unity time

        void Start()
        {
            if (_isVisibleOnStart)
                IsVisible = true;
            else
                IsVisible = false;

            _shootEffect = GetComponentInChildren<ParticleSystem>();
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (_barrel.tag == "Melee") return;

            _timer.Update();
            if (_timer.IsEvent())
                _canFire = true;
        }
        #endregion

        public void Shoot(Ammunition ammunition)
        {
            if (_canFire && ammunition && _ammoCopacity[0] != 0)
            {
                Bullet bullet = Instantiate(ammunition, _barrel.position, _barrel.rotation) as Bullet;

                if (bullet)
                {
                    _shootEffect.Play();
                    _animator.SetTrigger("Shoot");

                    bullet.GetComponent<Rigidbody>().AddForce(_barrel.forward * _force);
                    bullet.name = "Bullet";
                    _ammoCopacity[0] -= 1;
                    _canFire = false;
                    _timer.Start(_rechargeTime);
                }
            }
            else if (_ammoCopacity[0] <= 0)
            {
                Reload();
            }
        }

        public void AfterShoot()
        {
            _animator.SetTrigger("AfterFire");
        }

        public void Melee()
        {
            _animator.SetTrigger("Melee");

            GetComponentInChildren<BoxCollider>().enabled = true;

            _canFire = false;
            _timer.Start(_meleeTime);
        }

        public void Reload()
        {
            _animator.SetTrigger("Reloading");

            _canFire = false;
            _timer.Start(_changeMagTime);
            _ammoCopacity[0] = _ammoCopacity[1];
        }



        #region Для редактора

        /// <summary>
        /// Точка, откуда летит пуля
        /// </summary>
        public Transform GetBarrel
        {
            get { return _barrel; }
        }
        #endregion
    }
}