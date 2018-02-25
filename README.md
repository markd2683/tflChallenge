# tflChallenge

tflChallenge is a console application written in C# using Visual Studio 2017 community edition.  The application provides the status of major roads using the TFL REST API.

The application is written using .NET Framework version 4.6.1, therefore Visual Studio 2015 or later is recommended to build the application.

## How to Build

1.	Clone the tflChallenge repository
2.	Open the TflChallenge solution in Visual Studio
3.	In the Solution Explorer right click the solution name and select “Restore NuGet Packages”
4.	Open the App.config file and update the values for `app_id` and `app_key` with your API credentials and save changes.
5.	Press F6 or in the solution explorer right click on the solution name and select “Build Solution”

## How to Run

Once the solution has been built:
1.	Open Windows Command Prompt
2.	Change directory to the Debug folder of the tflChallenge local repository (e.g. `cd C:\repos\tflChallenge\TflChallenge\bin\debug`)
3.	Enter the executable name followed by a valid major road name e.g. `TflChallenge.exe A2` (road IDs that contain spaces should be enclosed in quotes e.g. `TflChallenge.exe “blackwall tunnel”`

## Exit Codes

|                       | Exit Code | Description                                   |
| --------------------- |-----------| --------------------------------------------- |
| Success               | 0         | The operation completed successfully              |
| Error_InvalidArgs     | 1         | Either none or >1 argument had been provided      |
| Error_EntityNotFound  | 2         | A road could not be found with the ID provided    |
|Error_UnexpectedError  | 3         | An unexpected error occurred                      |

## Running Tests
Once the solution has been built:
1.	Open the Test Explorer window in Visual Studio from the “Test> Windows>Test Explorer” (or using the shortcut Ctrl+E,T)
2.	In the Test Explorer window click “Run All” (or using the shortcut Ctrl+R,A)

## Assumptions
 - The only success response code returned by the TFL API is `200 OK`.  Therefore the RoadService is only checking for a success status code and not 200 specifically i.e. 
 
    ```csharp
    if (response.IsSuccessStatusCode) 
    {
         ...
    }
    ```