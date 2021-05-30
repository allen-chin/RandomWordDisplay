using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RandomWordDisplay.API.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RandomWordDisplay.API.Services
{
    public class RandomWordService : BackgroundService, IRandomWordService
    {
        public const int TotalCommandTime = 5_000;
        private readonly ILogger<RandomWordService> _logger;

        public RandomWordService(ILogger<RandomWordService> logger)
        {
            _logger = logger;
        }

        public bool CommandRunning { get; private set; }
        public int CommandTimeRemaining => _commandTimeRemainingMilliseconds / 1000;
        public string CurrentWordSelected { get; private set; }
        public List<string> WordList { get; private set; }

        private int _commandTimeRemainingMilliseconds;
        private int _millisecondDelay { get; set; }

        public bool Configure(string[] wordList)
        {
            if (CommandRunning)
            {
                return false;
            }

            WordList = wordList.ToList();
            WordList.Shuffle();

            _commandTimeRemainingMilliseconds = WordList?.Count > 0 ? TotalCommandTime : 0;
            _millisecondDelay = WordList?.Count > 0 ? TotalCommandTime / WordList.Count : 1000;
            CommandRunning = true;
            return true;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (!CommandRunning)
                {
                    return;
                }

                if (WordList.Count == 0 || _commandTimeRemainingMilliseconds < _millisecondDelay)
                {
                    _commandTimeRemainingMilliseconds = 0;
                    CommandRunning = false;
                    return;
                }

                if (_commandTimeRemainingMilliseconds > 0)
                {
                    CurrentWordSelected = WordList?[^1] ?? String.Empty;
                    WordList.RemoveAt(WordList.Count - 1);
                    _commandTimeRemainingMilliseconds -= _millisecondDelay;
                }

                await Task.Delay(_millisecondDelay, stoppingToken);
            }
        }
    }
}