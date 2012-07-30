using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParticleSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;
using GRNG;

namespace ParticleTests
{
    class XNAEmitter : ParticleEmitter
    {
        Game parent;
        SpriteEffects spriteEffects = SpriteEffects.None;
        BlendState blendState = BlendState.AlphaBlend;
        SpriteSortMode spriteSortMode = SpriteSortMode.Deferred;

        private List<Texture2D> TextureList = new List<Texture2D>();
        Texture2D particleTexture;
        private bool USES_MULTIPLE_TEXTURES = false;

        /**
         * XML-based contructor. Initializes emitter
         * with parameters from xmlFileName.
         **/
        public XNAEmitter(Game p, String xmlFileName) : base()
        {
            XmlDocument doc = new XmlDocument();

            parent = p;
            doc.Load(xmlFileName);
            LoadXMLEmitter(doc);
            LoadXNAXMLParameters(doc);
        }

        /**
         * Explicit contructor initializes emitter with 
         * specified parameters.
         **/
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

        public void LoadXNAXMLParameters(XmlDocument doc)
        {
            XmlNode XNAPars =
                doc.SelectSingleNode("/ParticleSystem/XNAParameters");

            
            
            spriteEffects = (SpriteEffects)Enum.Parse(typeof(SpriteEffects),
                Convert.ToString(XNAPars.SelectSingleNode("spriteEffects").
                Attributes.GetNamedItem("x").Value));
            /**
            blendState = (BlendState)Enum.Parse(typeof(BlendState),
                Convert.ToString(XNAPars.SelectSingleNode("blendState").
                Attributes.GetNamedItem("x").Value));**/

            spriteSortMode = (SpriteSortMode)Enum.Parse(typeof(SpriteSortMode),
                Convert.ToString(XNAPars.SelectSingleNode("spriteSortMode").
                Attributes.GetNamedItem("x").Value));

            foreach (XmlNode texture in XNAPars.SelectSingleNode("textureList"))
            {
                Texture2D n = parent.Content.Load<Texture2D>(
                    Convert.ToString(texture.Attributes.GetNamedItem("x").Value));
                
                LoadTexture(n);
            }
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

       

        /**
         * XNA-specific Draw method.
         **/
        public void Draw(SpriteBatch spriteBatch)
        {
            
            Texture2D DrawTexture = particleTexture;

            spriteBatch.Begin(spriteSortMode, blendState);
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
                        new Color((float)p.color.X, (float)p.color.Y, (float)p.color.Z, (float)p.transparency), 
                        (float)p.angle,
                        new Vector2(particleTexture.Width / 2, particleTexture.Height / 2), 
                        (float)p.size, 
                        spriteEffects, 0f); 

                }
            }

            spriteBatch.End();
            
        }
    }
}
