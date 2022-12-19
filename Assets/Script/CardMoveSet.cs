using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class CardMoveSet : ICardMoveSet       
{
    private HexBoard _board;
    private readonly HexEngine _engine;

    protected HexBoard HexBoard => _board;
    protected HexEngine HexEngine => _engine;

    public CardMoveSet(HexBoard board, HexEngine engine)
    {
        _board = board;
        _engine = engine;
    }
    public abstract List<HexPosition> Positions(HexPosition hoverPosition); 

    public abstract bool Execute(HexPosition hoverPosition, CardView cardView);
}
