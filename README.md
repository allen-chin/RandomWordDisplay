# RandomWordDisplay

A web application which displays words in random order, one at a time, with server-side execution.

The application was written in .NET 5 and Angular. Its design is as follows:

1. To display the backend server state, the UI server submits GET requests to the /api/State endpoint every second.
1. Upon entering a list of words and submitting the form, the UI server submits a POST request to the /api/Start endpoint.
1. The API server then starts the singleton service RandomWordService in the background with the wordList as input.
1. After 60 seconds has elapsed, the service is reset to its initial state.

---

## Requirements

* [Angular](https://angular.io/guide/setup-local)
* [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)

## Installation

1. Clone the repository.

   ```sh
   git clone https://github.com/allen-chin/RandomWordDisplay.git
   ```

1. In the RandomWordDisplay directory, start the backend server:

   ```sh
   cd RandomWordDisplay.API
   dotnet run
   ```

1. In the RandomWordDisplay directory, start the frontend server:

   ```sh
   cd RandomWordDisplay.UI
   npm install
   ng serve --open
   ```

## Improvements

Unfortunately, I was not able to get the project at where I wanted it to be due to time constraints (not to mention learning .NET Web API and Angular during the assignment period).

* Use configuration files in the frontend to avoid hard-coding the API URL.
* Use web sockets instead of polling every second with GET requests.
* Use a build automation tool such as Make or psake to run both servers simultaneously.
* Deploy a live demo on Azure via Azure Pipelines or GitHub Actions.
* Create a DB to log command and state changes.
* Create containers for bother servers.
* Generate documentation from code.
