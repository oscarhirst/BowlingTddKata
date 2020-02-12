// <copyright file="ScoringEngine.cs" company="Corsham Science">
// Copyright (c) Corsham Science. All rights reserved.
// </copyright>
// sln based on exercise http://www.peterprovost.org/blog/2012/05/02/kata-the-only-way-to-learn-tdd/
// using World Bowling Scoring

namespace Bowling
{
    using System;

    public class ScoringEngine
    {
        public int ScoreRoll(int pinsKnocked)
        {
            if (pinsKnocked > 10)
            {
                throw new InvalidOperationException();
            }

            return pinsKnocked == 10 ? 30 : pinsKnocked;
        }
    }
}
