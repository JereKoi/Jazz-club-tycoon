/* If running into object was not set to an instance of object, you have to do instance setting on awake because start method might be too fast
and thus not be able to load it

Singleton = global class that can only appear once in the scene. Generally used if want to manage events in the game and ensure code is easily communicated.
            Only one instance of class that any script can reference to. Singletons give global access to an instance and is rarely wise decision, but in
            game dev it is needed.

Events are more professional and better way than instance use

OnLoadDb?.Invoke(); is how you use event inside function, it prevents null reference errors if nothing is subscribed to that event

Additional info can be passed to events with parameters; public static Action<int> xyz;

*/
