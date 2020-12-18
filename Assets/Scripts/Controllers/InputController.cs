using UnityEngine;

namespace DeadLords.Shooter.Controllers
{
    /// <summary>
    /// Класс отвечающий за управление(входные данные с клавамыши)
    /// </summary>
    public class InputController : BaseController
    {
        bool _isSelectedWeapon = true;
        int indexWeapon = 0;
        Light _light;
        Weapon[] _gun;
        Ammunition[] _ammo;

        public bool canShoot { get; set; }

        private void Start()
        {
            _light = Main.Instance.GetObjectManager.Flashlight;
            _light.enabled = false;
            _gun = Main.Instance.GetObjectManager.Weapon;
            _ammo = Main.Instance.GetObjectManager.Ammo;

            canShoot = false;

            base.On();
        }

        public void Update()
        {
            if (!Enabled)
                return;

            //Работа фанарика. Вкл/выкл
            if (Input.GetButtonDown("Flashlight"))
                _light.enabled = !_light.enabled;

            //Смена оружия по кнопкам
            WeaponSelect();

            //Стрельба
            if (canShoot)
            {
                if (Input.GetButtonDown("Fire"))
                    _gun[indexWeapon].Shooting.Shoot(_ammo[indexWeapon]);


                if (Input.GetButton("Fire") && _gun[indexWeapon].Auto)
                {
                    _gun[indexWeapon].Shooting.Shoot(_ammo[indexWeapon]);
                    canShoot = false;
                    Invoke("CanFireSwitch", _gun[indexWeapon].FireRate);
                }
            }

            //Перезарядка
            if (Input.GetButtonDown("Reload"))
            {
                _gun[indexWeapon].Reload();
                
                canShoot = false;

                Invoke("CanFireSwitch", _gun[indexWeapon].ReloadTime);
            }                
        }

        public void CanFireSwitch() { canShoot = !canShoot; }

        /// <summary>
        /// Выбор оружия
        /// </summary>
        void WeaponSelect()
        {
            if (Input.GetButtonDown("First weapon"))
            {
                _gun[indexWeapon].IsVisible = false;
                //_ammo[indexWeapon].IsVisible = false;

                _isSelectedWeapon = false;
                indexWeapon = 0;
            }           //Если выбрано первое оружие, предыдущее - исчезает
            else if (Input.GetButtonDown("Secondary weapon"))
            {
                _gun[indexWeapon].IsVisible = false;

                _isSelectedWeapon = false;
                indexWeapon = 1;
            }  //Если выбрано второе - предыдущее тоже исчезнет

            //Выбор оружия колесиком мыши
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                _gun[indexWeapon].IsVisible = false;

                _isSelectedWeapon = false;

                if (indexWeapon == _gun.Length - 1)
                    indexWeapon = 0;
                else
                    indexWeapon++;
            }   //Следующее оружие
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                _gun[indexWeapon].IsVisible = false;

                _isSelectedWeapon = false;

                if (indexWeapon == 0)
                    indexWeapon = _gun.Length - 1;
                else
                    indexWeapon--;
            }   //Предыдущее
            else if (_isSelectedWeapon) return;     //Если оружие выбрано - дальше не идем

            _gun[indexWeapon].IsVisible = true;

            _isSelectedWeapon = true;

            canShoot = true;
        }
    }
}