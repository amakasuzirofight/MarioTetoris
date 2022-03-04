using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RobotItem;
namespace TetrisPiece
{
    class TetrisPieceCore : MonoBehaviour, ITetrisPieceHit
    {

        IAddTetrisPiece addTetrisPiece;
        private void Awake()
        {
            Utility.Locator<ITetrisPieceHit>.Bind(this);
        }
        private void Start()
        {
            addTetrisPiece = Utility.Locator<IAddTetrisPiece>.GetT();
        }
        public void TetrisHit(int num)
        {
            addTetrisPiece.AddTetris(num);
        }
    }
}
