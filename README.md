# Sandbox.System
Sandbox API wrappings for C#'s standard libraries.  
This library should be used to port existing C# libraries to S&Box with minimal effort by just replacing `using System.*;` with `using Sandbox.System.*;`. See the notes bellow for more details.

# Wrapped classes and limiatations
* System
    * Console
        * WriteLine and Console.Error only (TextWriter is currently not whitelisted)
* System.Net.Http
    * HttpClient  
        * Only GET requests
        * No GetAsync
        * No cancellation tokens
* System.IO  
    **Note:** Some classes from System.IO are whitelisted, you may need to use aliases, e.g. `using Stream = System.IO.Stream;`
    * File, Path, Directory
        * Above classes need .Mounted/Data/OrganizationData depending on use
        * No FileInfo, DirectoryInfo
        * CreationTime, AccessTime return DateTime.(Utc)Now, cannot be set
* System.Diagnostics
    * Trace
        * Only WriteLine