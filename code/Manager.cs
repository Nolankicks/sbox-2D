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
	public bool ShouldAddScore { get; set; } = false;
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
		Game.ActiveScene.Load(menuScene);
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
