using System;
using System.Collections.Generic;

public class PieceMovedEventArgs : EventArgs
{
    public HexPieceView HexPiece { get; } 
    
    public HexPosition FromPostion { get; }

    public HexPosition ToPosition { get; }

    public PieceMovedEventArgs(HexPieceView hexPieceView, HexPosition fromPostion, HexPosition toPosition)
    {
        HexPiece = hexPieceView;
        FromPostion = fromPostion;
        ToPosition = toPosition;
    }
}

public class PieceTakenEventArgs : EventArgs
{
    public HexPieceView HexPiece { get; }
    public HexPosition FromPostion { get; }

    public PieceTakenEventArgs(HexPieceView hexPieceView, HexPosition fromPostion)
    {
        HexPiece = hexPieceView;
        FromPostion = fromPostion;
    }
}

public class CardDroppedEventArgs : EventArgs
{
    public HexPieceView HexPiece { get; }

    public HexPosition ToPosition { get; }

    public CardDroppedEventArgs(HexPieceView hexPieceView, HexPosition toPosition)
    {
        HexPiece = hexPieceView;
        ToPosition = toPosition;
    }
}

public class HexBoard
{
    public event EventHandler<PieceMovedEventArgs> PieceMoved;
    public event EventHandler<PieceTakenEventArgs> PieceTaken;
    public event EventHandler<CardDroppedEventArgs> PiecePlaced;

    private Dictionary<HexPosition, HexPieceView> _pieces = new Dictionary<HexPosition, HexPieceView>();
    private int _range = 3;
   
    public HexBoard( )
    {
        
    }
    //With the position we return a piece and if it is there
    public bool TryGetPieceAt(HexPosition position, out HexPieceView piece) 
    {
        return _pieces.TryGetValue(position, out piece);
    }

    public bool IsValid(HexPosition position) //Hex Range = 3
    {
        return (-_range <= position.Q && position.Q <= _range) 
            && (Math.Max(-_range, - position.Q -_range) <= position.R && position.R <= Math.Min(_range, -position.Q + _range));          
    }

    public bool Place(HexPosition position, HexPieceView piece) 
    {
        if (piece == null)
            return false;

        if (!IsValid(position))
            return false;

        if (_pieces.ContainsKey(position))
            return false;

        if (_pieces.ContainsValue(piece))
            return false;

        _pieces[position] = piece;

        OnPiecePlaced(new CardDroppedEventArgs(piece, position)); 

        return true;
    }

    public bool Move( HexPosition fromPosition, HexPosition toPosition)
    {
        if (!IsValid(toPosition))
            return false;

        if (_pieces.ContainsKey(toPosition))
            return false;

        if (!_pieces.TryGetValue(fromPosition, out var piece))
            return false;

        _pieces.Remove(fromPosition); //We remove the old position from the dictionary
        _pieces[toPosition] = piece;

        OnPieceMoved(new PieceMovedEventArgs(piece, fromPosition, toPosition));

        return true;
    }

    public bool Take(HexPosition fromPosition)
    {
        if (!IsValid(fromPosition))
            return false;
        if (!_pieces.ContainsKey(fromPosition))
            return false;
        if (!_pieces.TryGetValue(fromPosition, out var piece))
            return false;

        _pieces.Remove(fromPosition);
        OnPieceTaken(new PieceTakenEventArgs(piece, fromPosition));

        return true;
    }

    protected virtual void OnPieceMoved(PieceMovedEventArgs eventArgs)
    {
        var handler = PieceMoved;
        handler?.Invoke(this, eventArgs);
    }
    protected virtual void OnPieceTaken(PieceTakenEventArgs eventArgs)
    {
        var handler = PieceTaken;
        handler?.Invoke(this, eventArgs);
    }
    protected virtual void OnPiecePlaced(CardDroppedEventArgs eventArgs)
    {
        var handler = PiecePlaced;
        handler?.Invoke(this, eventArgs);
    }    
}