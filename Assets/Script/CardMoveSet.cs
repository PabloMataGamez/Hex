using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal abstract class CardMoveSet : ICardMoveSet       
{
    private HexBoard _board;
    protected HexBoard HexBoard => _board;

    public CardMoveSet(HexBoard board)
    {
        _board = board;
    }
    public abstract List<HexBoard> Positions(HexPosition fromPosition); //Override

    public virtual bool Execute(HexPieceView pieceView, HexPosition fromPosition, HexPosition toPosition) //Logic here
    {
        _board.Take(pieceView, toPosition); //FROMPOSITION OR TOPOSITION

        return _board.Move(pieceView, fromPosition, toPosition);
    }

    List<HexPosition> ICardMoveSet.Positions(HexPosition fromPosition) //   ?????
    {
        throw new NotImplementedException();
    }
}
