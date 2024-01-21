using Sandbox;

public sealed class Manager : Component
{
	public bool Playing { get; private set; } = false;
	[Property] public string LeaderboardName { get; set; } = "highscores";
	public static Manager Instance { get; private set; }
	public long Score { get; private set; } = 0;
	public bool IsPlaying { get; private set; } = false;
	[Property] public SceneFile main { get; set; }
	public Sandbox.Services.Leaderboards.Board Leaderboard;
protected override void OnUpdate()
	{
		StartGame();
	}

	public void StartGame()
	{
		if ( Playing ) return;

		Playing = true;
		Score = 0;

		FetchLeaderboardInfo();
	}
async void FetchLeaderboardInfo()
	{
		Leaderboard = Sandbox.Services.Leaderboards.Get( LeaderboardName );
		Leaderboard.MaxEntries = 10;
		await Leaderboard.Refresh();
		foreach ( var entry in Leaderboard.Entries )
		{
			if ( entry.SteamId == Game.SteamId )
			{
				Score = (long)entry.Value;
			}
		}
	}
	
	public void ScoreSystem()
	{
		Score += 1;
	}
	
	public void EndGame()
	{
		if ( !Playing ) return;
		Playing = false;
		Log.Info(Score);
		
		Sandbox.Services.Stats.SetValue( "highscore", Score );
		
	}
}
