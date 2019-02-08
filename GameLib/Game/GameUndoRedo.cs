namespace GameLib
{
    public partial class Game : UndoRedoMechanism<GameSaveLoadStruct>
    {
        private bool canUndo = false;

        public override bool CanRedo()
        {
            return false;
        }

        public override bool CanUndo()
        {
            return base.CanUndo() && canUndo;
        }

        public override void OnUndoRedo(GameSaveLoadStruct state)
        {
            canUndo = false;
            gameBoard = new GameBoard(state.grid, size, baseValue);
            score = new GameScore(state.score, score.Highscore);
        }
    }
}
