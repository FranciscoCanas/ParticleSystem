using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GRNG;


namespace ParticleSystem
{
    public class ParticleEmitter
    {
        /**
         * Members
         **/

        /**
         * Random number generator used for initializing particles.
         **/
        
        private GRandom grandom;

        /**
         * List contains references to all active particles until removed.
         **/
        public List<Particle> particles;

        

        /**
         * Properties of this Particle Emitter:
         * Alpha and Beta params used to randomly determine
         * the corresponding property of a particle when it
         * is instantiated by the emitter.
         * 
         * For normally-distributed params, alpha=mean and
         * beta=variance.
         * For uniformly-distributed params, alpha=min and
         * beta=max.
         * For exponentially-distributed params, alpha=mean.
         * beta is unused.
         * For fixed params, only alpha is used.
         **/

        /**
         * Position is relative to emitter's centre.
         **/
        Vector3D positionAlpha; 
        Vector3D positionBeta;
        Distribution positionDist;

        Vector3D velocityAlpha;
        Vector3D velocityBeta;
        Distribution velocityDist;

        Vector3D accelerationAlpha;
        Vector3D accelerationBeta;
        Distribution accelerationDist;

        double angleAlpha; 
        double angleBeta;
        Distribution angleDist;

        double angVelocityAlpha;
        double angVelocityBeta;
        Distribution angVelocityDist;

        Vector3D colorAlpha;
        Vector3D colorBeta;
        Distribution colorDist;

        double tranparencyAlpha;
        double transparencyBeta;
        Distribution transparencyDist;

        double sizeAlpha;
        double sizeBeta;
        Distribution sizeDist;

        double sizeGrowthAlpha;
        double sizeGrowthBeta;
        Distribution sizeGrowthDist;

        double ttlAlpha;
        double ttlBeta;
        Distribution ttlDist;

      

        /**
         * Particle Emitter behaviour parameters. These properties 
         * belong to the emitter itself, not to the particles.
         **/
        public Vector3D Location { get; set; } // Center point of emitter.
        public Vector3D EmitDimensions { get; set; } // Area from which particles can be emitted. currently unused.
        
        
        public int MaxNumParticles { get; set; } // Max number of particles allowed in queue.
        public int EmitRate { get; set; } // How many particles we can generate each update.
        public int EmitDelay { get; set; } // How much time between individual new particle emits, in MS.
        public int EmitLifetime { get; set; } // How long emitter emits for, in MS.
        public int CurrentLifeTime { get; set; } // Running total of time spent emitting, in MS.
        public int TimeSinceLastEmit { get; set; } // How long ago we last emitted, in MS.


        public bool EmitActivity { get; set; } // Whether emitter should currently emitting particles.
        public bool PermanentParticles { get; set; } // Are particles permanent or do they disappear?

        public int NumTextures { get; set; } // Used for multi-texture emitters.






        /**
         * Current position of the emitter on screen.
         **/
        public Vector3D position { get; set; }
        

        /**
         * Constructors
         **/
        public ParticleEmitter()
        {
            grandom = new GRandom();
            particles = new List<Particle>();
        }

        /**
         * Constructor taking filename with particle parameters.
         **/
        public ParticleEmitter(String paramFileName) : this()
        {
            
        }

        /**
         * Explicit Constructor.
         **/ 

        public ParticleEmitter(
            /** Particle parameters **/
            Vector3D positionMean, Vector3D positionVar, Distribution pDist,
            Vector3D velocityMean, Vector3D velocityVar, Distribution vDist,
            Vector3D accelerationMean, Vector3D accelerationVar, Distribution aDist,
            double angleMean, double angleVar, Distribution angleDist,
            double angVelocityMean, double angVelocityVar, Distribution angVelDist,
            Vector3D colorMean, Vector3D colorVar, Distribution colDist,
            double alphaMean, double alphaVar, Distribution tranDist,
            double sizeMean, double sizeVar, Distribution sizeDist,
            double sizeDeltaMean, double sizeDeltaVar, Distribution sizeDeltaDist,
            double ttlMean, double ttlVar, Distribution ttlDist,
            /** Emitter Parameters **/
            Vector3D location,
            Vector3D dimension,
            int maxNumPart,
            int emitRate,
            int emitDelay,
            int emitLife,
            bool permParts
            ) : this() 
        {
            this.positionAlpha = positionMean;
            this.positionBeta = positionVar;
            this.positionDist = pDist;

            this.velocityAlpha = velocityMean;
            this.velocityBeta = velocityVar;
            this.velocityDist = vDist;

            this.accelerationAlpha = accelerationMean;
            this.accelerationBeta = accelerationVar;
            this.accelerationDist = aDist;

            this.angleAlpha = angleMean;
            this.angleBeta = angleVar;
            this.angleDist = angleDist;

            this.angVelocityAlpha = angVelocityMean;
            this.angVelocityBeta = angVelocityVar;
            this.angVelocityDist = angVelDist;

            this.colorAlpha = colorMean;
            this.colorBeta = colorVar;
            this.colorDist = colDist;

            this.tranparencyAlpha = alphaMean;
            this.transparencyBeta = alphaVar;
            this.transparencyDist = tranDist;

            this.sizeAlpha = sizeMean;
            this.sizeBeta = sizeVar;
            this.sizeDist = sizeDist;

            this.sizeGrowthAlpha = sizeDeltaMean;
            this.sizeGrowthBeta = sizeDeltaVar;
            this.sizeGrowthDist = sizeDeltaDist;

            this.ttlAlpha = ttlMean;
            this.ttlBeta = ttlVar;
            this.ttlDist = ttlDist;


            this.Location = location;
            this.EmitDimensions = dimension;
            this.MaxNumParticles = maxNumPart;
            this.EmitRate = emitRate;
            this.EmitDelay = emitDelay;
            this.EmitLifetime = emitLife;
            this.PermanentParticles = permParts;

            NumTextures = 0;

            
        }

