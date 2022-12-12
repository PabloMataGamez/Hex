using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class CardMoveSet : ICardMoveSet       
{
    private HexBoard _board;
    protected HexBoard HexBoard => _board;

    public CardMoveSet(HexBoard board)
    {
        _board = board;
    }
    public abstract List<HexPosition> Positions(HexPosition fromPosition); //Override ??????

    public virtual bool Execute(HexPosition fromPosition, HexPosition toPosition) //Logic here
    {
        _board.Take(toPosition); 

        return _board.Move(fromPosition, toPosition);
    }

    List<HexPosition> ICardMoveSet.Positions(HexPosition fromPosition) //   ?????
    {
        throw new NotImplementedException();
    }
}
