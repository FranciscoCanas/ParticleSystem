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
        private static double PARTICLE_LEVEL = 1.0;
        KeyboardState current = new KeyboardState();
        KeyboardState previous = new KeyboardState();


        List<XNAEmitter> EmitterList = new List<XNAEmitter>();

        
        

        public ParticleTestSuite(Game game)
            : base(game)
        {
            parent = game;
            InitializeEmitters();
        }

        public void InitializeEmitters()
        {
            // TODO: Construct any child components here
            EmitterList.Clear();
            EmitterList.Add(new XNAEmitter(parent, "D:\\workspace\\ParticleSystem\\ParticleTests\\ParticleTestsContent\\particleEmitter2.xml", PARTICLE_LEVEL));
            EmitterList.Add(new XNAEmitter(parent, "D:\\workspace\\ParticleSystem\\ParticleTests\\ParticleTestsContent\\particleEmitter1.xml", PARTICLE_LEVEL));
            EmitterList.Add(new XNAEmitter(parent, "D:\\workspace\\ParticleSystem\\ParticleTests\\ParticleTestsContent\\particleEmitter3.xml"));
            EmitterList.Add(new XNAEmitter(parent, "D:\\workspace\\ParticleSystem\\ParticleTests\\ParticleTestsContent\\particleEmitter4.xml"));        
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
                    if (EmitterList[1].EmitActivity)
                    {
                        EmitterList[0].Stop();
                        EmitterList[1].Stop();
                    }
                    else
                    {
                        EmitterList[0].Start();
                        EmitterList[1].Start();
                    }
                    break;
                case 2:
                    if (EmitterList[3].EmitActivity)
                    {
                        EmitterList[2].Stop();
                        EmitterList[3].Stop();
                    }
                    else
                    {
                        EmitterList[2].Start();
                        EmitterList[3].Start();
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
            if (current.IsKeyDown(Keys.I) && !previous.IsKeyDown(Keys.I))
            {
                InitializeEmitters();
            }

            previous = current;
            
            base.Update(gameTime);

            foreach (XNAEmitter emitter in EmitterList)
            {
                if (emitter != null) emitter.Update((double)(gameTime.ElapsedGameTime.Milliseconds));
                
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (XNAEmitter emitter in EmitterList)
            {
                if (emitter != null) emitter.Draw(spriteBatch);
            }
                       
        }
    }
}