        /**
         * Returns a new particle with randomized properties based on
         * emitter parameters.
         **/
        private Particle GenerateParticle()
        {
            Vector3D generateLocation;

            if (positionDist == Distribution.Normal)
            {
                
                generateLocation = grandom.GetNormalVector3D(
                    new Vector3D(Location.X + positionAlpha.X, 
                        Location.Y + positionAlpha.Y,
                        Location.Z + positionAlpha.Z),
                         
                    positionBeta);
            }
            else
            {
                generateLocation = grandom.GetUniformVector3D(
                    new Vector3D(Location.X - positionAlpha.X,
                        Location.Y - positionAlpha.Y,
                        Location.Z - positionAlpha.Z),
                    new Vector3D(Location.X + positionBeta.X,
                        Location.Y + positionBeta.Y,
                        Location.Z + positionBeta.Z));      
            }


            
            return new Particle(this,
                generateLocation,
                grandom.GetRandomVector3D(velocityDist, velocityAlpha, velocityBeta),
                grandom.GetRandomVector3D(accelerationDist, accelerationAlpha, accelerationBeta),
                grandom.GetRandomDouble(angleDist, angleAlpha, angleBeta), 
                grandom.GetRandomDouble(angVelocityDist, angVelocityAlpha, angVelocityBeta),
                grandom.GetRandomVector3D(colorDist, colorAlpha, colorBeta),
                grandom.GetRandomDouble(transparencyDist, tranparencyAlpha, transparencyBeta),
                grandom.GetRandomDouble(sizeDist, sizeAlpha, sizeBeta),
                grandom.GetRandomDouble(sizeGrowthDist,sizeGrowthAlpha, sizeGrowthBeta),
                (int)grandom.GetRandomDouble(ttlDist, ttlAlpha, ttlBeta),
                grandom.GetUniformInt(0, NumTextures));

        }

        public void Start()
        {
            EmitActivity = true;
            CurrentLifeTime = 0;
            TimeSinceLastEmit = 0;
        }

        /** 
         * Immediately cease to emit particles.
         **/
        private void Stop()
        {
            EmitActivity = false;
        }

        /** 
         * Updates every particle for this emitter.
         **/
        public void Update(int MSSinceLastUpdate /**Milliseconds**/)
        {

            CurrentLifeTime += MSSinceLastUpdate; // Update lifetime timer.
            TimeSinceLastEmit += MSSinceLastUpdate; // Update emitrate timer.

            if (CurrentLifeTime > EmitLifetime)
            {
                Stop();
            }
            else
            {
                if (TimeSinceLastEmit > EmitDelay)
                {
                    /**
                    * If emitter is still emitting, and it's been
                    * long enough since the last emit, let's make
                    * some more particles.
                    **/
                    TimeSinceLastEmit = 0;
                    // If we still have room on the particle queue, make as many particles
                    // as we're allowed in this update.
                    for (int i = 0; ((i < EmitRate) && (particles.Count < MaxNumParticles)); i++)
                    {
                        particles.Add(GenerateParticle());
                    }

                }
            }
            
            /** 
             * Update existing particles:
             **/
            for (int index = 0; index < particles.Count; index++)
            {
                // If particle is still alive or emitter uses perma particles.
                if ((particles[index].TTL > 0) || PermanentParticles)
                {
                    particles[index].Update(MSSinceLastUpdate);
                }
                else
                {
                    /**
                    * Remove dead particle from queue.
                    **/
                    particles.RemoveAt(index);
                    index--;
                }
            }
        }

       

    }
}
