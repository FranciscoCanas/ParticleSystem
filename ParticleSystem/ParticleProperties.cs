using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParticleSystem
{
    public enum ParticleProperties
    {
        /**
         * Position on screen.
         * */
        Position,

        /**
         * Velocity vector3D.
         **/
        Velocity,

        /**
         * Acceleration vector3D.
         **/
        Acceleration,

        /**
         * Rotation angle
         **/
        Angle,

        /**
         * How fast particle rotates.
         **/
        AngularVelocity,

        /**
         * RGB color vector.
         **/
        Color,

        /**
         * Scale at which to draw particle.
         **/
        Size,

        /**
         * Amount by which size changes every update.
         * (multiplicative, not additive)
         **/
        SizeDelta,

        /**
         * Number of updates this particle is active for. 
         **/
        TTL

    }
}
