// <copyright file="ScoringEngineTests.cs" company="Corsham Science">
// Copyright (c) Corsham Science. All rights reserved.
// </copyright>

namespace Bowling.Tests
{
    using System;
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
        public void ScoreRoll_Given0Knocked_Returns0()
        {
            Assert.AreEqual(0, _engine.ScoreRoll(0));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(6)]
        [TestCase(8)]
        public void ScoreRoll_GivenLessThan10Knocked_ReturnsAPointPerPinKnocked(int pinsKnocked)
        {
            Assert.AreEqual(pinsKnocked, _engine.ScoreRoll(pinsKnocked));
        }

        [TestCase(11)]
        [TestCase(1010)]
        public void ScoreRoll_GivenGreaterThan10Knocked_ThrowsInvalidOperationException(int pinsKnocked)
        {
            Assert.Catch(typeof(InvalidOperationException), () => { _engine.ScoreRoll(pinsKnocked); });
        }

        [Test]
        public void ScoreRoll_GivenStrike_Returns30()
        {
            Assert.AreEqual(30, _engine.ScoreRoll(10));
        }
    }
}
