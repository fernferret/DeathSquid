namespace DeathSquid
{
	static class EntryPoint
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			using (var game = new DeathSquid())
			{
				game.Run();
			}
		}
	}
}

