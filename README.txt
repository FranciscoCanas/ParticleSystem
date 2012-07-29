Particle system framework implemented in C#. 

- Uses only standard library C# (with exception of GRNG, which is on my repository as well)
- Uses GRNG random class for non-uniform random number generation.
- XNAEmitter Wrapper class included under Test Suite.

TODO: 
- Add advanced parameters:
 location generation by emitter location,etc.
- Add more XNA-specific features to wrapper:
 random/varied Texture2D for particles from same emitter
 spriteEffects and drawMode options, etc.
- Make XNA wrapper class permanent part of particle system (take it out of test suite)
- XML-based particle system initialization.
- Full-featured test suite including user input to change emitter parameters.


