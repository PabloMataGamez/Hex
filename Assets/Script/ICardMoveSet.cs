using System.Collections.Generic;

public interface ICardMoveSet
{
    List<HexPosition> Positions(HexPosition fromPosition);

    bool Execute(HexPosition hoverPosition, CardView cardView);
}