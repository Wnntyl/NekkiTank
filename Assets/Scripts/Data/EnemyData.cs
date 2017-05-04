using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

[Serializable]
public class EnemyData: EntityData
{
    public float damage;
    public string spriteName;

    private Sprite _sprite;

    public Sprite sprite
    {
        get
        {
            if (_sprite == null)
            {
                var tanksSpritesheet = Resources.LoadAll<Sprite>("GFX/enemies_spritesheet");
                _sprite = tanksSpritesheet.Single(s => s.name == spriteName);
            }

            return _sprite;
        }
    }
}