﻿using System;
using System.Collections.Generic;
﻿using DeathSquid.Utils;
﻿using Microsoft.Xna.Framework.Input;
public enum ButtonPressed
{
	Before, After
}
public enum ButtonAction
{
	MenuAccept, MenuCancel, MenuUp, MenuDown
}
namespace XNASystem.Utils
{
	public class InputHandler
	{
		private KeyboardState _keyState;
		private GamePadState _gamePadState;
		private static Dictionary<ButtonAction, List<ButtonAlias>> _superButton;
		private static Dictionary<String, double> _holdTimes;
		private readonly Dictionary<String, ButtonAlias> _buttonLocks;
		public InputHandler()
		{
			_buttonLocks = new Dictionary<string, ButtonAlias>();
			_holdTimes = new Dictionary<string, double>();
			_superButton = new Dictionary<ButtonAction, List<ButtonAlias>>();
			_superButton.Add(ButtonAction.MenuUp, new List<ButtonAlias>
			                     	{
			                     		new ButtonAlias(Buttons.DPadUp, -1,"MenuUp"),
										new ButtonAlias(Buttons.LeftThumbstickUp, -1,"MenuUp"),
										new ButtonAlias(Keys.NumPad8,-1,"MenuUp"),
										new ButtonAlias(Keys.W,-1,"MenuUp"),
										new ButtonAlias(Keys.Up,2,"MenuUp")
			                     	});
			_superButton.Add(ButtonAction.MenuDown, new List<ButtonAlias>
			                     	{
			                     		new ButtonAlias(Buttons.DPadDown, -1,"MenuDown"),
										new ButtonAlias(Buttons.LeftThumbstickDown, -1,"MenuDown"),
										new ButtonAlias(Keys.Down,1,"MenuDown"),
										new ButtonAlias(Keys.NumPad2,-1,"MenuDown"),
										new ButtonAlias(Keys.S,-1,"MenuDown"),
										new ButtonAlias(Keys.W,-1,"MenuDown")
			                     	});
			_superButton.Add(ButtonAction.MenuAccept, new List<ButtonAlias>
			                     	{
			                     		new ButtonAlias(Buttons.A, -1,"MenuAccept"),
										new ButtonAlias(Buttons.Start, -1,"MenuAccept"),
										new ButtonAlias(Keys.Enter, -1,"MenuAccept")
			                     	});
			_superButton.Add(ButtonAction.MenuCancel, new List<ButtonAlias>
			                     	{
			                     		new ButtonAlias(Buttons.B, -1,"MenuCancel"),
										new ButtonAlias(Buttons.Back, -1,"MenuCancel"),
										new ButtonAlias(Keys.Delete, -1,"MenuCancel")
			                     	});
		}

