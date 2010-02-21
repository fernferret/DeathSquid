using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace DeathSquid
{
	class ShooterShipNew : ShooterGameObjectNew
	{
		private const int Width = 45;
		private const int Height = 45;

		private static readonly ShooterProjectileMulti Shot = new ShooterProjectileMulti();


		private static readonly List<String> StandardSprites = new List<String> { "ship" };
		private static readonly List<String> ShootSprites = new List<String> { "ship_a" };
		private static readonly List<String> DeadSprites = new List<String> { "ship_explode_1", "ship_explode_2", "ship_explode_3", "explosion_1", "explosion_2", "explosion_3", "explosion_4", "explosion_5", "explosion_6", "explosion_7", "explosion_8", "dead_ship" };
		private static readonly List<String> BlankSprites = new List<String> { "dead_ship" };

		public ShooterShipNew(float xPosition, float yPosition) : 
			base(
			xPosition, 
			yPosition, 
			Width, 
			Height, 
			0, 
			0, 
			2, 
			20,
			0, 
			new List<ShooterGunObject>
				{
					new ShooterGunBasic(
						xPosition + 2*Width/3, 
						yPosition, 
						0,
						1,
						Shot),
					new ShooterGunBasic(
						xPosition + Width/3, 
						yPosition, 
						0,
						1,
						Shot),
					new ShooterGunBasic(
						xPosition + Width/4, 
						yPosition, 
						0,
						1,
						Shot),
					new ShooterGunBasic(
						xPosition + 3*Width/4, 
						yPosition, 
						0,
						1,
						Shot)
				},
			Color.White,
			StandardSprites,
			DeadSprites,
			new List<String>{},
			BlankSprites)
		{}

		public void Reload()
		{
			if (!IsDying())
			{
				AddSpritesToDraw(ShootSprites);
			}
		}
	}
}