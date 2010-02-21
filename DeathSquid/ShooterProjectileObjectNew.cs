using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace DeathSquid
{
	class ShooterProjectileObjectNew : ShooterGameObjectNew
	{
		protected ShooterProjectileObjectNew(int width, int height, int duration, List<ShooterGunObject> guns, Color color, int damage, List<String> standardSprites, List<String> deadSprites, List<String> blankSprite) : 
			base(0, 0, width, height, 0, 0, duration, damage, 0, guns, color, standardSprites, deadSprites, standardSprites, blankSprite) {}
	
		public void SetX(float x)
		{

			_xPosition = x;

			foreach(ShooterGunObject g in _guns)
			{
				g.SetX(x);

			}
		}

		public void SetY(float y)
		{

			_yPosition = y;

			foreach (ShooterGunObject g in _guns)
			{
				g.SetY(y);

			}
		}

		public void SetXVelocity(float x)
		{
			this._xVelocity = x;
		}

		public void SetYVelocity(float y)
		{
			this._yVelocity = y;
		}

		public override void Kill()
		{
			if (!_isDying)
			{
				RemoveAllSpritesToDraw();
				AddSpritesToDraw(_deadSprites);
				_isDying = true;

				foreach(ShooterGunObject g in _guns)
				{
					_children.AddRange(g.Shoot());
				}
			}
		}

		public List<ShooterGunObject> CloneGuns()
		{
			List<ShooterGunObject> clones = new List<ShooterGunObject>();

			foreach(ShooterGunObject g in _guns)
			{
				clones.Add(g.Clone());
			}

			return clones;
		}



		//generates a deep clone of the object
		public ShooterProjectileObjectNew Clone()
		{
			return new ShooterProjectileObjectNew(_width, _height, _score, _guns, _color, _damage,  _standardSprites, _deadSprites, _blankSprite );
		}
	
	}
}