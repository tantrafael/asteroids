# Asteroids

### Ambition
First and foremost I wanted to write a program that’s easy to read and understand, with clean and testable code.

### Physics
With Unity laying the entire PhysX at my feet, I’ve taken the opportunity to use its power in moving things around with rigid-body simulation. With everything wrapping around the screen edges, and most things moving at constant speeds, I decided to mainly use the kinematic body type. Thus I have the stability of physics simulation, running lightly, with the freedom of teleporting when necessary. Only the player ship has the dynamic body type, being accelerated by thrust. For the right feeling of turning quickly, it’s rotation is set explicitly rather than applying torque. I also set the position in the screen wrapping. But it’s done according to the art of minimally disrupting the simulation. I think it’s a good balance.

### Collisions
Asteroids is about things hitting each other. All bodies are equipped with a collider, accompanied by a collision detection component. The latter is responsible for emitting appropriate events depending on the things colliding, all specified in a lookup table.

### Events
An event manager is used. I find it a clean messaging system, letting things stay decoupled.

### Performance
Object pooling would definitely be in place, with all spawning and destroying going on. I haven’t gotten around to it yet though.

### Testing
Oh, the tests I had planned! Ever since attending a lecture on TDD some years ago, I’ve known that it’s the way to go. I did start with setting up a test suite and writing a test. But not sure about having the time to produce a playable game, I renegaded into writing features without tests. At least, I tried to program as testable as I know how to. Add ing tests is my absolute priority at this point.

### Controls
Press enter to start and restart game. Use arrow keys to move. Press space to fire.
