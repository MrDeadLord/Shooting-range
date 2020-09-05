using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadLords.Shooter
{
    public class BaseEnvoirment : MonoBehaviour
    {
        protected bool _isVisible;

        /// <summary>
        /// Скрывает/показывает объект(вкл/выкл рендер объекта). Применяется так же ко всем вложенным объектам
        /// </summary>
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                AskVisible(transform, _isVisible);
            }
        }

        /// <summary>
        /// Меняет видимость объекта
        /// </summary>
        /// <param name="obj">Объект, которому нужно изменить видимость</param>
        /// <param name="value">Значение видимости. Виден ли?</param>
        private void AskVisible(Transform obj, bool value)
        {
            if (obj.GetComponent<MeshRenderer>())
                obj.GetComponent<MeshRenderer>().enabled = _isVisible;
            if (obj.GetComponent<SkinnedMeshRenderer>())
                obj.GetComponent<SkinnedMeshRenderer>().enabled = _isVisible;
            if (obj.childCount > 0)
            {
                foreach (Transform child in obj)
                    AskVisible(child, value);
            }
        }
    }
}