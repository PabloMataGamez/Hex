using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovedEventArgs : EventArgs
{
  //  public HexPieceView Piece { get; } //DO I HAVE TO PASS IT AS THE TPIECE?

    public HexPosition FromPostion { get; }

    public HexPosition ToPosition { get; }

    public PieceMovedEventArgs(HexPosition fromPostion, HexPosition toPosition)
    {
       // Piece = hexPieceView;
        FromPostion = fromPostion;
        ToPosition = toPosition;
    }
}

public class PieceTakenEventArgs : EventArgs
{  
    public HexPosition FromPostion { get; }

    public PieceTakenEventArgs(HexPosition fromPostion)
    {      
        FromPostion = fromPostion;
    }
}

public class CardDroppedEventArgs : EventArgs
{   

    public HexPosition ToPosition { get; }

    public CardDroppedEventArgs(HexPosition toPosition)
    { 
        ToPosition = toPosition;
    }
}

public class HexBoard
{
    //  PieceView[,] position = new PieceView[PositionHelper.Columns, PositionHelper.Rows];

    public event EventHandler<PieceMovedEventArgs> PieceMoved;
    public event EventHandler<PieceTakenEventArgs> PieceTaken;
    public event EventHandler<CardDroppedEventArgs> PiecePlaced;

    private Dictionary<HexPosition, HexPieceView> _pieces = new Dictionary<HexPosition, HexPieceView>();
    private int _range = 3;
   
    public HexBoard( )
    {
        
    }

    public bool TryGetPieceAt(HexPosition position, out HexPieceView piece)
    {
        return _pieces.TryGetValue(position, out piece);
    }

    public bool IsValid(HexPosition position) //To Hex Range
    {
        return (-_range <= position.Q && position.Q <= _range) 
            && (-_range <= position.R && position.R <= _range); //Enough for now, pay attention         
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

        OnPiecePlaced(new CardDroppedEventArgs(position));

        return true;
    }

    public bool Move(HexPosition fromPosition, HexPosition toPosition)
    {
        if (!IsValid(toPosition))
            return false;

        if (_pieces.ContainsKey(toPosition))
            return false;

        if (!_pieces.TryGetValue(fromPosition, out var piece))
            return false;

        _pieces.Remove(fromPosition);
        _pieces[toPosition] = piece;

        OnPieceMoved(new PieceMovedEventArgs(fromPosition, toPosition));

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
        OnPieceTaken(new PieceTakenEventArgs(fromPosition));

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