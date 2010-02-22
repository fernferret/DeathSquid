using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeathSquid
{
	class ShooterEnemyBasicNew : ShooterGameObjectNew
	{
		private const int Width = 50;
		private const int Height = 50;
		private static readonly ShooterProjectileBasic Shot = new ShooterProjectileBasic();

		public ShooterEnemyBasicNew(float xPosition, float yPosition) : 
			base(
			(xPosition * Width) + 2,
			yPosition = (yPosition * Height) + 2,
			Width,
			Height,
			0,
			0,
			20,
			10,
			1,
			new List<ShooterGunObject>
				{
					new ShooterGunBasic(
						xPosition + Width/2 - Shot.GetWidth(), 
						yPosition + Height, 
						0,
						-1,
						Shot)
				}, 
			Color.White
			)
		{
			StandardSprites = new List<String> { "enemy_basic_1", "enemy_basic_2" };
			DeadSprites = new List<String> { "enemy_explode_1", "enemy_explode_2", "enemy_explode_3", "explosion_1", "explosion_2", "explosion_3", "explosion_4", "explosion_5", "explosion_6", "explosion_7", "explosion_8" };
			PainSprites = new List<String> { "shooter_enemy_pain" };
			BlankSprite = new List<String> { "dead_ship" };
		}

		public override void UpdatePostion(float x, float y)
		{
			_xPosition += x;
			_yPosition += y;

			foreach (ShooterGunObject g in _guns)
			{
				g.UpdatePostion(_xPosition, _yPosition);
			}

			_collisionBox.Location = new Point((int)_xPosition, (int)_yPosition);

			if (_spriteQueue.Count <= 1)
			{
				AddSpritesToDraw(StandardSprites);
			}
		}
	}
}