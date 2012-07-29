

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
        Texture2D p1Texture, p2Texture, p2Texture2, p2Texture3;
        SpriteEffects p1Effects, p2Effects;
        Vector3D TestLocation = new Vector3D(350, 350,0);
        

        public ParticleTestSuite(Game game)
            : base(game)
        {
            parent = game;
            p1Effects = SpriteEffects.None;
            p2Effects = SpriteEffects.None;
            p1Texture = game.Content.Load<Texture2D>(@"smoke2");
            p2Texture = game.Content.Load<Texture2D>(@"fire");
            p2Texture2 = game.Content.Load<Texture2D>(@"electric");
            p2Texture3 = game.Content.Load<Texture2D>(@"smoke2");

            // TODO: Construct any child components here
            pe1 = new XNAEmitter(
             
                new Vector3D(0, -25, 0), // Pos
                 new Vector3D(500, 10, 0),
                 Distribution.Normal,
                  new Vector3D(-0.25, -0.25, 0), // Vel
                   new Vector3D(0, -0.005, 0),
                   Distribution.Uniform,
                    new Vector3D(0.00, -0.01, 0), // Acc
                    new Vector3D(0.01, -0.001, 0),
                    Distribution.Uniform,
                    0.0, // angleAlpha
                    1.0, // angleBeta
                    Distribution.Uniform,
                    0.0005, //AngVelMean
                    0.015, // AngVelVar
                    Distribution.Uniform,
                    new Vector3D(255, 255, 255), // ColorMean
                    new Vector3D(255, 255, 255), // ColorVar
                    Distribution.Fixed,
                    255.0, 255.0, // alphamin and max
                    Distribution.Uniform,
                    0.35, 0.005, // SizeMean, SizeVar
                    Distribution.Normal,
                    1.0, 1.001, //SizeDeltaMean and Var
                    Distribution.Uniform,
                    8000, // TTLMean
                    1000, // TTLvar
                    Distribution.Normal,
                    TestLocation, // Emitter Loc
                    new Vector3D(25, 25, 0), // Emitter Dim
                    5000, // MaxNumParticles
                    5, // EmitRate
                    55, // EmitDelay
                    15000, // Emitter Life
                    false);

            pe1.LoadTexture(p1Texture);

            pe2 = new XNAEmitter(
             
                new Vector3D(0, 0, 0), // Pos
                 new Vector3D(500, 150, 0),
                 Distribution.Normal,
                  new Vector3D(0, -0.5, 0), // Vel
                   new Vector3D(0.5, -0.25, 0),
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
                    0.1, 0.0005, // SizeMean, SizeVar
                    Distribution.Normal,
                    1.0, 0.000, //SizeDeltaMean and Var
                    Distribution.Fixed,
                    800, // TTLMean
                    100, // TTLvar
                    Distribution.Normal,
                    TestLocation, // Emitter Loc
                    new Vector3D(25, 25, 0), // Emitter Dim
                    500, // MaxNumParticles
                    5, // EmitRate
                    10, // EmitDelay
                    15000, // Emitter Life
                    false);
            pe2.LoadTexture(p2Texture);
            pe2.LoadTexture(p2Texture2);
            
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

            
            pe1.Draw(spriteBatch);
            pe2.Draw(spriteBatch);
            
            
        }
    }
}
