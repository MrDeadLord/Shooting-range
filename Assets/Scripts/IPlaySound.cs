using UnityEngine;

namespace DeadLords.Shooter.Interface
{
    /// <summary>
    /// Проигрывание звука
    /// </summary>
    public interface IPlaySound
    {
        void PlaySound(Collider collider);
    }
}