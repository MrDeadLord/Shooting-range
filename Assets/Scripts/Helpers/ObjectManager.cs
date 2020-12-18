using DeadLords.Shooter;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private Light _flashlight;
    [SerializeField] private Weapon[] _Weapon;
    [SerializeField] private Ammunition[] _ammo;
    [Space(10)]
    [SerializeField] [Tooltip("Маркер попадания")] private Image hit;
    [SerializeField] [Tooltip("Маркер попадания")] private Image hitHalf;
    [SerializeField] [Tooltip("Маркер убийства")] private Image hitKill;
    [Space(5)]
    [SerializeField] [Tooltip("Эффект попадания по человеку")] private ParticleSystem _humanHitEffect;
    [SerializeField] [Tooltip("Эффект попадания по поверхности")] private ParticleSystem _surfaceHitEffect;

    public Light Flashlight { get { return _flashlight; } }
    public Weapon[] Weapon { get { return _Weapon; } }
    public Ammunition[] Ammo { get { return _ammo; } }
    public Image Hit { get { return hit; } }
    public Image HitHalf { get { return hitHalf; } }
    public Image HitKill { get { return hitKill; } }
    public ParticleSystem HitEffectHuman { get { return _humanHitEffect; } }
    public ParticleSystem HitEffectSurface { get { return _surfaceHitEffect; } }
}
