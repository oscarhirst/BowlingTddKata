﻿// <copyright file="BowlingMatch.cs" company="Corsham Science">
// Copyright (c) Corsham Science. All rights reserved.
// </copyright>
// sln based on exercise http://www.peterprovost.org/blog/2012/05/02/kata-the-only-way-to-learn-tdd/
// using World Bowling Scoring

namespace Bowling
{
    using System;

    public class BowlingMatch
    {
        private const int TotalPins = 10;

        public BowlingMatch()
        {
            Score = 0;
        }

        public int Score { get; private set; }

        public void AddFrame(int roll1PinsKnocked, int? roll2PinsKnocked)
            => Score += ScoreFrame(roll1PinsKnocked, roll2PinsKnocked);

        private int ScoreFrame(int roll1PinsKnocked, int? roll2PinsKnocked)
        {
            var totalKnocked = roll1PinsKnocked + (roll2PinsKnocked ?? 0);

            if (totalKnocked > TotalPins || (roll1PinsKnocked == 10 && roll2PinsKnocked != null))
            {
                throw new InvalidOperationException();
            }

            if (roll1PinsKnocked == TotalPins)
            {
                return 30;
            }

            if (totalKnocked == TotalPins)
            {
                return TotalPins + roll1PinsKnocked;
            }

            return totalKnocked;
        }
    }
}