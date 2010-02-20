using System;
using Microsoft.Xna.Framework.Input;

public enum PressType
{
	None, XboxController, Key
}
namespace DeathSquid.Utils
{
	public class ButtonAlias
	{
		private Buttons _button;// = Buttons.BigButton;
		private Keys _key;// = Keys.None;
		private String _association;
		private double _holdable;
		public PressType Pressed;
		public ButtonAlias(Buttons b, double holdable, String a)
		{
			_association = a;
			_button = b;
			//_key;
			_holdable = holdable;
			Pressed = PressType.None;
		}
		public ButtonAlias(Keys k, double holdable, String a)
		{
			_association = a;
			//_button = b;
			_key = k;
			_holdable = holdable;
			Pressed = PressType.None;
		}
		public String GetAssociation()
		{
			return _association;
		}
		public Buttons GetButton()
		{
			return _button;
		}
		public Keys GetKey()
		{
			return _key;
		}

		public bool IsHoldable()
		{
			if(_holdable>-1)
			{
				return true;
			}
			return false;
		}
		public double GetHoldable()
		{
			return _holdable;
		}
	}
}
