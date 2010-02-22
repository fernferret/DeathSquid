using System;
using System.Collections.Generic;
using System.Linq;
using DeathSquid.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DeathSquid
{
	abstract class ShooterGameObjectNew : IShooterGameObject
	{
		protected float _xPosition;
		protected float _yPosition;
		protected float _xVelocity;
		protected float _yVelocity;
		protected int _damage;
		protected int _width;
		protected int _height;
		protected int _hitPoints;
		protected int _score;
		protected bool _isDying;
		private bool _isHurt;
		protected List<ShooterGunObject> _guns;
		protected Color _color;
		protected Rectangle _collisionBox;
		protected List<ShooterGameObjectNew> _children;  //objects which are generated when the current object dies

		//animation related
		private float timer = 0f;
		private float interval = 1000f / 7f;
		protected List<String> StandardSprites { get; set; }
		protected List<String> DeadSprites { get; set; }
		protected List<String> PainSprites { get; set; }
		protected List<String> BlankSprite { get; set; }
		protected Queue<String> _spriteQueue;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="xPosition">The X Position</param>
		/// <param name="yPosition">The Y Position</param>
		/// <param name="width">The width of this object</param>
		/// <param name="height">The height of this object</param>
		/// <param name="xVelocity">The X Velocity</param>
		/// <param name="yVelocity">The Y Velocity</param>
		/// <param name="hitPoints">The amount of damage this object can take before it is destroyed</param>
		/// <param name="damage">The amount of damage this unit does upon hitting another one</param>
		/// <param name="score"></param>
		/// <param name="guns"></param>
		/// <param name="color"></param>
		protected ShooterGameObjectNew(float xPosition, float yPosition, int width, int height, float xVelocity, float yVelocity, int hitPoints, int damage, int score, List<ShooterGunObject> guns, Color color)
		{
			_xPosition = xPosition;
			_xVelocity = xVelocity;
			_yVelocity = yVelocity;
			_color = color;
			_guns = guns;
			_score = score;
			_damage = damage;
			_hitPoints = hitPoints;
			_height = height;
			_width = width;
			_yPosition = yPosition;
			_collisionBox = new Rectangle((int)_xPosition, (int)_yPosition, _width - 5, _height - 5);
			_children = new List<ShooterGameObjectNew>();
			_isDying = false;
			_isHurt = false;
			_spriteQueue = new Queue<String>();
			//AddSpritesToDraw(StandardSprites);
		}

		public virtual void UpdatePostion(float x, float y)
		{
			_xPosition += _xVelocity;
			_yPosition += _yVelocity;

			foreach(ShooterGunObject g in _guns)
			{
				g.UpdatePostion(_xVelocity, _yVelocity);
			}

			_collisionBox.Location = new Point((int)_xPosition, (int)_yPosition);

			if(_spriteQueue.Count <= 1)
			{
				AddSpritesToDraw(StandardSprites);
			}
		}

		public void Draw()
		{
			if (!IsDead())
			{
				//why do I need this?
				if (_spriteQueue.Count <= 0)
				{
					AddSpritesToDraw(StandardSprites);
				}

				DeathSquid.GameSpriteBatch.Draw(DeathSquid.GameGraphics[_spriteQueue.Peek()], new Vector2(_xPosition, _yPosition), _color);
			}
		}

		public float GetX()
		{
			return _xPosition;
		}

		public float GetY()
		{
			return _yPosition;
		}

		public List<ShooterGameObjectNew> GetChildren()
		{
			return _children;
		}

		public void AddGun(ShooterGunObject g)
		{
			_guns.Add(g);
		}

		public void SetXVelocity(float x)
		{
			_xVelocity = x;
		}

		public void SetYVelocity(float y)
		{
			_yVelocity = y;
		}

		public void Hurt()
		{
			RemoveAllSpritesToDraw();
			AddSpritesToDraw(PainSprites);
		}

		public virtual void Kill()
		{
			if (!_isDying)
			{
				RemoveAllSpritesToDraw();
				AddSpritesToDraw(DeadSprites);
				_isDying = true;
			}
		}

		public int GetDamage()
		{
			return _damage;
		}

		public List<ShooterGameObjectNew> Shoot()
		{
			List<ShooterGameObjectNew> projectiles = new List<ShooterGameObjectNew>();

			foreach(ShooterGunObject g in _guns)
			{
				projectiles.AddRange(g.Shoot());
			}

			return projectiles;
		}

		public void Animate(GameTime gameTime)
		{
			timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

			if (timer > interval)
			{
				if (_spriteQueue.Count <= 0)
				{
					AddSpritesToDraw(BlankSprite);
				}
				_spriteQueue.Dequeue();
				timer = 0f;
			}
		}

		public void AddSpritesToDraw(List<String> sprites)
		{
			foreach(var i in sprites)
			{
				_spriteQueue.Enqueue(i);
			}
		}

		public void RemoveAllSpritesToDraw()
		{
			int i;
			for (i = 0; i < _spriteQueue.Count; i++)
			{
				_spriteQueue.Dequeue();
			}
		}

		public int Damage(int damage)
		{
			_hitPoints -= damage;

			if(_hitPoints <= 0)
			{
				Kill();
				return _score;
			}
			else
			{
				Hurt();
			}
			return 0;
		}

		public Rectangle GetCollisionBox()
		{
			return _collisionBox;
		}

		public bool IsDying()
		{
			return _isDying;
		}

		public bool IsDead()
		{
			return (IsDying() && !_spriteQueue.Contains(DeadSprites.Last()));
		}

		public float GetWidth()
		{
			return _width;
		}

		public float GetHeight()
		{
			return _height;
		}
	}
}