using System;
public static class Settings
{
    public static int Width { get; set; }
    public static int Height { get; set; }
    public static int Speed { get; set; }
    public static int Score { get; set; }
    public static int Points { get; set; }
    public static bool GameOver { get; set; }
    public static bool YouWon { get; set; }

    public static void InitializeSettings()
    {
        Width = 20;
        Height = 20;
        Speed = 10;
        Score = 0;
        Points = 100;
        GameOver = false;
        YouWon = false;
    }
}