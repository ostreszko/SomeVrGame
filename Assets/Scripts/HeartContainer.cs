using System.Collections.Generic;
using System.Linq;

public class HeartContainer
{
    private readonly List<Heart> _hearts;
    public HeartContainer(List<Heart> hearts)
    {
        _hearts = hearts;
    }

    public void Replenish(int heartPieces)
    {
        foreach (var heart in _hearts)
        {
            var emptyHeartsPieces = heart.EmptyHeartPieces;
            if (heartPieces <= 0) break; else if (emptyHeartsPieces == 0) continue;
            heart.Replenish(heartPieces);
            heartPieces -= emptyHeartsPieces;
        }
    }

    public void Deplate(int heartPieces)
    {
        foreach (var heart in _hearts.AsEnumerable().Reverse())
        {
            var FilledHeartPieces = heart.FilledHeartPieces;
            if (heartPieces <= 0) break; else if (FilledHeartPieces == 0) continue;
            heart.Deplate(heartPieces);
            heartPieces -= FilledHeartPieces;
        }
    }
}

