using UnityEngine;

namespace DeadLords.Shooter.Controllers
{
    public abstract class BaseController : MonoBehaviour
    {
        private bool _enabled = false;  //По умолчанию контроллер выключен

        public bool Enabled
        {
            get { return _enabled; }
            private set { _enabled = value; }
        }

        /// <summary>
        /// Включение контроллера
        /// </summary>
        public virtual void On()
        {
            _enabled = true;
        }

        /// <summary>
        /// Выключение контроллера
        /// </summary>
        public virtual void Off()
        {
            _enabled = false;
        }
    }
}