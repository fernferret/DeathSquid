using System;
using System.Collections.Generic;
using DeathSquid.Interfaces;
using Microsoft.Xna.Framework;

namespace DeathSquid
{
	class ShooterHerdNew : IGameObject
	{
		private float _xPosition;
		private float _yPosition;
		private float _width;  //when to change directions
		private float _height;  //when to die
		private float _speed;
		private int _shootingFrequency;
		private float _yCounter = 0;
		private float _xVelocity;
		private float _yVelocity;

		private List<ShooterGameObjectNew> _objects;

		Random _rndElement = new Random();

		public ShooterHerdNew(float xPosition, float yPosition, float width, float height, float speed, int shootingFrequency)
		{
			_xPosition = xPosition;
			_yPosition = yPosition;
			_width = width;
			_shootingFrequency = shootingFrequency;
			_speed = speed;
			_height = height;
			_objects = new List<ShooterGameObjectNew>();

			_yCounter = 0;
			_xVelocity = _speed;
			_yVelocity = 0;
		}

		public void AddObject(ShooterGameObjectNew o)
		{
			_objects.Add(o);
			
		}

		public void AddObjects(List<ShooterGameObjectNew> l)
		{
			_objects.AddRange(l);
		}

		public void Shoot()
		{
			List<ShooterGameObjectNew> projectiles = new List<ShooterGameObjectNew>();
			List<ShooterGameObjectNew> objects;
			int rand;
			if (_objects.Count > 0)
			{
				rand = _rndElement.Next(0, 30);
				if (rand < _shootingFrequency)
				{
					var o = _objects[_rndElement.Next(0, _objects.Count)];
					objects = o.Shoot();
					foreach (var p in objects)
					{
						_objects.Add(p);
					}
					
				}
			}
		}

		public void UpdatePostion(float x, float y)
		{

			if (_yCounter > 0)
			{
				_yVelocity = _speed;
				_yCounter -= _yVelocity;
			}
			else
			{
				_yCounter = 0;
				_yVelocity = 0;
			}

			foreach (ShooterGameObjectNew o in _objects)
			{

				if (o.GetX() <= _xPosition)
				{
					_xVelocity = _speed;
					_yCounter = o.GetWidth() - 2;

				}

				if ((o.GetX() + o.GetWidth()) >= _width)
				{
					_xVelocity = -_speed;
					_yCounter = o.GetWidth() - 2;
				}

				if (o.GetY() + o.GetHeight() >= _height && !o.IsDying())  //modify to set yInc to negative or something similar
				{
					o.Kill();
				}
				if (o.GetY() < 0 && !o.IsDying())
				{
					o.Kill();
				}
				if (o.GetX() + o.GetWidth() >= _width && !o.IsDying())
				{
					//o.Kill();
				}
				if(o.GetX() < 0 && !o.IsDying())
				{
					//o.Kill();
				}
			}

			foreach (ShooterGameObjectNew o in _objects)
			{
				o.UpdatePostion(_xVelocity, _yVelocity);
			}
		}

		public void Animate(GameTime gameTime)
		{
			foreach (ShooterGameObjectNew o in _objects)
			{
				o.Animate(gameTime);

				if (o.IsDead())
				{
					_objects.Remove(o);
					break;
				}
			}
		}

		public void Draw()
		{
			foreach (ShooterGameObjectNew o in _objects)
			{
				o.Draw();
			}
		}

		public List<ShooterGameObjectNew> GetObjects()
		{
			return _objects;
		}

		public float GetX()
		{
			return _xPosition;
		}

		public float GetY()
		{
			return _yPosition;
		}

		public List<Rectangle> GetCollisionBoxes()
		{
			List<Rectangle> collisionBoxes = new List<Rectangle>();

			foreach(ShooterGameObjectNew o in _objects)
			{
				collisionBoxes.Add(o.GetCollisionBox());
			}

			return collisionBoxes;
		}

		public bool Empty()
		{
			return (_objects.Count <= 0);
		}
	}
}