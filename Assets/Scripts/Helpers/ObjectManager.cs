using DeadLords.Shooter;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private Light _flashlight;
    [SerializeField] private Weapons[] _weapons;
    [SerializeField] private Ammunition[] _ammo;

    public Light Flashlight { get { return _flashlight; } }
    public Weapons[] Weapons { get { return _weapons; } }
    public Ammunition[] Ammo { get { return _ammo; } }
}
