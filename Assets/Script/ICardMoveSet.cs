using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ICardMoveSet
{
    List<HexPosition> Positions(HexPosition fromPosition);

    bool Execute(HexPosition hoverPosition, CardView cardView);
}