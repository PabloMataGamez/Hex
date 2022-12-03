using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovedEventArgs<TPiece> : EventArgs
{
    public TPiece Piece { get; }

    public HexPosition FromPostion { get; }

    public HexPosition ToPosition { get; }

    public PieceMovedEventArgs(TPiece piece, HexPosition fromPostion, HexPosition toPosition)
    {
        Piece = piece;
        FromPostion = fromPostion;
        ToPosition = toPosition;
    }
}

public class PieceTakenEventArgs<TPiece> : EventArgs
{
    public TPiece Piece { get; }

    public HexPosition FromPostion { get; }

    public PieceTakenEventArgs(TPiece pieceView, HexPosition fromPostion)
    {
        Piece = pieceView;
        FromPostion = fromPostion;
    }
}

public class CardDroppedEventArgs<TPiece> : EventArgs
{
    public TPiece Piece { get; }

    public HexPosition ToPosition { get; }

    public CardDroppedEventArgs(TPiece pieceView, HexPosition toPosition)
    {
        Piece = pieceView;
        ToPosition = toPosition;
    }
}

public class HexBoard<TPiece>
{
    //  PieceView[,] position = new PieceView[PositionHelper.Columns, PositionHelper.Rows];

    public event EventHandler<PieceMovedEventArgs<TPiece>> PieceMoved;
    public event EventHandler<PieceTakenEventArgs<TPiece>> PieceTaken;
    public event EventHandler<CardDroppedEventArgs<TPiece>> PiecePlaced;

    private Dictionary<HexPosition, TPiece> _pieces = new Dictionary<HexPosition, TPiece>();
    private int _range = 3;
    private int _q; //STILL NEEDED? AKA ROWS
    private int _r; //STILL NEEDED? AKA COLUMNS

    public HexBoard(int q, int r)
    {
        _q = q;
        _r = r;
    }

    public bool TryGetPieceAt(HexPosition position, out TPiece piece)
    {
        return _pieces.TryGetValue(position, out piece);
    }

    public bool IsValid(HexPosition position) //To Hex Range
    {
        return (-_range <= position.Q && position.Q < _range) && (-_range <= position.R && position.R < _range); //CORRECT? 

        /*
         var results = [] 
        for each -N <= q <= +N: 
            for each max(-N, -q-N) <= r <= min(+N, -q+N): 
                results.append(axial_add(center, Hex(q, r)))
         */
    }

    public bool Place(HexPosition position, TPiece piece)
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

        OnPiecePlaced(new CardDroppedEventArgs<TPiece>(piece, position));

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

        OnPieceMoved(new PieceMovedEventArgs<TPiece>(piece, fromPosition, toPosition));

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
        OnPieceTaken(new PieceTakenEventArgs<TPiece>(piece, fromPosition));

        return true;
    }

    protected virtual void OnPieceMoved(PieceMovedEventArgs<TPiece> eventArgs)
    {
        var handler = PieceMoved;
        handler?.Invoke(this, eventArgs);
    }
    protected virtual void OnPieceTaken(PieceTakenEventArgs<TPiece> eventArgs)
    {
        var handler = PieceTaken;
        handler?.Invoke(this, eventArgs);
    }
    protected virtual void OnPiecePlaced(CardDroppedEventArgs<TPiece> eventArgs)
    {
        var handler = PiecePlaced;
        handler?.Invoke(this, eventArgs);
    }    
}