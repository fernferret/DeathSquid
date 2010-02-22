using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace DeathSquid
{
	class ShooterShipNew : ShooterGameObjectNew
	{
		private const int Width = 45;
		private const int Height = 45;

		private static readonly ShooterProjectileMulti ShotM = new ShooterProjectileMulti();
		private static readonly ShooterProjectileBasic ShotB = new ShooterProjectileBasic();

		public ShooterShipNew(float xPosition, float yPosition) : 
			base(
			xPosition, 
			yPosition, 
			Width, 
			Height, 
			0, 
			0, 
			20000, 
			20000,
			0, 
			new List<ShooterGunObject>
				{
					//new ShooterGunBasic(xPosition + 2*Width/3, yPosition, 0,1,Shot),
					//new ShooterGunBasic(xPosition + Width/3, yPosition, 0,1,Shot),
					new ShooterGunBasic(xPosition + (float)Width/4, yPosition, 0,1,ShotB),
					new ShooterGunBasic(xPosition + (float)3*Width/4, yPosition,0,1,ShotB)
				},
			Color.White
			)
		{
			StandardSprites = new List<String> { "ship" };
			DeadSprites = DeadSprites = new List<String> { "ship_explode_1", "ship_explode_2", "ship_explode_3", "explosion_1", "explosion_2", "explosion_3", "explosion_4", "explosion_5", "explosion_6", "explosion_7", "explosion_8", "dead_ship" };
			BlankSprite = new List<String> { "dead_ship" };
			PainSprites = new List<String>();
            
			
			
		}

		public void Reload()
		{
			if (!IsDying())
			{
				AddSpritesToDraw(new List<String> { "ship_a" });
			}
		}
	}
}