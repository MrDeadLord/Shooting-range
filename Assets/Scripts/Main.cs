using UnityEngine;

namespace DeadLords.Shooter
{
    public class Main : MonoBehaviour
    {
        public static Main Instance { get; private set; }

        private ObjectManager _objManager;

        private void Awake()
        {
            Instance = this;

            _objManager = GetComponent<ObjectManager>();
        }

        public ObjectManager GetObjectManager
        {
            get { return _objManager; }
        }
    }
}