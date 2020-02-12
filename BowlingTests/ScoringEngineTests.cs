// <copyright file="ScoringEngineTests.cs" company="Corsham Science">
// Copyright (c) Corsham Science. All rights reserved.
// </copyright>

namespace Bowling.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class BowlingMatchTests
    {
        private BowlingMatch _match;

        [SetUp]
        public void Setup() => _match = new BowlingMatch();

        [TestCase(11)]
        [TestCase(1010)]
        public void AddRoll_GivenGreaterThan10Knocked_ThrowsInvalidOperationException(int pinsKnocked)
        {
            Assert.Catch(typeof(InvalidOperationException), () => { _match.AddRoll(pinsKnocked); });
        }

        [Test]
        public void AddRoll_GivenNegativeKnocked_ThrowsInvalidOperationException()
        {
            Assert.Catch(typeof(InvalidOperationException), () => { _match.AddRoll(-1); });
        }

        [TestCase(6, 5)]
        [TestCase(2, 9)]
        public void AddingAFrame_GivenKnockedGreaterThan10OverBothRolls_ThrowsInvalidOperationException(int roll1PinsKnocked, int roll2PinsKnocked)
        {
            Assert.Catch(typeof(InvalidOperationException), () =>
            {
                _match.AddRoll(roll1PinsKnocked);
                _match.AddRoll(roll2PinsKnocked);
            });
        }

        [Test]
        public void Score_AtStartOfGame_Is0()
        {
            Assert.AreEqual(0, _match.Score);
        }

        [Test]
        public void Score_AfterFrameWith0Knocked_Is0()
        {
            _match.AddRoll(0);
            _match.AddRoll(0);
            Assert.AreEqual(0, _match.Score);
        }

        [Test]
        public void Score_AfterFrameWith1Knocked_Is1()
        {
            _match.AddRoll(0);
            _match.AddRoll(1);
            Assert.AreEqual(1, _match.Score);
        }

        [TestCase(1)]
        [TestCase(4)]
        [TestCase(9)]
        public void Score_AfterSingleRollWithLessThan10Knocked_IsAPointPerPinKnocked(int pinsKnocked)
        {
            _match.AddRoll(pinsKnocked);
            Assert.AreEqual(pinsKnocked, _match.Score);
        }

        [Test]
        public void Score_AfterSingleRollStrike_Is30()
        {
            _match.AddRoll(10);
            Assert.AreEqual(30, _match.Score);
        }

        [Test]
        public void Score_AfterStrikeOnSecondRoll_Is30()
        {
            _match.AddRoll(0);
            _match.AddRoll(10);
            Assert.AreEqual(30, _match.Score);
        }

        [TestCase(3, 6)]
        [TestCase(4, 5)]
        [TestCase(9, 0)]
        public void Score_AfterOpenFrame_IsCorrect(int roll1PinsKnocked, int roll2PinsKnocked)
        {
            _match.AddRoll(roll1PinsKnocked);
            _match.AddRoll(roll2PinsKnocked);
            Assert.AreEqual(roll1PinsKnocked + roll2PinsKnocked, _match.Score);
        }

        [TestCase(3, 7)]
        [TestCase(5, 5)]
        [TestCase(9, 1)]
        public void Score_AfterSpare_IsCorrect(int roll1PinsKnocked, int roll2PinsKnocked)
        {
            _match.AddRoll(roll1PinsKnocked);
            _match.AddRoll(roll2PinsKnocked);
            Assert.AreEqual(roll1PinsKnocked + 10, _match.Score);
        }

        [Test]
        public void Score_AfterSeveralRolls_IsCorrect()
        {
            _match.AddRoll(10);
            _match.AddRoll(3);
            _match.AddRoll(3);
            _match.AddRoll(10);
            Assert.AreEqual(66, _match.Score);
        }

        [Test]
        public void AddGame_GivenMoreThan20Rolls_ThrowsInvalidOperationException()
        {
            Assert.Catch(typeof(InvalidOperationException), () => { _match.AddGame(new int?[21] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, }); });
        }

        [Test]
        public void AddGame_GivenLessThan20Rolls_ThrowsInvalidOperationException()
        {
            Assert.Catch(typeof(InvalidOperationException), () => { _match.AddGame(new int?[19] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, }); });
        }

        [Test]
        public void Score_AfterGameOf1s_Is20()
        {
            _match.AddGame(new int?[20] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, });

            Assert.AreEqual(20, _match.Score);
        }

        [Test]
        public void Score_AfterGameOfStrikes_Is300()
        {
            _match.AddGame(new int?[20] { 10, null, 10, null, 10, null, 10, null, 10, null, 10, null, 10, null, 10, null, 10, null, 10, null, });

            Assert.AreEqual(300, _match.Score);
        }
    }
}
