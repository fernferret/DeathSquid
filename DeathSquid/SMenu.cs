using System;
using System.Collections.Generic;
using DeathSquid.Interfaces;
using DeathSquid.Utils;

namespace DeathSquid
{
	public class SMenu:IScreen
	{
		private ScreenMenu _menu;
		private readonly List<String> _menuText;
		
		public SMenu()
		{
			_menuText = new List<string> {"Play!", "Options", "About", "Quit"};
			_menu = new ScreenMenu(_menuText,"Death! Squid!");
		}

		public void Update()
		{
			_menu.Update();
			if(_menu.GetSelectedItem() == "Play!")
			{
				DeathSquid.CurrentScreen = new ShooterNew();
			}
		}

		public void Draw()
		{
			DeathSquid.GameSpriteBatch.Begin();
			_menu.Draw();
			DeathSquid.GameSpriteBatch.End();
		}
	}
}
