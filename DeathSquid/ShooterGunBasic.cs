using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeathSquid
{
	class ShooterGunBasic : ShooterGunObject
	{
		public ShooterGunBasic(float xPosition, float yPosition, float xAim, float yAim, ShooterProjectileObjectNew projectile) : base(xPosition, yPosition, 1, xAim, yAim, 7, projectile)
		{
			Load(projectile);
		}

	}
}