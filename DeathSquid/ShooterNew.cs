using System;
using System.Collections.Generic;
using DeathSquid.Interfaces;
using DeathSquid.Utils;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DeathSquid
{
	class ShooterNew : IGame, IScreen
	{

		private readonly int _width = DeathSquid.ScreenWidth;
		private readonly int _height = DeathSquid.ScreenHeight;

		private int _lives = 3;
		private int _score = 0;

		private ShooterShipNew _ship;

		private int _currentLevel;

		private List<ShooterLevelNew> _levels;
		private List<ShooterHerdNew> _herdList;
		private List<ShooterGameObjectNew> _shipStuff;


		public ShooterNew()
		{
			_ship = new ShooterShipNew(50, _width/2);

			_shipStuff = new List<ShooterGameObjectNew>();

			_shipStuff.Add(_ship);

			_levels = new List<ShooterLevelNew>();

			_herdList = new List<ShooterHerdNew>();


			//remove later
			StartGame(0);

		}

		public void AdvanceLevel()
		{
			if (_currentLevel <= _levels.Count)
			{
				_currentLevel++;
				_herdList = _levels[_currentLevel].GetHerdList();
				ResetGame();
			}
		}

		public void ResetGame()
		{
			_ship = new ShooterShipNew(50, _width/2);
		}

		public List<PowerUp> GetPowerUps()
		{
			throw new NotImplementedException();
		}

		public void FinishGame()
		{
			//_main.EndGame();
		}

		public void StartGame(int level)
		{
				
			#region 1st level new

			ShooterHerdNew enemies1 = new ShooterHerdNew(0, 0, _width, _height, (float)0.5, 1);
			ShooterLevelNew level1 = new ShooterLevelNew();

			enemies1.AddObject(new ShooterEnemyBasicNew(1, 5));
			enemies1.AddObject(new ShooterEnemyBasicNew(2, 5));
			enemies1.AddObject(new ShooterEnemyBasicNew(3, 5));
			enemies1.AddObject(new ShooterEnemyBasicNew(4, 5));
			enemies1.AddObject(new ShooterEnemyBasicNew(5, 5));
			enemies1.AddObject(new ShooterEnemyBasicNew(6, 5));
			enemies1.AddObject(new ShooterEnemyBasicNew(7, 5));
			enemies1.AddObject(new ShooterEnemyBasicNew(8, 5));
			enemies1.AddObject(new ShooterEnemyBasicNew(9, 5));
			enemies1.AddObject(new ShooterEnemyBasicNew(10, 5));
			enemies1.AddObject(new ShooterEnemyBasicNew(11, 5));
			enemies1.AddObject(new ShooterEnemyBasicNew(12, 5));

			enemies1.AddObject(new ShooterEnemyBasicNew(1, 4));
			enemies1.AddObject(new ShooterEnemyBasicNew(2, 4));
			enemies1.AddObject(new ShooterEnemyBasicNew(3, 4));
			enemies1.AddObject(new ShooterEnemyBasicNew(4, 4));
			enemies1.AddObject(new ShooterEnemyBasicNew(5, 4));
			enemies1.AddObject(new ShooterEnemyBasicNew(6, 4));
			enemies1.AddObject(new ShooterEnemyBasicNew(7, 4));
			enemies1.AddObject(new ShooterEnemyBasicNew(8, 4));
			enemies1.AddObject(new ShooterEnemyBasicNew(9, 4));
			enemies1.AddObject(new ShooterEnemyBasicNew(10, 4));
			enemies1.AddObject(new ShooterEnemyBasicNew(11, 4));
			enemies1.AddObject(new ShooterEnemyBasicNew(12, 4));


			level1.AddHerd(enemies1);

			_levels.Add(level1);
			
				
			#endregion
			_herdList = _levels[0].GetHerdList();


			_herdList = _levels[level].GetHerdList();
		}

		public int GetLevelScore()
		{
			return _score;
		}

		public void ResetScore()
		{
			_score = 0;
		}

		public bool GameWon()
		{
			foreach (ShooterHerdNew l in _herdList)
			{
				if (!l.Empty())
				{
					return false;
				}
			}
			return true;
		}

		public bool GameLost()
		{
			if (_ship.IsDead())
			{
				_lives--;
				_ship = new ShooterShipNew(50, _width/2);
			}

			if (_lives < 0)
			{
				return true;
			}
			return false;
		}

		/*private void AddShipShots(List<ShooterGameObjectNew> shots)
		{
			foreach(ShooterGameObjectNew p in shots)
			{
				_shipShots.Add(p);
			}
		}*/

		/*private void AddEnemyShots(List<ShooterGameObjectNew> shots)
		{
			foreach (ShooterProjectileObjectNew p in shots)
			{
				_enemyShots.Add(p);
			}
		}*/

		private void UpdateCollisions(List<ShooterGameObjectNew> group1, List<ShooterGameObjectNew> group2)
		{
			int damage_o1;
			int damage_o2;

			List<ShooterGameObjectNew> mergeGroup1 = new List<ShooterGameObjectNew>();
			List<ShooterGameObjectNew> mergeGroup2 = new List<ShooterGameObjectNew>();

			foreach (ShooterGameObjectNew o1 in group1)
			{
				o1.UpdatePostion(0, 0);

				foreach(ShooterGameObjectNew o2 in group2)
				{
					if (!o1.IsDying() && !o2.IsDying())
					{
						if (o2.GetCollisionBox().Intersects(o1.GetCollisionBox()))
						{
							damage_o1 = o1.GetDamage();
							damage_o2 = o2.GetDamage();
							o1.Damage(damage_o2);
							o2.Damage(damage_o1);

							o1.SetXVelocity(0);
							o1.SetYVelocity(0);

							o2.SetXVelocity(0);
							o2.SetYVelocity(0);

							if (o1.IsDying())
							{
								mergeGroup1.AddRange(o1.GetChildren());
							}

							if (o2.IsDying())
							{
								mergeGroup2.AddRange(o2.GetChildren());
							}
							//o1.Kill();
						}
					}
				}
			}

			group1.AddRange(mergeGroup1);
			group2.AddRange(mergeGroup2);

		}


		public void Update()
		{
			/*if (SystemMain.SoundBackgroundInstance.State != SoundState.Playing)
			{
				SystemMain.SoundBackgroundInstance.IsLooped = true;
				SystemMain.SoundBackgroundInstance.Volume = .35f;
				SystemMain.SoundBackgroundInstance.Play();
			}*/
			if (GameWon() || GameLost())
			{
				AdvanceLevel();//remove later
				_lives += 3;//remove later
				FinishGame();
			}

			//animation stuff
			foreach (ShooterGameObjectNew s in _shipStuff)
			{
				s.Animate(DeathSquid.CurrentGameTime);
			}

			foreach (ShooterHerdNew h in _herdList)
			{
				h.Animate(DeathSquid.CurrentGameTime);
				h.UpdatePostion(0, 0);
				UpdateCollisions(_shipStuff, h.GetObjects());
				//h.Shoot();

			}


			if (DeathSquid.GetInput.IsButtonPressed(ButtonAction.ShipMoveLeftSlow))
			{
				_ship.SetXVelocity(-7);
			}
			else if (DeathSquid.GetInput.IsButtonPressed(ButtonAction.ShipMoveRightSlow))
			{
				_ship.SetXVelocity(7);
			}
			else
			{
				_ship.SetXVelocity(0);
			}

			_ship.UpdatePostion(0, 0);

			if (DeathSquid.GetInput.IsButtonPressed(ButtonAction.ShipShoot))
			{
				_ship.RemoveAllSpritesToDraw();
				_shipStuff.AddRange(_ship.Shoot());
				_ship.Reload();
				//SystemMain.SoundBackgroundInstance.IsLooped = true;
			}

		}

		public void Draw()
		{
			DeathSquid.GameSpriteBatch.Begin();

			//draw the background
			DeathSquid.GameSpriteBatch.Draw(DeathSquid.GameGraphics["Background"], new Rectangle(0, 0, _width, _height), Color.Black);

			//draw text
			DeathSquid.GameSpriteBatch.DrawString(DeathSquid.SystemFonts["Main"], "Lives: " + _lives, new Vector2(_width - 100, 0), Color.White);
			DeathSquid.GameSpriteBatch.DrawString(DeathSquid.SystemFonts["Main"], "Score: " + _score, new Vector2(0, 0), Color.White);

			foreach (ShooterHerdNew h in _herdList)
			{
				h.Draw();
			}

			foreach (ShooterGameObjectNew s in _shipStuff)
			{
				s.Draw();
			}

			DeathSquid.GameSpriteBatch.End();
		}
	}
}