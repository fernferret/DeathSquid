using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DeathSquid.Utils;
using XNASystem.Utils;

namespace DeathSquid.Interfaces
{
	public interface IScreen
	{
		void Update();

		void Draw();
	}
}