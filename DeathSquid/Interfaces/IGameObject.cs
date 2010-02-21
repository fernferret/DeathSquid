namespace DeathSquid.Interfaces
{
	interface IGameObject
	{
		void UpdatePostion(float x, float y);

		void Draw();

		float GetX();

		float GetY();
	}
}