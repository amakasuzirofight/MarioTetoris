namespace Tetris
{
    interface IGetTetrisInfo
    {
        public TetrisScriptableObject GetTetrimino(TetrisTypeEnum type,TetrisAngle angle);
    }
}