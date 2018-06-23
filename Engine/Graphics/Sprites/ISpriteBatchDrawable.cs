using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Graphics.TextureAtlases;

namespace Engine.Graphics.Sprites
{
    public interface ISpriteBatchDrawable
    {
        bool IsVisible { get; }
        TextureRegion2D TextureRegion { get; }
        Vector2 Position { get; }
        float Rotation { get; }
        Vector2 Scale { get; }
        Color Color { get; }
        Vector2 Origin { get; }
        SpriteEffects Effect { get; }
    }
}