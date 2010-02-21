using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace DeathSquid.Interfaces
{
	interface IShooterGameObject : IGameObject
	{
		void SetXVelocity(float x);

		void SetYVelocity(float y);

		void Hurt();

		void Kill();

		void Animate(GameTime gameTime);

		void AddSpritesToDraw(List<String> sprites);

		void RemoveAllSpritesToDraw();

		int Damage(int damage);

		Rectangle GetCollisionBox();

		bool IsDying();

		bool IsDead();

		float GetWidth();

		float GetHeight();
	}
}