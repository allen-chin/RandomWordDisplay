using Microsoft.Extensions.Hosting;

namespace RandomWordDisplay.API.Services
{
    public interface IRandomWordService : IHostedService
    {
        public bool CommandRunning { get; }

        public int CommandTimeRemaining { get; }

        public string CurrentWordSelected { get; }

        public bool Configure(string[] wordList);
    }
}