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
        private static String XML_DIR = "Content\\XMLs\\";
        KeyboardState current = new KeyboardState();
        KeyboardState previous = new KeyboardState();
        MouseState currentMouse = new MouseState();
        MouseState previousMouse = new MouseState();
        Vector2 location = new Vector2(250, 250);

        Projectile tester1;

        int currentEffect = 1;

        List<XNAEmitter> CurrentEmitter = new List<XNAEmitter>();
        List<XNAEmitter> EmitterList1 = new List<XNAEmitter>();
        List<XNAEmitter> EmitterList2 = new List<XNAEmitter>();
        List<XNAEmitter> EmitterList3 = new List<XNAEmitter>();

        public ParticleTestSuite(Game game)
            : base(game)
        {
            parent = game;
            tester1 = new Projectile(
                XML_DIR + "Projectile1.xml",
                new Vector3D(),
                new Vector3D(),
                new Vector3D(),
                0.0,
                0.0,
                new Vector3D(),
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0);

            
            InitializeEmitters();
        }

        public void InitializeEmitters()
        {
            // TODO: Construct any child components here
            EmitterList1.Clear();
            EmitterList2.Clear();
            EmitterList3.Clear();
            EmitterList1.Add(new XNAEmitter(parent, new Vector2(250.0f, 250.0f), XML_DIR + "particleEmitter2.xml", PARTICLE_LEVEL));
            EmitterList1.Add(new XNAEmitter(parent, new Vector2(250.0f, 250.0f), XML_DIR + "particleEmitter1.xml", PARTICLE_LEVEL));
            EmitterList2.Add(new XNAEmitter(parent, new Vector2(350.0f, 250.0f), XML_DIR + "particleEmitter3.xml", PARTICLE_LEVEL, 0.15));
            EmitterList2.Add(new XNAEmitter(parent, new Vector2(350.0f, 250.0f), XML_DIR + "particleEmitter4.xml", PARTICLE_LEVEL, 0.15));
            EmitterList3.Add(new XNAEmitter(parent, new Vector2(350.0f, 250.0f), XML_DIR + "squareEmitter.xml", PARTICLE_LEVEL, 1.0));
            EmitterList3.Add(new XNAEmitter(parent, new Vector2(350.0f, 250.0f), XML_DIR + "squareEmitter.xml", PARTICLE_LEVEL, 1.0));
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

        public void ToggleEffect(int num) 
        {
            switch (num)
            {
                case 1:

                    EmitterList1.Add(new XNAEmitter(parent, location, XML_DIR + "particleEmitter2.xml", PARTICLE_LEVEL));
                    EmitterList1.Add(new XNAEmitter(parent, location, XML_DIR + "particleEmitter1.xml", PARTICLE_LEVEL));
                    TriggerEffects(EmitterList1);
                    break;
                case 2:
                    EmitterList2.Add(new XNAEmitter(parent, location, XML_DIR + "particleEmitter3.xml", PARTICLE_LEVEL, 0.25));
                    EmitterList2.Add(new XNAEmitter(parent, location, XML_DIR + "particleEmitter4.xml", PARTICLE_LEVEL, 0.25));
                    TriggerEffects(EmitterList2);
                    break;
                case 3:
                    EmitterList3.Add(new XNAEmitter(parent, location, XML_DIR + "squareEmitter.xml", PARTICLE_LEVEL, 1.0));
                    EmitterList3.Add(new XNAEmitter(parent, location, XML_DIR + "squareEmitter.xml", PARTICLE_LEVEL, 1.0));
                    TriggerEffects(EmitterList3);
                    break;
            }
        }

        
        private void TriggerEffects(List<XNAEmitter> list)
        {
            list[list.Count - 2].Start();
            list[list.Count - 1].Start();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            current = Keyboard.GetState();
            currentMouse = Mouse.GetState();

            if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
            {
                location = new Vector2(currentMouse.X, currentMouse.Y);
                ToggleEffect(currentEffect);
            }

            if (currentMouse.RightButton == ButtonState.Released && previousMouse.RightButton == ButtonState.Pressed)
            {
                location = new Vector2(currentMouse.X, currentMouse.Y);
                ToggleEffect(currentEffect);
            }

            if (current.IsKeyDown(Keys.Escape) && !previous.IsKeyDown(Keys.Escape))
            {
                parent.Exit();
            }
            if (current.IsKeyDown(Keys.D1) && !previous.IsKeyDown(Keys.D1))
            {
                currentEffect = 1;
            }
            if (current.IsKeyDown(Keys.D2) && !previous.IsKeyDown(Keys.D2))
            {
                currentEffect = 2;
            }
            if (current.IsKeyDown(Keys.D3) && !previous.IsKeyDown(Keys.D3))
            {
                currentEffect = 3;
            }
            if (current.IsKeyDown(Keys.I) && !previous.IsKeyDown(Keys.I))
            {
                InitializeEmitters();
            }
            if (current.IsKeyDown(Keys.Down))
            {
                location.Y += 10;
            }
            if (current.IsKeyDown(Keys.Up))
            {
                location.Y -= 10;
            }
            if (current.IsKeyDown(Keys.Left))
            {
                location.X -= 10;
            }
            if (current.IsKeyDown(Keys.Right))
            {
                location.X += 10;
            }

            previous = current;
            previousMouse = currentMouse;
            UpdateEmitterList(EmitterList1, gameTime);
            UpdateEmitterList(EmitterList2, gameTime);
            UpdateEmitterList(EmitterList3, gameTime);
            base.Update(gameTime); 
        }

        /**
         *  
         **/
        private void UpdateEmitterList(List<XNAEmitter> list, GameTime gameTime)
        {
            for (int index = 0; index < list.Count; index++)
            {
                if (list[index].IsAlive()) list[index].Update((double)(gameTime.ElapsedGameTime.Milliseconds));
                else list.RemoveAt(index);
            }
        }

        /**
         * 
         **/
        public void Draw(SpriteBatch spriteBatch)
        {
            DrawEmitterList(EmitterList1, spriteBatch);
            DrawEmitterList(EmitterList2, spriteBatch);
            DrawEmitterList(EmitterList3, spriteBatch);           
        }

        private void DrawEmitterList(List<XNAEmitter> list, SpriteBatch spriteBatch)
        {
            foreach (XNAEmitter emitter in list)
            {
                if (emitter != null) emitter.Draw(spriteBatch);
            }
        }
    }
}
