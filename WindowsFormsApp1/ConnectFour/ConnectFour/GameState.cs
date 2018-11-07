namespace ConnectFour
{
    public class GameState
    {
        public int[,] board { get; set; }
        public int turn { get; set; }
        public GameState() { }
        public GameState(int turn, int[,] board)
        {
            this.board = new int[Board.nColumns, Board.nRows];
            this.copyArray(board,this.board);
        }        
        private void copyArray(int[,] source, int[,] destination)
        {
            for (int i = 0; i < (Board.nColumns - 1); i++)
            {
                for (int j = 0; j < (Board.nRows - 1); j++)
                {
                    destination[i, j] = source[i, j];
                }
            }
        }
    }
}