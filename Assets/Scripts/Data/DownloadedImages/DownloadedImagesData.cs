using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DownloadedImagesData", menuName = "Data/DownloadedImagesData")]
public class DownloadedImagesData : ScriptableObject
{
    private List<Sprite> _sprites = new List<Sprite>();

    public List<Sprite> Sprites => _sprites;

    public void AddSprite(Sprite sprite)
    {
        _sprites.Add(sprite);
    }

    public void Clear()
    {
        _sprites.Clear();
    }
}
