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
        Texture2D particleTexture;

        public XNAEmitter()
        {

        }

        public XNAEmitter(Texture2D tex,
            SpriteEffects spFX,
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
            particleTexture = tex;
            spriteEffects = spFX;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            

            foreach (Particle p in particles)
            {
                if ((p.TTL > 0) || PermanentParticles)
                {

                    spriteBatch.Draw(particleTexture, 
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
