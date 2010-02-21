using System;
using System.Collections.Generic;
using DeathSquid.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Utils;
using DeathSquid.Interfaces;

namespace DeathSquid
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class DeathSquid : Game
	{
		GraphicsDeviceManager _graphics;
		public static SpriteBatch GameSpriteBatch;
		public static Dictionary<String, SpriteFont> SystemFonts;
		public static Dictionary<String, Texture2D> GameGraphics;
		public static InputHandler GetInput = new InputHandler();
		public static DrawHelper Drawing;
		public static int ScreenWidth { get; set; }
		public static int ScreenHeight { get; set; }
		public static GameTime CurrentGameTime;
		SMenu _menu;
		public static IScreen CurrentScreen;

		public DeathSquid()
		{
			CurrentGameTime = new GameTime();
			_graphics = new GraphicsDeviceManager(this){PreferredBackBufferWidth = 1280, PreferredBackBufferHeight = 720};
			ScreenWidth = 1280;
			ScreenHeight = 720;
			Content.RootDirectory = "Content";
			_menu = new SMenu();
			SystemFonts = new Dictionary<string, SpriteFont>();
			
			GameGraphics = new Dictionary<string, Texture2D>();
			//GameGraphics.Add("Ship", Content.Load<Texture2D>("Graphics//Game//Ship"));
		}

		

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			GameSpriteBatch = new SpriteBatch(GraphicsDevice);
			Drawing = new DrawHelper(GameSpriteBatch);
			SystemFonts.Add("Title", Content.Load<SpriteFont>("Fonts//Title"));
			SystemFonts.Add("Main", Content.Load<SpriteFont>("Fonts//Main"));

			GameGraphics.Add("Hilight_left", Content.Load<Texture2D>("Graphics//UI//Hilight_left"));
			GameGraphics.Add("Hilight_center", Content.Load<Texture2D>("Graphics//UI//Hilight_center"));
			GameGraphics.Add("Hilight_right", Content.Load<Texture2D>("Graphics//UI//Hilight_right"));

			GameGraphics.Add("Background", Content.Load<Texture2D>("Graphics//UI//xnaGamesBackground_1280_720"));

			GameGraphics.Add("ship", Content.Load<Texture2D>("Graphics//Game//ship"));//8
			GameGraphics.Add("ship_a", Content.Load<Texture2D>("Graphics//Game//ship_alternate"));//9

			GameGraphics.Add("ship_explode_1", Content.Load<Texture2D>("Graphics//Game//beginexplosion_ship_1"));//10
			GameGraphics.Add("ship_explode_2", Content.Load<Texture2D>("Graphics//Game//beginexplosion_ship_2"));//11
			GameGraphics.Add("ship_explode_3", Content.Load<Texture2D>("Graphics//Game//beginexplosion_ship_3"));//12

			GameGraphics.Add("enemy_basic_1", Content.Load<Texture2D>("Graphics//Game//shooterenemybasic"));//13
			GameGraphics.Add("enemy_basic_2", Content.Load<Texture2D>("Graphics//Game//shooterenemybasic_alternate"));//14

			GameGraphics.Add("enemy_explode_1", Content.Load<Texture2D>("Graphics//Game//beginexplosion_1b"));//15
			GameGraphics.Add("enemy_explode_2", Content.Load<Texture2D>("Graphics//Game//beginexplosion_2b"));//16
			GameGraphics.Add("enemy_explode_3", Content.Load<Texture2D>("Graphics//Game//beginexplosion_3b"));//17

			GameGraphics.Add("explosion_1", Content.Load<Texture2D>("Graphics//Game//explosion_1"));//18
			GameGraphics.Add("explosion_2", Content.Load<Texture2D>("Graphics//Game//explosion_2"));//19
			GameGraphics.Add("explosion_3", Content.Load<Texture2D>("Graphics//Game//explosion_3"));//20
			GameGraphics.Add("explosion_4", Content.Load<Texture2D>("Graphics//Game//explosion_4"));//21
			GameGraphics.Add("explosion_5", Content.Load<Texture2D>("Graphics//Game//explosion_5"));//22

			GameGraphics.Add("dead_ship", Content.Load<Texture2D>("Graphics//Game//dead_ship"));//23
			GameGraphics.Add("projectile", Content.Load<Texture2D>("Graphics//Game//projectile"));//24
			GameGraphics.Add("shooter_enemy_pain", Content.Load<Texture2D>("Graphics//Game//shooterenemybasic_pain"));//25

			GameGraphics.Add("shooterboss", Content.Load<Texture2D>("Graphics//Game//shooterboss"));//26
			GameGraphics.Add("shooterboss_alt", Content.Load<Texture2D>("Graphics//Game//shooterboss_alternative"));//27
			GameGraphics.Add("shooterboss_pain", Content.Load<Texture2D>("Graphics//Game//shooterboss_pain"));//28
			GameGraphics.Add("explosion_6", Content.Load<Texture2D>("Graphics//Game//explosion_6"));//29
			GameGraphics.Add("explosion_7", Content.Load<Texture2D>("Graphics//Game//explosion_7"));//30
			GameGraphics.Add("explosion_8", Content.Load<Texture2D>("Graphics//Game//explosion_8"));//31
			GameGraphics.Add("ShooterBoss_explosion_1_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_1_a"));//32
			//GameGraphics.Add("ShooterBoss_explosion_2_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_2_a"));//33
			GameGraphics.Add("ShooterBoss_explosion_3_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_3_a"));//34
			GameGraphics.Add("ShooterBoss_explosion_4_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_4_a"));//35
			GameGraphics.Add("ShooterBoss_explosion_5_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_5_a"));//36
			GameGraphics.Add("ShooterBoss_explosion_6_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_6_a"));//37
			GameGraphics.Add("ShooterBoss_explosion_7_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_7_a"));//38
			GameGraphics.Add("ShooterBoss_explosion_8_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_8_a"));//39
			GameGraphics.Add("ShooterBoss_explosion_9_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_9_a"));//40
			GameGraphics.Add("ShooterBoss_explosion_10_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_10_a"));//41
			GameGraphics.Add("ShooterBoss_explosion_11_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_11_a"));//42
			GameGraphics.Add("ShooterBoss_explosion_12_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_12_a"));//43
			GameGraphics.Add("ShooterBoss_explosion_13_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_13_a"));//44
			GameGraphics.Add("ShooterBoss_explosion_14_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_14_a"));//45
			GameGraphics.Add("ShooterBoss_explosion_15_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_15_a"));//46
			GameGraphics.Add("ShooterBoss_explosion_16_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_16_a"));//47
			GameGraphics.Add("ShooterBoss_explosion_17_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_17_a"));//48
			GameGraphics.Add("ShooterBoss_explosion_18_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_18_a"));//49
			GameGraphics.Add("ShooterBoss_explosion_19_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_19_a"));//50
			GameGraphics.Add("ShooterBoss_explosion_20_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_20_a"));//51
			GameGraphics.Add("ShooterBoss_explosion_21_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_21_a"));//52
			GameGraphics.Add("ShooterBoss_explosion_22_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_22_a"));//53
			GameGraphics.Add("ShooterBoss_explosion_23_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_23_a"));//54
			GameGraphics.Add("ShooterBoss_explosion_24_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_24_a"));//55
			GameGraphics.Add("ShooterBoss_explosion_25_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_25_a"));//56
			GameGraphics.Add("ShooterBoss_explosion_26_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_26_a"));//57
			GameGraphics.Add("ShooterBoss_explosion_27_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_27_a"));//58
			GameGraphics.Add("ShooterBoss_explosion_28_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_28_a"));//59
			GameGraphics.Add("ShooterBoss_explosion_29_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_29_a"));//60
			GameGraphics.Add("ShooterBoss_explosion_30_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_30_a"));//61
			GameGraphics.Add("ShooterBoss_explosion_31_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_31_a"));//62
			GameGraphics.Add("ShooterBoss_explosion_32_a", Content.Load<Texture2D>("Graphics//Game//ShooterBoss_explosion_32_a"));//63

			GameGraphics.Add("projectile_explosion_1", Content.Load<Texture2D>("Graphics//Game//projectile_explosion_1"));//62
			GameGraphics.Add("projectile_explosion_2", Content.Load<Texture2D>("Graphics//Game//projectile_explosion_2"));//63
			GameGraphics.Add("projectile_explosion_3", Content.Load<Texture2D>("Graphics//Game//projectile_explosion_3"));//62
			CurrentScreen = _menu;
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			CurrentGameTime = gameTime;
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				Exit();
			GetInput.SetInputs(Keyboard.GetState(),GamePad.GetState(PlayerIndex.One));
			//_menu.Update();
			CurrentScreen.Update();
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			CurrentScreen.Draw();
			//_menu.Draw();
			base.Draw(gameTime);
		}
	}
}
