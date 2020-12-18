using System.Collections;
using UnityEngine;

namespace DeadLords.Shooter
{
    /// <summary>
    /// Базовый клас для всего оружия
    /// </summary>
    [RequireComponent(typeof(Shooting))]
    public class Weapon : BaseEnvoirment
    {
        #region Variables
        [Header("Стрельба")]
        [SerializeField] [Tooltip("Автомат ли это?")] bool _auto = false;
        [SerializeField] [Tooltip("Время между выстрелами. Скорострельность")] private float _fireRate = 0.2f;

        [Space(10)]
        [SerializeField] [Tooltip("Время удара ближнего боя")] private float _meleeTime = 2;
        [SerializeField] [Tooltip("Урон в ближнем бою")] private float _meleeDamage = 50;
        [Space(10)]
        [SerializeField] [Tooltip("Видимость оружия при старте")] private bool _isVisibleOnStart;

        [Space(10)]
        [Header("Перезарядка")]
        [SerializeField] [Tooltip("Време перезарядки")] private float _reloadTime = 2f;
        [SerializeField] [Tooltip("Если при перезарядке есть потроны")] private Renderer bulletSet;     //ДОПИЛИТЬ
        [SerializeField] [Tooltip("Место появления обоймы в мире")] private Transform magInstPlace;
        [SerializeField] [Tooltip("Модель обоймы с патронами")] private GameObject fullMag;
        [SerializeField] [Tooltip("Модель пустой обоймы")] private GameObject emptyMag;

        protected Animator _animator;
        Shooting _shot;
        #endregion Variables

        #region Unity time

        void Start()
        {
            if (_isVisibleOnStart)
            {
                IsVisible = true;
                bulletSet.enabled = false;
            }
            else
            {
                IsVisible = false;
            }

            //_shootEffect = GetComponentInChildren<ParticleSystem>();
            _animator = GetComponentInChildren<Animator>();

            _shot = GetComponent<Shooting>();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Включение анимации после выстрела
        /// </summary>
        public void AfterShoot()
        {
            _animator.SetTrigger("AfterFire");
        }

        /// <summary>
        /// Ближний бой
        /// </summary>
        public void Melee()
        {
            _animator.SetTrigger("Melee");

            GetComponentInChildren<BoxCollider>().enabled = true;
        }

        /// <summary>
        /// Перезарядка. Анимация и разрешение стрельбы
        /// </summary>
        public void Reload()
        {
            //Если обойма полная - ничего не делаем
            if (_shot.AmmoCopacity[0] == _shot.AmmoCopacity[1])
                return;

            //Если обойма не пустая, то рендерим пули в обойме
            if (_shot.AmmoCopacity[0] > 0)
            {
                //bulletSet.enabled = true;
                StartCoroutine(InstMag(fullMag));
            }
            else
                StartCoroutine(InstMag(emptyMag));

            _animator.SetTrigger("Reload"); //Запус аниматора

            _shot.AmmoCopacity[0] = _shot.AmmoCopacity[1];
        }

        /// <summary>
        /// Выбрасываемая обойма
        /// </summary>
        /// <param name="obj">Модель обоймы, что будет сброшена</param>
        /// <returns></returns>
        IEnumerator InstMag(GameObject obj)
        {
            yield return null;
            GameObject newAmmo = Instantiate(obj, magInstPlace.position, Quaternion.identity, null);
            Destroy(newAmmo, 10);
        }
        #endregion Methods

        #region For editor

        /// <summary>
        /// Скрипт стрельбы
        /// </summary>
        public Shooting Shooting
        {
            get { return _shot; }
        }

        /// <summary>
        /// Есть ли автострельба(автомат)
        /// </summary>
        public bool Auto { get { return _auto; } }

        /// <summary>
        /// Время перезарядки
        /// </summary>
        public float ReloadTime { get { return _reloadTime; } }

        /// <summary>
        /// Скорострельность
        /// </summary>
        public float FireRate { get { return _fireRate; } }
        #endregion For editor
    }
}