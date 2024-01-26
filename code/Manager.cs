using System.Collections.Generic;
using Microsoft.VisualBasic;
using Sandbox;

public sealed class Manager : Component
{
	[Property] public SceneFile sceneFile {get; set;}
	[Property] public UpdateUi updateUi {get; set;}
	public bool Playing { get; private set; } = false;
	public long Score { get; private set; } = 0;
	public long HighScore { get; private set; } = 0;
	[Property] public bool testBool {get; set;}
	[Property] public bool ableToInput { get; set; } = false;
	public bool ShouldAddScore { get; set; } = false;

	public Sandbox.Services.Leaderboards.Board Leaderboard;

	protected override void OnStart()
	{
		StartGame();
		ableToInput = false;
	}

	protected override void OnUpdate()
	{
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
			testBool = true;
			ableToInput = true;
		}
		else
		{
			testBool = false;
		}
		if (testBool)
		{
			//var badguys = GameObject.Tags.Has("badguy");
			
			foreach (var badguy in Scene.GetAllComponents<Shooter>())
			{
				badguy.GameObject.Destroy();
			}
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
		GameManager.ActiveScene.Load(sceneFile);
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
