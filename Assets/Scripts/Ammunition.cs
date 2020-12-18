using UnityEngine;
using UnityEngine.UI;
using DeadLords.Shooter.Interface;
using DeadLords.Shooter.Controllers;

namespace DeadLords.Shooter
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Ammunition : BaseController
    {
        #region Variables--------------

        public ShooterType shooter { get; set; }

        [SerializeField] float _dieTime = 8;
        [SerializeField] float _mass = 0.1f;

        Image _hitImg;      //Картинка маркера попадания
        Image _hitImgKill;  //Картинка маркера убийства

        //Таймер, определяющий скорострельность        
        protected Timer _timer = new Timer();

        float _currentDamage;
        public float damage { private get; set; }

        #endregion Variables-----------

        private void Start()
        {
            GetComponent<Rigidbody>().mass = _mass;

            Destroy(gameObject, _dieTime);

            _hitImg = Main.Instance.GetObjectManager.Hit;
            _hitImg.enabled = false;

            _hitImgKill = Main.Instance.GetObjectManager.HitKill;
            _hitImgKill.enabled = false;

            _currentDamage = damage;
        }

        private void Update()
        {
            _timer.Update();
            if (_timer.IsEvent())
                base.On();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Bullet") return;

            //Запускаем такую стрельбу только если стреляет игрок
            if (collision.collider.tag == "Enemy" && shooter == ShooterType.player)
            {
                //Маркер попадания
                _hitImg.enabled = true;

                SetDamage(collision.collider.GetComponent<ISetDamage>());

                //Instantiate(_hitEffectHuman, _hit.transform.position, Quaternion.identity); //Эффект попадания
            }
            else if (collision.collider.tag == "Thin surface")
            {
                _currentDamage /= 2;
                //Instantiate(_hitEffectSurface, _hit.transform.position, Quaternion.identity);   //Эффект попадания
            }

            Destroy(gameObject);
        }

        #region Methods

        private void SetDamage(ISetDamage obj)
        {
            if (obj != null)
            {
                obj.ApplyDamage(_currentDamage);
            }
        }

        private void PlaySound(IPlaySound sound, Collider obj)
        {
            if (sound != null)
            {
                sound.PlaySound(obj);
            }
        }

        #endregion Methods
    }
}