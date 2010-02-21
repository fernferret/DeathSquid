using System.Collections.Generic;
using DeathSquid.Utils;

namespace DeathSquid.Interfaces
{
	interface IGame
	{
		void AdvanceLevel();
		void ResetGame();
		List<PowerUp> GetPowerUps();
		void FinishGame();
		void StartGame(int level);
		int GetLevelScore();
		void ResetScore();
	}
}