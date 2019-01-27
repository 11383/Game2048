using System;
namespace GameLib
{
    public partial class Game 
    {
        private AppStorage appStorage = new AppStorage("Game2048");

        private string GameSaveFileName => $"incomplete-{size}x{size}";
        private string GameSaveHighScoreFileName => $"highscore-{size}x{size}";

        //private bool CanLoad() => !appStorage.Exists(GameSaveFileName);

        private void LoadHighscore() 
        {
            // try to load highscore from saved file
            if (appStorage.Exists(GameSaveHighScoreFileName))
            {
                score = new GameScore(0, appStorage.Load<int>(GameSaveHighScoreFileName));
            }
        }

        private bool Load()
        {
            LoadHighscore();

            //load gameData
            if (!appStorage.Exists(GameSaveFileName))
            {
                return false;
            }

            var loadedObj = appStorage.Load<GameSaveLoadStruct>(GameSaveFileName);
            gameBoard = new GameBoard(loadedObj.grid, size, baseValue);

            //clear score && set value to loaded from file
            score.Clear();
            score.Add(loadedObj.score);

            IsPlaying = true;

            return true;
        }

        private void Save()
        {
            // save game state
            var saveObj = new GameSaveLoadStruct(gameBoard.Grid, score.Score);
            appStorage.Save(saveObj, GameSaveFileName);

            // save highscore
            appStorage.Save(Highscore, GameSaveHighScoreFileName);
        }

        private void SaveRemove()
        {
            appStorage.Clear(GameSaveFileName);
        }
    }
}
