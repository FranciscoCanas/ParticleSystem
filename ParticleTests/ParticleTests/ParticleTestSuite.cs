

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ParticleSystem;
using GRNG;


namespace ParticleTests
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ParticleTestSuite : Microsoft.Xna.Framework.GameComponent
    {
        Game parent;
        XNAEmitter pe1;
        XNAEmitter pe2;
        Texture2D p1Texture, p2Texture;
        SpriteEffects p1Effects, p2Effects;

        public ParticleTestSuite(Game game)
            : base(game)
        {
            parent = game;
            p1Effects = SpriteEffects.None;
            p2Effects = SpriteEffects.None;
            p1Texture = game.Content.Load<Texture2D>(@"smoke2");
            p2Texture = game.Content.Load<Texture2D>(@"fire");

            // TODO: Construct any child components here
            pe1 = new XNAEmitter(
                p1Texture, 
                p1Effects,
                new Vector3D(250, 250, 0), // Pos
                 new Vector3D(50, 50, 0),
                 Distribution.Normal,
                  new Vector3D(-3, -5, 0), // Vel
                   new Vector3D(3, -1, 0),
                   Distribution.Uniform,
                    new Vector3D(0, 0, 0), // Acc
                    new Vector3D(0, 0, 0),
                    Distribution.Fixed,
                    0.0, // angleAlpha
                    0.0, // angleBeta
                    Distribution.Uniform,
                    0.0005, //AngVelMean
                    0.005, // AngVelVar
                    Distribution.Uniform,
                    new Vector3D(255, 255, 255), // ColorMean
                    new Vector3D(255, 255, 255), // ColorVar
                    Distribution.Fixed,
                    255.0, 255.0, // alphamin and max
                    Distribution.Uniform,
                    0.5, 0.005, // SizeMean, SizeVar
                    Distribution.Normal,
                    1.0, 0.000, //SizeDeltaMean and Var
                    Distribution.Fixed,
                    3000, // TTLMean
                    1000, // TTLvar
                    Distribution.Normal,
                    new Vector3D(200,200,0), // Emitter Loc
                    new Vector3D(25, 25, 0), // Emitter Dim
                    5000, // MaxNumParticles
                    5, // EmitRate
                    50, // EmitDelay
                    15000, // Emitter Life
                    false);

            pe2 = new XNAEmitter(
                p2Texture,
                p2Effects,
                new Vector3D(250, 250, 0), // Pos
                 new Vector3D(50, 50, 0),
                 Distribution.Normal,
                  new Vector3D(-1, -1, 0), // Vel
                   new Vector3D(1, -1, 0),
                   Distribution.Uniform,
                    new Vector3D(0, 0, 0), // Acc
                    new Vector3D(0, 0, 0),
                    Distribution.Fixed,
                    0.0, // angleAlpha
                    0.0, // angleBeta
                    Distribution.Uniform,
                    0.0005, //AngVelMean
                    0.005, // AngVelVar
                    Distribution.Uniform,
                    new Vector3D(255, 255, 255), // ColorMean
                    new Vector3D(255, 255, 255), // ColorVar
                    Distribution.Fixed,
                    255.0, 255.0, // alphamean and var
                    Distribution.Fixed,
                    0.1, 0.005, // SizeMean, SizeVar
                    Distribution.Normal,
                    1.0, 0.000, //SizeDeltaMean and Var
                    Distribution.Fixed,
                    1000, // TTLMean
                    100, // TTLvar
                    Distribution.Normal,
                    new Vector3D(200, 200, 0), // Emitter Loc
                    new Vector3D(25, 25, 0), // Emitter Dim
                    5000, // MaxNumParticles
                    5, // EmitRate
                    50, // EmitDelay
                    15000, // Emitter Life
                    false);      
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            pe1.Start();
            pe2.Start();
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            
            base.Update(gameTime);
            pe1.Update(gameTime.ElapsedGameTime.Milliseconds);
            pe2.Update(gameTime.ElapsedGameTime.Milliseconds);
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            pe2.Draw(spriteBatch);
            pe1.Draw(spriteBatch);
            
        }
    }
}
