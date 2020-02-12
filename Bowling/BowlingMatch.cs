// <copyright file="BowlingMatch.cs" company="Corsham Science">
// Copyright (c) Corsham Science. All rights reserved.
// </copyright>
// sln based on exercise http://www.peterprovost.org/blog/2012/05/02/kata-the-only-way-to-learn-tdd/
// using World Bowling Scoring https://en.wikipedia.org/wiki/Ten-pin_bowling#World_Bowling_scoring

namespace Bowling
{
    using System;

    public class BowlingMatch
    {
        private const int TotalPins = 10;
        private int? _pinsKnockedInFirstRoll;

        public BowlingMatch()
        {
            Score = 0;
            _pinsKnockedInFirstRoll = null;
        }

        public int Score { get; private set; }

        public void AddGame(int?[] rolls)
        {
            if (rolls.Length != 20)
            {
                throw new InvalidOperationException();
            }

            foreach (var roll in rolls)
            {
                AddRoll(roll);
            }
        }

        public void AddRoll(int? pinsKnocked)
        {
            if (pinsKnocked != null)
            {
                if (pinsKnocked < 0 || pinsKnocked > TotalPins)
                {
                    throw new InvalidOperationException();
                }

                if (_pinsKnockedInFirstRoll == null)
                {
                    if (pinsKnocked == TotalPins)
                    {
                        AddFrame(pinsKnocked.Value, null);
                        return;
                    }

                    Score += pinsKnocked.Value;
                    _pinsKnockedInFirstRoll = pinsKnocked;
                    return;
                }

                Score -= _pinsKnockedInFirstRoll.Value;

                AddFrame(_pinsKnockedInFirstRoll.Value, pinsKnocked);
            }
        }

        private void AddFrame(int roll1PinsKnocked, int? roll2PinsKnocked)
        {
            if (roll1PinsKnocked == 10 && roll2PinsKnocked != null)
            {
                throw new InvalidOperationException();
            }

            var totalKnocked = roll1PinsKnocked + (roll2PinsKnocked ?? 0);
            if (totalKnocked > TotalPins)
            {
                throw new InvalidOperationException();
            }

            if (roll1PinsKnocked == TotalPins || roll2PinsKnocked == TotalPins)
            {
                Score += 30;
            }
            else if (totalKnocked == TotalPins)
            {
                Score += TotalPins + roll1PinsKnocked;
            }
            else
            {
                Score += totalKnocked;
            }

            _pinsKnockedInFirstRoll = null;
        }
    }
}
