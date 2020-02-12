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

        [TestCase(11)]
        [TestCase(1010)]
        public void ScoreFrame_GivenARollWithGreaterThan10Knocked_ThrowsInvalidOperationException(int pinsKnocked)
        {
            Assert.Catch(typeof(InvalidOperationException), () => { _engine.ScoreFrame(pinsKnocked, null); });
            Assert.Catch(typeof(InvalidOperationException), () => { _engine.ScoreFrame(0, pinsKnocked); });
            Assert.Catch(typeof(InvalidOperationException), () => { _engine.ScoreFrame(pinsKnocked, pinsKnocked); });
        }

        [Test]
        public void ScoreFrame_GivenGreaterThan10KnockedOverBothRolls_ThrowsInvalidOperationException()
        {
            Assert.Catch(typeof(InvalidOperationException), () => { _engine.ScoreFrame(10, 1); });
            Assert.Catch(typeof(InvalidOperationException), () => { _engine.ScoreFrame(6, 5); });
            Assert.Catch(typeof(InvalidOperationException), () => { _engine.ScoreFrame(2, 9); });
        }

        [Test]
        public void ScoreFrame_GivenRoll1WithStrikeAndARoll2_ThrowsInvalidOperationException()
        {
            Assert.Catch(typeof(InvalidOperationException), () => { _engine.ScoreFrame(10, 0); });
        }

        [Test]
        public void ScoreRoll_GivenSingleRollWith0Knocked_Returns0()
        {
            Assert.AreEqual(0, _engine.ScoreFrame(0, null));
        }

        [Test]
        public void ScoreRoll_Given0Knocked_Returns0()
        {
            Assert.AreEqual(0, _engine.ScoreFrame(0, 0));
        }

        [TestCase(1)]
        [TestCase(4)]
        [TestCase(9)]
        public void ScoreFrame_GivenSingleRollWithLessThan10Knocked_ReturnsAPointPerPinKnocked(int pinsKnocked)
        {
            Assert.AreEqual(pinsKnocked, _engine.ScoreFrame(pinsKnocked, null));
        }

        [Test]
        public void ScoreFrame_GivenSingleRollWithStrike_Returns30()
        {
            Assert.AreEqual(30, _engine.ScoreFrame(10, null));
        }

        [TestCase(3, 6)]
        [TestCase(4, 5)]
        [TestCase(9, 0)]
        public void ScoreFrame_GivenOpen_ReturnsCorrectScore(int roll1PinsKnocked, int? roll2PinsKnocked)
        {
            Assert.AreEqual(roll1PinsKnocked + roll2PinsKnocked, _engine.ScoreFrame(roll1PinsKnocked, roll2PinsKnocked));
        }

        [TestCase(3, 7)]
        [TestCase(5, 5)]
        [TestCase(9, 1)]
        public void ScoreFrame_GivenSpare_ReturnsCorrectScore(int roll1PinsKnocked, int? roll2PinsKnocked)
        {
            Assert.AreEqual(roll1PinsKnocked + 10, _engine.ScoreFrame(roll1PinsKnocked, roll2PinsKnocked));
        }
    }
}
