using System;
using UnityEngine.UI;

public class Heart
{
    private Image _image;
    private const float FillPercentage = 0.25f;
    public const int HeartPiecesPerHeart = 4;

    public int FilledHeartPieces { get { return CalculateFilledHeartPieces(); } }

    public int EmptyHeartPieces 
    { get { return HeartPiecesPerHeart - CalculateFilledHeartPieces(); } }

    private int CalculateFilledHeartPieces()
    {
        return (int)(_image.fillAmount * HeartPiecesPerHeart);
    }

    public Heart(Image image)
    {
        this._image = image;
    }

    public void Replenish(int numberOfHeartPieces)
    {
        if (numberOfHeartPieces < 0) throw new ArgumentOutOfRangeException("numberOfHeartPieces");
        _image.fillAmount += numberOfHeartPieces * FillPercentage;
    }

    public void Deplate(int numberOfHeartPieces)
    {
        if (numberOfHeartPieces < 0) throw new ArgumentOutOfRangeException("numberOfHeartPieces");
        _image.fillAmount -= numberOfHeartPieces * FillPercentage;
    }
}