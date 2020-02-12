// <copyright file="ScoringEngineTests.cs" company="Corsham Science">
// Copyright (c) Corsham Science. All rights reserved.
// </copyright>

namespace Bowling.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ScoringEngineTests
    {
        private ScoringEngine _engine;

        [OneTimeSetUp]
        public void InitialSetup()
        {
            _engine = new ScoringEngine();
        }

        [Test]
        public void ScoreRound_Given0Knocked_Returns0()
        {
            Assert.AreEqual(0, _engine.ScoreRound(0));
        }
    }
}
