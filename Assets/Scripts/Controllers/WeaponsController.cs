using UnityEngine;

namespace DeadLords.Shooter.Controllers
{
    public class WeaponsController : BaseController
    {
        private Weapons _weapon;
        private Ammunition _ammunition;

        #region Unity timeLine
        void Update()
        {
            if (!Enabled) return;

            if (Input.GetButton("Fire1") && _weapon && _ammunition)
                _weapon.Shoot(_ammunition);
            else if (Input.GetButtonDown("Melee"))
                _weapon.Melee();
            else if (Input.GetButtonDown("Reload") && _weapon.IsVisible)
                _weapon.Reload();

            if (Input.GetButtonUp("Fire1"))
                _weapon.AfterShoot();
        }
        #endregion

        public virtual void On(Weapons weapon, Ammunition ammunition)
        {
            if (Enabled) return;

            base.On();
            _weapon = weapon;
            _ammunition = ammunition;
            weapon.IsVisible = true;
        }

        public virtual void Off(Weapons weapon, Ammunition ammunition)
        {
            if (!Enabled) return;
            base.Off();
            _weapon = null;
            _ammunition = null;
            weapon.IsVisible = false;
        }
    }
}