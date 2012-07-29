using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParticleSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GRNG;

namespace ParticleTests
{
    class XNAEmitter : ParticleEmitter
    {
        SpriteEffects spriteEffects;
        private List<Texture2D> TextureList = new List<Texture2D>();
        Texture2D particleTexture;
        private bool USES_MULTIPLE_TEXTURES = false;

        public XNAEmitter()
        {
            
        }

        public XNAEmitter(
            Vector3D positionMean, Vector3D positionVar, Distribution pDist,
            Vector3D velocityMean, Vector3D velocityVar, Distribution vDist,
            Vector3D accelerationMean, Vector3D accelerationVar, Distribution aDist,
            double angleMean, double angleVar, Distribution angDist,
            double angVelocityMean, double angVelocityVar, Distribution angVelDist,
            Vector3D colorMean, Vector3D colorVar, Distribution colDist,
            double alphaMean, double alphaVar, Distribution transDist,
            double sizeMean, double sizeVar, Distribution sizeDist,
            double sizeDeltaMean, double sizeDeltaVar, Distribution sizeGrowthDist,
            double ttlMean, double ttlVar, Distribution ttlDist,
            Vector3D location,
            Vector3D dimension,
            int maxNumPart,
            int emitRate,
            int emitDelay,
            int emitLife,
            bool permParts)
            : base(positionMean, positionVar, pDist,
            velocityMean, velocityVar, vDist,
            accelerationMean, accelerationVar, aDist,
            angleMean, angleVar, angDist,
            angVelocityMean, angVelocityVar, angVelDist,
            colorMean, colorVar, colDist,
            alphaMean, alphaVar, transDist,
            sizeMean, sizeVar, sizeDist,
            sizeDeltaMean, sizeDeltaVar, sizeGrowthDist,
            ttlMean, ttlVar,ttlDist,
            location,
            dimension,
             maxNumPart,
             emitRate,
             emitDelay,
             emitLife,
             permParts)
        {
             
        }

        /**
         * Adds textures to the list of particle
         * textures.
         **/
        public void LoadTexture(Texture2D texture)
        {
            TextureList.Add(texture);
            NumTextures++;
            if (TextureList.Count > 1)
            {
                USES_MULTIPLE_TEXTURES = true;
            }
            else
            {
                particleTexture = texture;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            Texture2D DrawTexture = particleTexture;

            foreach (Particle p in particles)
            {
                if ((p.TTL > 0) || PermanentParticles)
                {
                    if (USES_MULTIPLE_TEXTURES)
                    {
                        DrawTexture = TextureList[p.TextureIndex];
                    }
                    spriteBatch.Draw(DrawTexture, 
                        new Vector2((float)p.position.X, (float)p.position.Y), 
                        new Rectangle(0,0, particleTexture.Width, particleTexture.Height), 
                        new Color((float)p.color.X, (float)p.color.Y, (float)p.color.Z, (float)p.alpha), 
                        (float)p.angle,
                        new Vector2(particleTexture.Width / 2, particleTexture.Height / 2), 
                        (float)p.size, 
                        SpriteEffects.None, 0f); 
                    
            
                }
            }
            //spriteBatch.End();
        }
    }
}
