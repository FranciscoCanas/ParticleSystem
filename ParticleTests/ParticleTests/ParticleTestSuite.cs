

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
        KeyboardState current = new KeyboardState();
        KeyboardState previous = new KeyboardState();
           
        
        XNAEmitter pe1; // Smoke
        XNAEmitter pe2; // Fire
        
        XNAEmitter pe3;
        XNAEmitter pe4;
        
        

        public ParticleTestSuite(Game game)
            : base(game)
        {
            parent = game;
            // TODO: Construct any child components here
            pe1 = new XNAEmitter(parent, "D:\\workspace\\ParticleSystem\\ParticleTests\\ParticleTestsContent\\particleEmitter1.xml");
            pe2 = new XNAEmitter(parent, "D:\\workspace\\ParticleSystem\\ParticleTests\\ParticleTestsContent\\particleEmitter2.xml");
            pe3 = new XNAEmitter(parent, "D:\\workspace\\ParticleSystem\\ParticleTests\\ParticleTestsContent\\particleEmitter3.xml");
            pe4 = new XNAEmitter(parent, "D:\\workspace\\ParticleSystem\\ParticleTests\\ParticleTestsContent\\particleEmitter4.xml");        
            
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            base.Initialize();
        }

        public void ToggleEffect(int num) {
            switch (num)
            {
                case 1:
                    if (pe2.EmitActivity)
                    {
                        pe1.Stop();
                        pe2.Stop();
                    }
                    else
                    {
                        pe1.Start();
                        pe2.Start();
                    }
                    break;
                case 2:
                    if (pe4.EmitActivity)
                    {
                        pe3.Stop();
                        pe4.Stop();
                    }
                    else
                    {
                        pe3.Start();
                        pe4.Start();
                    }
                    break;
            }
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            current = Keyboard.GetState();

            if (current.IsKeyDown(Keys.Escape) && !previous.IsKeyDown(Keys.Escape))
            {
                parent.Exit();
            }
            if (current.IsKeyDown(Keys.D1) && !previous.IsKeyDown(Keys.D1))
            {
                ToggleEffect(1);
            }
            if (current.IsKeyDown(Keys.D2) && !previous.IsKeyDown(Keys.D2))
            {
                ToggleEffect(2);
            }

            previous = current;
            
            base.Update(gameTime);
            if (pe1 != null ) pe1.Update(gameTime.ElapsedGameTime.Milliseconds);
            if (pe2 != null ) pe2.Update(gameTime.ElapsedGameTime.Milliseconds);
            if (pe3 != null) pe3.Update(gameTime.ElapsedGameTime.Milliseconds);
            if (pe4 != null) pe4.Update(gameTime.ElapsedGameTime.Milliseconds);
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            pe2.Draw(spriteBatch);
            pe4.Draw(spriteBatch);
            pe1.Draw(spriteBatch);
            pe3.Draw(spriteBatch);
                       
        }
    }
}
