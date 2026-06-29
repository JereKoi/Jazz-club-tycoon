/* If running into object was not set to an instance of object, you have to do instance setting on awake because start method might be too fast
and thus not be able to load it

Singleton = global class that can only appear once in the scene. Generally used if want to manage events in the game and ensure code is easily communicated.
            Only one instance of class that any script can reference to. Singletons give global access to an instance and is rarely wise decision, but in
            game dev it is needed.

Events are more professional and better way than instance use

OnLoadDb?.Invoke(); is how you use event inside function, it prevents null reference errors if nothing is subscribed to that event

Additional info can be passed to events with parameters; public static Action<int> xyz;


About instances:
If you are referencing script on another script with instance, make sure referenced script is on scene and
make sure it's awake function has been ran. Exception is that if script does not inherit from MonoBehavior,
it can be referenced as pure C# object.

Life cycle:
Unity's script life cycle goes through all Awake methods first and after that it goes to start.
If you are using instance definition on start method, it is undetermined which script starts up first.
This results to nullreferenceExceptions.

Debugging:
Remember to write Debug.LogErrors so that you can understand instantly whats is the issue. Unity's generic
nullReferenceException gives row and tells that something is null. It is super important to understand
problem right away.

Another tip is to use returns so that code does not run if it fails or runs into an error.

About update:
Dont run code on update just like that since it runs every frame. Instead of calling scripts there
you can use timer (Time.DeltaTime) or create event based code. (Code is called only when something
really happens.)

*/
