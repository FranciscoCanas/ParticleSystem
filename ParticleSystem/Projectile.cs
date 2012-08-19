using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GRNG;

namespace ParticleSystem
{
    public class Projectile : Particle
    {
        /**
         * Members
         **/
        List<ParticleEmitter> EmitterList;

        public Projectile(String fileName,
            Vector3D position,
            Vector3D velocity,
            Vector3D acceleration,
            double angle,
            double angVel,
            Vector3D color,
            double trans,
            double transDelta,
            double size,
            double sizeDelta,
            double ttl,
            int txtIndex) : base(null, 
            position,
            velocity,
            acceleration,
            angle,
            angVel,
            color,
            trans,
            transDelta,
            size,
            sizeDelta,
            ttl,
            txtIndex)
        {
            EmitterList = new List<ParticleEmitter>();
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            LoadFromXML(doc);
        }

        

        /**
         * Loads projectile parameters from XmlDoc
         **/
        private void LoadFromXML(XmlDocument projectile)
        {
            foreach (XmlNode emitter in projectile.SelectSingleNode("EmitterList"))
            {
                EmitterList.Add(new ParticleEmitter());
                XmlNode emitterPars = emitter.SelectSingleNode("EmitterParameters");
                XmlNode particlePars = emitter.SelectSingleNode("ParticleParameters");
                EmitterList[EmitterList.Count - 1].LoadXMLEmitterPars(emitterPars);
                EmitterList[EmitterList.Count - 1].LoadXMLParticlePars(particlePars);
            }

        }

        public void Update(double MsSinceLastUpdate)
        {
            /**
             * Update all emitters:
             **/
            foreach (ParticleEmitter emitter in EmitterList)
            {
                emitter.Update(MsSinceLastUpdate);
            }

            /**
             * Update projectile/particle properties:
             **/
            base.Update(MsSinceLastUpdate);
        }
    }
}
