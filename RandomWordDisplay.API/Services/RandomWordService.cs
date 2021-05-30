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
    /// <summary>
    /// A service that selects a random word from the given list of words and rotates through the list in a 60 second cycle. 
    /// </summary>
    public class RandomWordService : BackgroundService, IRandomWordService
    {
        /// <summary>
        /// Total time for a command in milliseconds.
        /// </summary>
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

        /// <summary>
        /// Calculated milliseconds of delay between each execution in ExecuteAsync.
        /// </summary>
        private int _millisecondDelay { get; set; }

        /// <summary>
        /// This method configures the service by resetting the stopwatch, shuffling the new word list, and computing the shared time for each word.
        /// </summary>
        public bool Configure(string[] wordList)
        {
            if (CommandRunning || wordList is null || wordList.Length == 0)
            {
                return false;
            }

            WordList = wordList.ToList();
            WordList.Shuffle();

            _millisecondDelay = TotalCommandTime / WordList.Count;
            CommandRunning = true;
            return true;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _stopwatch.Restart();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (!CommandRunning || WordList.Count == 0 || _stopwatch.ElapsedMilliseconds > TotalCommandTime)
                {
                    break;
                }

                if (_stopwatch.ElapsedMilliseconds + _millisecondDelay < TotalCommandTime)
                {
                    CurrentWordSelected = WordList?[^1] ?? String.Empty;
                    WordList.RemoveAt(WordList.Count - 1);
                }

                await Task.Delay(_millisecondDelay, stoppingToken);
            }

            CurrentWordSelected = "";
            CommandRunning = false;
            _stopwatch.Stop();
        }
    }
}