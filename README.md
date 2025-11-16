Task Tracker – Local Setup Guide
1. Clone the Repository
Clone the project from the GitHub link provided: https://github.com/meniwatazethi-dotcom/APITaskTracker.git

2. Open and Run the API with Visual Studio
- Open Visual Studio.
- Load the cloned solution.
- Navigate to: TaskTrackerProject > APITaskTracker > Properties
- Open launchSettings.json.
- Copy the SSL port assigned by IIS Express (example: 44370).
Always run the API using IIS Express so the port stays consistent.
3. Configure the Angular Environment File
Go to:
TaskTrackerProject\APITaskTracker\ClientApp\task-tracker\src\environments
Open environment.ts and update apiUrl:
apiUrl: 'https://localhost:/api'
Example:
apiUrl: 'https://localhost:44370/api'
4. Verify the API
If configured correctly, Swagger UI will load with CRUD endpoints.
The backend uses .NET 8 with controller-based APIs (not minimal APIs).
5. Run the Angular 18 Application
Navigate to:
C:\TaskTrackerProject\APITaskTracker\ClientApp\task-tracker
Open Command Prompt in this folder:
cmd
Launch VS Code:
code .
In VS Code terminal:
npm install
Then start Angular:
ng serve --open
