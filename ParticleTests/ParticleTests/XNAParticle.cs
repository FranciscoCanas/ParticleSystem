using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParticleSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ParticleTests
{
    class XNAParticle
    {
        public Texture2D Texture { get; set; }
        public Rectangle Hitbox { get; set; }
        public SpriteEffects spriteEffects { get; set; }
        /**
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                new Vector2((float)position.X, (float)position.Y),
                null,
                new Color((float)color.X, (float)color.Y, (float)color.Z),
                (float)angle,
                new Vector2(),
                (float)size,
                spriteEffects,
                0);

                
        }**/
    }
}
