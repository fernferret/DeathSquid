using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace DeathSquid
{
	class ShooterProjectileMulti : ShooterProjectileObjectNew
	{
		public ShooterProjectileMulti() :
			base(
			10,
			10,
			0,
			new List<ShooterGunObject>(),
			Color.White,
			1,
			new List<String> { "projectile" },
			new List<String> { "projectile_explosion_1", "projectile_explosion_1", "projectile_explosion_1" },
			new List<String> { "dead_ship" })
		{
			AddGun(new ShooterGunBasic(_xPosition, _yPosition, _xPosition + 1, _yPosition + 1, new ShooterProjectileBasic()));
			AddGun(new ShooterGunBasic(_xPosition, _yPosition, _xPosition + 1, _yPosition - 1, new ShooterProjectileBasic()));
			AddGun(new ShooterGunBasic(_xPosition, _yPosition, _xPosition - 1, _yPosition + 1, new ShooterProjectileBasic()));
			AddGun(new ShooterGunBasic(_xPosition, _yPosition, _xPosition - 1, _yPosition - 1, new ShooterProjectileBasic()));
		}

	}
}