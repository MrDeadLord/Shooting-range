using UnityEngine;

namespace DeadLords.Shooter
{
    /// <summary>
    /// Класс отвечающий за управление(входные данные с клавамыши)
    /// </summary>
    public class InputController : BaseController
    {
        private bool _isSelectedWeapon = true;
        private int indexWeapon = 0;
        private Light _light;

        private void Start()
        {
            _light = Main.Instance.GetObjManager.Flashlight;
            _light.enabled = false;
        }

        public void Update()
        {
            //Работа фанарика. Вкл/выкл
            if (Input.GetButtonDown("Flashlight"))
            {
                _light.enabled = !_light.enabled;
            }

            //Смена оружия по кнопкам
            if (Input.GetButtonDown("First weapon"))
            {
                Main.Instance.GetWeaponsController.Off
                    (Main.Instance.GetObjManager.Weapons[indexWeapon], Main.Instance.GetObjManager.Ammunitions[indexWeapon]);
                _isSelectedWeapon = false;
                indexWeapon = 0;
            }           //Если выбрано первое оружие, предыдущее - исчезает
            else if (Input.GetButtonDown("Secondary weapon"))
            {
                Main.Instance.GetWeaponsController.Off
                    (Main.Instance.GetObjManager.Weapons[indexWeapon], Main.Instance.GetObjManager.Ammunitions[indexWeapon]);
                _isSelectedWeapon = false;
                indexWeapon = 1;
            }  //Если выбрано второе - предыдущее тоже исчезнет

            //Выбор оружия колесиком мыши
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                Main.Instance.GetWeaponsController.Off
                    (Main.Instance.GetObjManager.Weapons[indexWeapon], Main.Instance.GetObjManager.Ammunitions[indexWeapon]);
                _isSelectedWeapon = false;

                if (indexWeapon == Main.Instance.GetObjManager.Weapons.Length - 1)
                    indexWeapon = 0;
                else
                    indexWeapon++;

            }   //Следующее оружие
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                Main.Instance.GetWeaponsController.Off
                    (Main.Instance.GetObjManager.Weapons[indexWeapon], Main.Instance.GetObjManager.Ammunitions[indexWeapon]);
                _isSelectedWeapon = false;

                if (indexWeapon == 0)
                    indexWeapon = Main.Instance.GetObjManager.Weapons.Length - 1;
                else
                    indexWeapon--;
            }   //Предыдущее
            else if (_isSelectedWeapon) return;     //Если оружие выбрано - дальше не идем

            Main.Instance.GetWeaponsController.On
                    (Main.Instance.GetObjManager.Weapons[indexWeapon], Main.Instance.GetObjManager.Ammunitions[indexWeapon]);
            _isSelectedWeapon = true;
        }
    }
}