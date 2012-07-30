using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
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
         * For normally-distributed params, transparency=mean and
         * beta=variance.
         * For uniformly-distributed params, transparency=min and
         * beta=max.
         * For exponentially-distributed params, transparency=mean.
         * beta is unused.
         * For fixed params, only transparency is used.
         **/

        /**
         * Position is relative to emitter's centre.
         **/
        //protected Vector3D positionAlpha; 
        //protected Vector3D positionBeta;
        //protected Distribution positionDist;

        protected Parameter3D position = new Parameter3D();

        //protected Vector3D velocityAlpha;
        //protected Vector3D velocityBeta;
        //protected Distribution velocityDist;

        protected Parameter3D velocity = new Parameter3D();

        //protected Vector3D accelerationAlpha;
        //protected Vector3D accelerationBeta;
        //protected Distribution accelerationDist;

        protected Parameter3D acceleration = new Parameter3D();

        //protected double angleAlpha; 
        //protected double angleBeta;
        //protected Distribution angleDist;

        protected ParameterDouble angle = new ParameterDouble();

        //protected double angVelocityAlpha;
        //protected double angVelocityBeta;
        //protected Distribution angVelocityDist;

        protected ParameterDouble angularVelocity = new ParameterDouble();

        //Vector3D colorAlpha;
        //Vector3D colorBeta;
        //Distribution colorDist;

        protected Parameter3D color = new Parameter3D();

        //double tranparencyAlpha;
        //double transparencyBeta;
        //Distribution transparencyDist;

        protected ParameterDouble transparency = new ParameterDouble();

        //double sizeAlpha;
        //double sizeBeta;
        //Distribution sizeDist;

        protected ParameterDouble size = new ParameterDouble();

        //double sizeGrowthAlpha;
        //double sizeGrowthBeta;
        //Distribution sizeGrowthDist;

        protected ParameterDouble growth = new ParameterDouble();

        //double ttlAlpha;
        //double ttlBeta;
        //Distribution ttlDist;

        protected ParameterDouble ttl = new ParameterDouble();

      

        /**
         * Particle Emitter behaviour parameters. These properties 
         * belong to the emitter itself, not to the particles.
         **/
        public Vector3D Location { get; set; } // Center point of emitter.
        public Vector3D EmitDimensions { get; set; } // Area from which particles can be emitted. currently unused.
        
        
        public int MaxNumParticles { get; set; } // Max number of particles allowed in queue.
        public int EmitRate { get; set; } // How many particles we can generate each update.
        public int MeanEmitDelay { get; set; } // Mean time between individual new particle emits, in MS.
        public int EmitLifetime { get; set; } // How long emitter emits for, in MS.
        public int CurrentLifeTime { get; set; } // Running total of time spent emitting, in MS.
        public int TimeToNextEmit { get; set; } // How long ago we last emitted, in MS.


        public bool EmitActivity { get; set; } // Whether emitter should currently emitting particles.
        public bool PermanentParticles { get; set; } // Are particles permanent or do they disappear?

        public int NumTextures { get; set; } // Used for multi-texture emitters.






        
        

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
            this.position.alpha = positionMean;
            this.position.beta = positionVar;
            this.position.distribution = pDist;

            this.velocity.alpha = velocityMean;
            this.velocity.beta = velocityVar;
            this.velocity.distribution = vDist;

            this.acceleration.alpha = accelerationMean;
            this.acceleration.beta = accelerationVar;
            this.acceleration.distribution = aDist;

            this.angle.alpha = angleMean;
            this.angle.beta = angleVar;
            this.angle.distribution = angleDist;

            this.angularVelocity.alpha = angVelocityMean;
            this.angularVelocity.beta = angVelocityVar;
            this.angularVelocity.distribution = angVelDist;

            this.color.alpha = colorMean;
            this.color.beta = colorVar;
            this.color.distribution = colDist;

            this.transparency.alpha = alphaMean;
            this.transparency.beta = alphaVar;
            this.transparency.distribution = tranDist;

            this.size.alpha = sizeMean;
            this.size.beta = sizeVar;
            this.size.distribution = sizeDist;

            this.growth.alpha = sizeDeltaMean;
            this.growth.beta = sizeDeltaVar;
            this.growth.distribution = sizeDeltaDist;

            this.ttl.alpha = ttlMean;
            this.ttl.beta = ttlVar;
            this.ttl.distribution = ttlDist;


            this.Location = location;
            this.EmitDimensions = dimension;
            this.MaxNumParticles = maxNumPart;
            this.EmitRate = emitRate;
            this.MeanEmitDelay = emitDelay;
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

            if (position.distribution == Distribution.Normal)
            {
                
                generateLocation = grandom.GetNormalVector3D(
                    new Vector3D(Location.X + position.alpha.X, 
                        Location.Y + position.alpha.Y,
                        Location.Z + position.alpha.Z),
                         
                    position.beta);
            }
            else
            {
                generateLocation = grandom.GetUniformVector3D(
                    new Vector3D(Location.X - position.alpha.X,
                        Location.Y - position.alpha.Y,
                        Location.Z - position.alpha.Z),
                    new Vector3D(Location.X + position.beta.X,
                        Location.Y + position.beta.Y,
                        Location.Z + position.beta.Z));      
            }


            
            return new Particle(this,
                generateLocation,
                grandom.GetRandomVector3D(velocity.distribution, velocity.alpha, velocity.beta),
                grandom.GetRandomVector3D(acceleration.distribution, acceleration.alpha, acceleration.beta),
                grandom.GetRandomDouble(angle.distribution, angle.alpha, angle.beta), 
                grandom.GetRandomDouble(angularVelocity.distribution, angularVelocity.alpha, angularVelocity.beta),
                grandom.GetRandomVector3D(color.distribution, color.alpha, color.beta),
                grandom.GetRandomDouble(transparency.distribution, transparency.alpha, transparency.beta),
                grandom.GetRandomDouble(size.distribution, size.alpha, size.beta),
                grandom.GetRandomDouble(growth.distribution,growth.alpha, growth.beta),
                (int)grandom.GetRandomDouble(ttl.distribution, ttl.alpha, ttl.beta),
                grandom.GetUniformInt(0, NumTextures));

        }

        public void Start()
        {
            EmitActivity = true;
            CurrentLifeTime = 0;
            TimeToNextEmit = MeanEmitDelay;
        }

        /** 
         * Immediately cease to emit particles.
         **/
        public void Stop()
        {
            EmitActivity = false;
        }

        /** 
         * Updates every particle for this emitter.
         **/
        public void Update(int MSSinceLastUpdate /**Milliseconds**/)
        {

            CurrentLifeTime += MSSinceLastUpdate; // Update lifetime timer.
            TimeToNextEmit -= MSSinceLastUpdate; // Update emitrate timer.

            if (CurrentLifeTime > EmitLifetime)
            {
                Stop();
            }
            else
            {
                if ((TimeToNextEmit <= 0) && (EmitActivity))
                {
                    /**
                    * If emitter is still emitting, and it's been
                    * long enough since the last emit, let's make
                    * some more particles.
                    **/
                    TimeToNextEmit = (int)grandom.GetExpDouble((double)MeanEmitDelay);
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

        /**
        * Loads emitter parameters from XML file.
        **/
        public void LoadXMLEmitter(XmlDocument emitter)
        {
            XmlNode emitterPars =
                emitter.SelectSingleNode("/ParticleSystem/EmitterParameters");
            XmlNode particlePars =
                emitter.SelectSingleNode("/ParticleSystem/ParticleParameters");
            /**
             * Load Emitter Parameters:
             **/
            Location = LoadXMLVector3D(emitterPars.SelectSingleNode("location"));
            EmitDimensions = LoadXMLVector3D(emitterPars.SelectSingleNode("dimension"));
            MaxNumParticles =
                Convert.ToInt32(emitterPars.SelectSingleNode("maxNumParticles").
                Attributes.GetNamedItem("x").Value);

            EmitRate =
                Convert.ToInt32(emitterPars.SelectSingleNode("emitRate").
                Attributes.GetNamedItem("x").Value);
            MeanEmitDelay =
                Convert.ToInt32(emitterPars.SelectSingleNode("meanEmitDelay").
                Attributes.GetNamedItem("x").Value);
            EmitLifetime =
                Convert.ToInt32(emitterPars.SelectSingleNode("emitterLifetime").
                Attributes.GetNamedItem("x").Value);
            PermanentParticles =
                Convert.ToBoolean(emitterPars.SelectSingleNode("permanentParticles").
                Attributes.GetNamedItem("x").Value);

            /**
             * Load particle randomization parameters:
             **/
            position = LoadXMLParameter3D(particlePars.SelectSingleNode("position"));
            velocity = LoadXMLParameter3D(particlePars.SelectSingleNode("velocity"));
            acceleration = LoadXMLParameter3D(particlePars.SelectSingleNode("acceleration"));
            color = LoadXMLParameter3D(particlePars.SelectSingleNode("color"));
            angle = LoadXMLParameterDouble(particlePars.SelectSingleNode("angle"));
            angularVelocity = LoadXMLParameterDouble(particlePars.SelectSingleNode("angularVelocity"));
            transparency = LoadXMLParameterDouble(particlePars.SelectSingleNode("transparency"));
            size = LoadXMLParameterDouble(particlePars.SelectSingleNode("size"));
            growth = LoadXMLParameterDouble(particlePars.SelectSingleNode("growth"));
            ttl = LoadXMLParameterDouble(particlePars.SelectSingleNode("ttl"));

        }


        /** 
         * Loads a Double parameter from the specified
         * XMLNode.
         **/
        public ParameterDouble LoadXMLParameterDouble(XmlNode node)
        {
            ParameterDouble newPar = new ParameterDouble();

            newPar.alpha = Convert.ToDouble(node.Attributes.GetNamedItem("alpha").Value);
            newPar.beta = Convert.ToDouble(node.Attributes.GetNamedItem("beta").Value);
            newPar.distribution = (Distribution)Enum.Parse(typeof(Distribution),
                Convert.ToString(node.Attributes.GetNamedItem("distribution").Value));

            return newPar;
        }

        /**
         * Loads a 3D Parameter from the specified XMLNode
         **/
        public Parameter3D LoadXMLParameter3D(XmlNode node)
        {
            Parameter3D newPar = new Parameter3D();

            newPar.alpha = LoadXMLVector3D(node.SelectSingleNode("alpha"));
            newPar.beta = LoadXMLVector3D(node.SelectSingleNode("beta"));
            newPar.distribution = (Distribution)Enum.Parse(typeof(Distribution),
                Convert.ToString(node.SelectSingleNode("distribution").Attributes.GetNamedItem("x").Value));

            return newPar;
        }

        /**
         * Parses and returns a Vector3D from the specified xmlnode.
         **/
        public Vector3D LoadXMLVector3D(XmlNode node)
        {
            Vector3D vec = new Vector3D();

            vec.X = Convert.ToDouble(node.Attributes.GetNamedItem("x").Value);
            vec.Y = Convert.ToDouble(node.Attributes.GetNamedItem("y").Value);
            vec.Z = Convert.ToDouble(node.Attributes.GetNamedItem("z").Value);

            return vec;
        }

       

    }
}
