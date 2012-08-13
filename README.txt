Particle system framework implemented in C#. 

- Needs only standard library C# (with exception of GRNG, which is on my repository as well)
- Particle systems can be loaded directly from XML paramater files.
- Uses GRNG random class for non-uniform random number generation.
- Basic support for varying global particle levels (high/low/medium, etc)
- XNAEmitter Wrapper class included under Test Suite for XNA 4.0 support.
- Basic XNA test suite and a few sample XML particle parameter files for experimenting.
- Support for XNA Blendmodes
- Support for XML files to load particle parameters


TODO: 
- Add more global parameters (gravity, friction coefficients, wind, etc)
- Add option for direction and magnitude based particle generation (instead of
x,y,z component vector movement)