		public bool IsButtonPressed(ButtonAction b)
		{
			// If our epic superbutton Dictionary contains the action provided
			// ProTip: If you're doing it right, it will ;) aka It always will if you've defined everything correctly
			if(_superButton.ContainsKey(b))
			{
				// Check each ButtonAlias within our specific _superButton
				foreach (var button in _superButton[b])
				{
					// Handle GamePad buttons first, if and only if the gampead is plugged in!
					if(!button.GetButton().Equals(null) && _gamePadState.IsConnected)
					{
						// If you don't have a controller plugged in, you shouldent see this...

						// This _superButton Command Has a Key Associated with it, 
						// Is it DOWN?
						// Does this command NOT already have an associated press?
						if (_gamePadState.IsButtonDown(button.GetButton()) && !_buttonLocks.ContainsKey(button.GetAssociation()))
						{
							//NO! Sweet, let's set the association.  This is the button that will be counted as pressed
							// UNTIL IT DIES (is unpressed)
							button.Pressed = PressType.XboxController;
							//_pressedButtons.Add(button.GetAssociation());
							_buttonLocks.Add(button.GetAssociation(), button);
							return true;
							// Return True, since the button is being held

						}
						// Only perform this check if the current button is held, and if it owns the lock!
						if (_gamePadState.IsButtonDown(button.GetButton()) && _buttonLocks.ContainsValue(button))
						{
							//We already have an association, are we holdable and still held?
							if (button.IsHoldable())
							{
								return true;
							}
							// Otherwise, tell the system to only do it once!

							// REMEMBER BUTTON IS STILL HELD DOWN HERE
							return false;
							// We return false here, because we don't want to register a continuous press for
							// non-held buttons!  Their true happened up above!

						}
						// This _superButton Command Has a Key Associated with it
						// Is it UP?
						// Does this command already have an associated press?
						//if (_keyState.IsKeyUp(button.GetKey()) && _pressedButtons.Contains(button.GetAssociation()))
						if (_gamePadState.IsButtonUp(button.GetButton()) && _buttonLocks.ContainsValue(button))
						{
							//_pressedButtons.Remove(button.GetAssociation());
							_buttonLocks.Remove(button.GetAssociation());
							// We already have an association
							// Which has just been removed (key finally pulled up, remove association)
							// Were we holdable?
							// Well I guess it doesnt matter... The association... HAS BEEN REVOKED!
							return false;
						}
					}
					// If the key is SOMETHING aka, not a button
					if (!button.GetKey().Equals(Keys.None))
					{
						// This _superButton Command Has a Key Associated with it, 
						// Is it DOWN?
						// Does this command NOT already have an associated press?
						//if (_keyState.IsKeyDown(button.GetKey()) && !_pressedButtons.Contains(button.GetAssociation()))
						// OR
						if(_keyState.IsKeyDown(button.GetKey()) && !_buttonLocks.ContainsKey(button.GetAssociation()))
						{
							//NO! Sweet, let's set the association.  This is the key that will be counted as pressed
							// UNTIL IT DIES (is unpressed)
							button.Pressed = PressType.Key;
							//_pressedButtons.Add(button.GetAssociation());
							_buttonLocks.Add(button.GetAssociation(),button);
							return true;
							// Return True, since the button is being held
							//return false;

						}
						//if (_keyState.IsKeyDown(button.GetKey()) && _pressedButtons.Contains(button.GetAssociation()))
						if (_keyState.IsKeyDown(button.GetKey()) && _buttonLocks.ContainsValue(button))
						{
							//We already have an association, are we holdable and still held?
							if (button.IsHoldable())
							{
								if(button.GetHoldable() == 0)
								{
									return true;
								}
								// If we have a holdable button whos delay is greater than 0 seconds, and we have NO association, create one and return
								if(button.GetHoldable() > 0 && !_holdTimes.ContainsKey(button.GetAssociation()))
								{
									_holdTimes.Add(button.GetAssociation(),DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds+button.GetHoldable());
									return false;
								}
								if(button.GetHoldable() > 0  && _holdTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) < 0)
								{
									// Remove, re-add, rinse, repeat!
									_holdTimes.Remove(button.GetAssociation());
									return true;
								}
								
								
							}
							// Otherwise, tell the system to only do it once!

							// REMEMBER BUTTON IS STILL HELD DOWN HERE
							return false;
							
						}
						// This _superButton Command Has a Key Associated with it
						// Is it UP?
						// Does this command already have an associated press?
						//if (_keyState.IsKeyUp(button.GetKey()) && _pressedButtons.Contains(button.GetAssociation()))
						if (_keyState.IsKeyUp(button.GetKey()) && _buttonLocks.ContainsValue(button))
						{
							//_pressedButtons.Remove(button.GetAssociation());
							_buttonLocks.Remove(button.GetAssociation());
							//if (button.GetHoldable() > 0 && _holdTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) > 0)
							//{
								//DrawHelper.Debug = _holdTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) + ",(" + _holdTimes[button.GetAssociation()] + ", " + button.GetHoldable()+")";
							//	return true;
							//}
							if (_holdTimes.ContainsKey(button.GetAssociation()) && button.GetHoldable() > 0 && _holdTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) < 0)
							{
								DrawHelper.Debug = "FINSHES";
								//DrawHelper.Debug = _holdTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) + ",(" + _holdTimes[button.GetAssociation()] + ", " + button.GetHoldable() + ")";
								_holdTimes.Remove(button.GetAssociation());
							}
							// We already have an association
							// Which has just been removed (key finally pulled up, remove association)
							// Were we holdable?
							// Well I guess it doesnt matter... The association... HAS BEEN REVOKED!
							return false;
						}
					}
				}
				return false;
			}
			return false;
		}
		internal void SetInputs(KeyboardState keyState, GamePadState padState)
		{
			_keyState = keyState;
			_gamePadState = padState;
		}
	}
}
