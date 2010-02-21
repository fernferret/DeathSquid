using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeathSquid.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace DeathSquid
{
	class ShooterGunObject : IGameObject
	{
		private float _xPosition;
		private float _yPosition;
		private float _range;
		private int _maxProjectiles;
		private float _xAim;
		private float _yAim;
		private float _power;
		private ShooterProjectileObjectNew _projectile;

		public ShooterGunObject(float xPosition, float yPosition, int maxProjectiles, float xAim, float yAim, float power, ShooterProjectileObjectNew projectile)
		{
			_xPosition = xPosition;
			_yAim = yAim;
			_xAim = xAim;
			_power = power;
			_maxProjectiles = maxProjectiles;
			_yPosition = yPosition;
			_projectile = projectile;
		}

		public void UpdatePostion(float x, float y)
		{
			_xPosition += x;
			_yPosition += y;
		}

		public void Draw()
		{
			throw new NotImplementedException();
		}

		public void Load(ShooterProjectileObjectNew p)
		{
			_projectile = p;
		}

		public List<ShooterGameObjectNew> Shoot()
		{
			List<ShooterGameObjectNew> projectiles;
			projectiles = new List<ShooterGameObjectNew>();
			var clone = _projectile.Clone();
			projectiles.Add(clone);
			clone.SetX(_xPosition - _projectile.GetWidth()/2);
			clone.SetY(_yPosition);
			clone.SetXVelocity(GetXVelocity());
			clone.SetYVelocity(GetYVelocity());
			//projectiles.AddRange(clone.Shoot());
			return projectiles;
		}

		private float GetXVelocity()
		{
			float xVelocity;

			if(_yAim == 0)
			{
				if (_xAim < 0)
				{
					return -_power;
				}
				else
				{
					return _power;
				}
			}
			else
			{
				return _xAim*Math.Abs(_xAim/_yAim*_power);
			}
		}

		private float GetYVelocity()
		{
			float yVelocity;

			if (_xAim == 0)
			{
				if (_yAim < 0)
				{
					return _power;
				}
				else
				{
					return -_power;
				}
			}
			else
			{
				return _yAim*Math.Abs(_yAim / _xAim * _power);
			}
		}

		public void SetXAim(float x)
		{
			_xAim = x;
		}

		public void SetYAim(float y)
		{
			_yAim = y;
		}

		public float GetX()
		{
			return _xPosition;
		}

		public float GetY()
		{
			return _yPosition;
		}

		public void SetX(float x)
		{
			_xPosition = x;
		}

		public void SetY(float y)
		{
			_yPosition = y;
		}

		public ShooterGunObject Clone()
		{
			return new ShooterGunObject(_xPosition, _yPosition, _maxProjectiles, _xAim, _yAim, _power, _projectile);
		}
	}
}