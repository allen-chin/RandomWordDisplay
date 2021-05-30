using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RandomWordDisplay.API.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RandomWordDisplay.API.Services
{
    public class RandomWordService : BackgroundService, IRandomWordService
    {
        public const int TotalCommandTime = 60_000;
        private readonly ILogger<RandomWordService> _logger;
        private readonly Stopwatch _stopwatch = new();

        public RandomWordService(ILogger<RandomWordService> logger)
        {
            _logger = logger;
        }

        public bool CommandRunning { get; private set; }
        public int CommandTimeRemaining => (int)Math.Max(TotalCommandTime - _stopwatch.ElapsedMilliseconds, 0) / 1000;
        public string CurrentWordSelected { get; private set; }
        public List<string> WordList { get; private set; }

        private int _commandTimeRemainingMilliseconds;
        private int _millisecondDelay { get; set; }

        public bool Configure(string[] wordList)
        {
            if (CommandRunning || wordList is null || wordList.Length == 0)
            {
                return false;
            }

            WordList = wordList.ToList();
            WordList.Shuffle();

            _commandTimeRemainingMilliseconds = TotalCommandTime;
            _millisecondDelay = TotalCommandTime / WordList.Count;
            CommandRunning = true;
            return true;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _stopwatch.Restart();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (!CommandRunning)
                {
                    break;
                }

                if (WordList.Count == 0 || _stopwatch.ElapsedMilliseconds > TotalCommandTime)
                {
                    break;
                }

                if (_commandTimeRemainingMilliseconds > 0)
                {
                    CurrentWordSelected = WordList?[^1] ?? String.Empty;
                    WordList.RemoveAt(WordList.Count - 1);
                    _commandTimeRemainingMilliseconds -= _millisecondDelay;
                }

                await Task.Delay(_millisecondDelay, stoppingToken);
            }

            _commandTimeRemainingMilliseconds = 0;
            CommandRunning = false;
            _stopwatch.Stop();
        }
    }
}