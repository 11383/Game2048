using System;
namespace GameLib
{
    public partial class Game 
    {
        private AppStorage appStorage = new AppStorage("Game2048");

        private string GameSaveFileName => $"incomplete-{size}x{size}";
        private string GameSaveHighScoreFileName => $"highscore-{size}x{size}";

        //private bool CanLoad() => !appStorage.Exists(GameSaveFileName);

        /// <summary>
        /// Try to load highscore from saved file
        /// </summary>
        private void LoadHighscore() 
        {
            if (appStorage.Exists(GameSaveHighScoreFileName))
            {
                score = new GameScore(0, appStorage.Load<int>(GameSaveHighScoreFileName));
            }
        }

        /// <summary>
        /// Load game data
        /// </summary>
        private bool Load()
        {
            LoadHighscore();

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

        /// <summary>
        /// Save game state and highscore
        /// </summary>
        private void Save()
        {
            var saveObj = new GameSaveLoadStruct(gameBoard.Grid, score.Score);
            appStorage.Save(saveObj, GameSaveFileName);

            appStorage.Save(Highscore, GameSaveHighScoreFileName);
        }

        /// <summary>
        /// Remove saved game
        /// </summary>
        private void SaveRemove()
        {
            appStorage.Clear(GameSaveFileName);
        }
    }
}
