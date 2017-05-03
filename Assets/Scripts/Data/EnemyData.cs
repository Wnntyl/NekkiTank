using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[SerializeField]
public class EnemyData
{
    public float health;
    public float armor;
    public float maxSpeed;
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