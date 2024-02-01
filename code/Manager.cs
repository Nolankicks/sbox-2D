using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.VisualBasic;
using Sandbox;
using Sandbox.UI;
using System.Linq;

public sealed class Manager : Component
{
	[Property] public SceneFile menuScene {get; set;}
	public bool Playing { get; private set; } = false;
	public long Score { get; private set; } = 0;
	public long HighScore { get; private set; } = 0;
	[Property] public bool testBool {get; set;}
	
	public bool ShouldAddScore { get; set; } = false;
	[Property] public bool ShouldUpgrade { get; set; } = false;
	[Property] public GameObject newUpgradeUi {get; set;}
	public enum Theme
	{
		
		none, 
		[Icon("grass")]
		grass,
		[Icon("view_in_ar")]
		stone,
		matt
	}
	
	[Property] public Theme theme {get; set;}
	[Property] public Material grasssMaterial {get; set;}
	[Property] public Material stoneMaterial {get; set;}
	[Property] public Material mattMaterial {get; set;}
	public Material overrideMaterial {get; set;}
	[Property] public bool overideColor {get; set;}
	[Property] Color color {get; set;}

	[Property] List<GameObject> platforms {get; set;}
	

	public Sandbox.Services.Leaderboards.Board Leaderboard;

	protected override void OnStart()
	{
		StartGame();
		
		var themeValue = Sandbox.FileSystem.Data.ReadAllText( "player.txt" ).ToInt() - 1;
		theme = (Theme)themeValue;
	}

	protected override void OnUpdate()
	{
		if (ShouldUpgrade)
		{
			newUpgradeUi.Enabled = true;
			foreach (var badguy in Scene.GetAllComponents<Shooter>())
			{
				badguy.GameObject.Destroy();
			}
			foreach (var zombie in Scene.GetAllComponents<Zombie>())
			{
				zombie.GameObject.Destroy();
			}
		}
		else
		{
			newUpgradeUi.Enabled = false;
		}
		

		if (ShouldAddScore)
		{
			AddScore();
			ShouldAddScore = false;
		}


		if (!Playing && Input.Pressed("Jump"))
		{
			StartGame();
		}
		
		if (Score % 100 == 0 && Score != 0)
		{
			ShouldUpgrade = true;
		}
		else
		{
			ShouldUpgrade = false;
		}

		



		if (testBool)
		{
			//var badguys = GameObject.Tags.Has("badguy");
			
			foreach (var badguy in Scene.GetAllComponents<Shooter>())
			{
				badguy.GameObject.Destroy();
			}
			foreach (var zombie in Scene.GetAllComponents<Zombie>())
			{
				zombie.GameObject.Destroy();
			}
		}
		
		foreach (var platfrom in platforms)
		{
			var modelRender = platfrom.Components.Get<ModelRenderer>();
			if (overideColor)
			{
			modelRender.Tint = color;
			}
			//theme selection
			if (theme == Theme.grass)
			{
				overrideMaterial = grasssMaterial;
				modelRender.Tint = Color.White;
			}
			
			if (theme == Theme.stone)
			{
				overrideMaterial = stoneMaterial;
				modelRender.Tint = Color.White;
			}
			modelRender.MaterialOverride = overrideMaterial;

		}
	}

	public void StartGame()
	{
		if ( Playing ) return;


		Playing = true;
		Score = 0;

		FetchLeaderboardInfo();
	}

	public void EndGame()
	{
		if ( !Playing ) return;

		Playing = false;
		Sandbox.Services.Stats.SetValue( "highscore", Score );
		GameManager.ActiveScene.Load(menuScene);
	}

	
	public void AddScore()
	{
		
		var score = 0;
		Score += 5;
		Score += score;
		if ( Score > HighScore ) HighScore = Score;
	}

	async void FetchLeaderboardInfo()
	{
		Leaderboard = Sandbox.Services.Leaderboards.Get( "highscores" );
		Leaderboard.MaxEntries = 10;
		await Leaderboard.Refresh();
		foreach ( var entry in Leaderboard.Entries )
		{
			if ( entry.SteamId == Game.SteamId )
			{
				HighScore = (long)entry.Value;
			}
		}
	}

}
