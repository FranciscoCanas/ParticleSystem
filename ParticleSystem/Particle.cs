using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GRNG;


namespace ParticleSystem
{
    public class Particle
    {
        
        /**
         * Members
         **/

        /**
         * The parent emitter.
         * ie. "the thing that done launchered it"
         **/
        public ParticleEmitter parentEmitter { get; set; }

        /**
         * Properties
         **/

        /**
         * Position on screen.
         * */
        public Vector3D position;

        /**
         * Velocity vector.
         **/
        private Vector3D velocity;

        /**
         * Acceleration vector.
         **/
        private Vector3D acceleration;

        /**
         * Rotation angle
         **/
        public double angle { get; set; }

        /**
         * How fast particle rotates.
         **/
        private double angularVelocity;

        /**
         * RGB color vector.
         **/
        public Vector3D color { get; set; }

        public double transparency { get; set; }

        /**
         * Scale at which to draw particle.
         **/
        public double size { get; set; }

        /**
         * Amount by which size changes every update.
         * (multiplicative, not additive)
         **/
        private double sizeDelta;

        /**
         * Number of updates this particle is active for. 
         **/
        public int TTL { get; set; }

        /**
         * Index of sprite/Texture used by this particle.
         * Used for emitters with multiple textures.
         **/
        public int TextureIndex { get; set; }


       


        public Particle(
            ParticleEmitter parent,
            Vector3D position,
            Vector3D velocity,
            Vector3D acceleration,
            double angle,
            double angVel,
            Vector3D col,
            double alpha,
            double size,
            double sizeDelta,
            int ttl,
            int txtIndex
            )
        {
            this.parentEmitter = parent;
            this.position = position;
            this.velocity = velocity;
            this.acceleration = acceleration;
            this.angle = angle;
            this.angularVelocity = angVel;
            this.color = col;
            this.transparency = alpha;
            this.size = size;
            this.sizeDelta = sizeDelta;
            this.TTL = ttl;
            this.TextureIndex = txtIndex;
        }




        /**
         * Updated every tick.
         **/
        public void Update(int TimeSinceLastUpdate)
        {
            TTL-=TimeSinceLastUpdate;

            position.X += velocity.X;
            position.Y += velocity.Y;
            position.Z += velocity.Z;

            velocity.X += acceleration.X;
            velocity.Y += acceleration.Y;
            velocity.Z += acceleration.Z;

            angle += angularVelocity;
            size *= sizeDelta;

        }    
        
    }
}
