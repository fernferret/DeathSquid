using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace DeathSquid
{
	class ShooterProjectileBasic : ShooterProjectileObjectNew
	{
		public ShooterProjectileBasic() :
			base(
			10, 
			10, 
			0, 
			new List<ShooterGunObject> (), 
			Color.White, 
			1,
			new List<String> {"projectile"},
			new List<String> { "projectile_explosion_1", "projectile_explosion_2", "projectile_explosion_3" }, 
			new List<String> {"dead_ship"})
		{
		}
	}
}