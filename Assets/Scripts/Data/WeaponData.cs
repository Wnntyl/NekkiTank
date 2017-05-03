using System;
using UnityEngine;
using System.Linq;

[Serializable]
public class WeaponData
{
    public float damage;
    public string spriteName;
    public string bulletSpriteName;

    private Sprite _sprite;

    public Sprite sprite
    {
        get
        {
            if (_sprite == null)
            {
                var tanksSpritesheet = Resources.LoadAll<Sprite>("GFX/tanks_spritesheet");
                _sprite = tanksSpritesheet.Single(s => s.name == spriteName);
            }

            return _sprite;
        }
    }
}