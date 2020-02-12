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
        private BowlingMatch _match;

        [SetUp]
        public void Setup() => _match = new BowlingMatch();

        [TestCase(11)]
        [TestCase(1010)]
        public void AddFrame_GivenARollWithGreaterThan10Knocked_ThrowsInvalidOperationException(int pinsKnocked)
        {
            Assert.Catch(typeof(InvalidOperationException), () => { _match.AddFrame(pinsKnocked, null); });
            Assert.Catch(typeof(InvalidOperationException), () => { _match.AddFrame(0, pinsKnocked); });
            Assert.Catch(typeof(InvalidOperationException), () => { _match.AddFrame(pinsKnocked, pinsKnocked); });
        }

        [Test]
        public void AddFrame_GivenGreaterThan10KnockedOverBothRolls_ThrowsInvalidOperationException()
        {
            Assert.Catch(typeof(InvalidOperationException), () => { _match.AddFrame(10, 1); });
            Assert.Catch(typeof(InvalidOperationException), () => { _match.AddFrame(6, 5); });
            Assert.Catch(typeof(InvalidOperationException), () => { _match.AddFrame(2, 9); });
        }

        [Test]
        public void AddFrame_GivenRoll1WithStrikeAndARoll2_ThrowsInvalidOperationException()
        {
            Assert.Catch(typeof(InvalidOperationException), () => { _match.AddFrame(10, 0); });
        }

        [Test]
        public void Score_AfterSingleRollWith0Knocked_Is0()
        {
            _match.AddFrame(0, null);
            Assert.AreEqual(0, _match.Score);
        }

        [Test]
        public void ScoreRoll_Given0Knocked_Is0()
        {
            _match.AddFrame(0, 0);
            Assert.AreEqual(0, _match.Score);
        }

        [TestCase(1)]
        [TestCase(4)]
        [TestCase(9)]
        public void Score_AfterSingleRollWithLessThan10Knocked_IsAPointPerPinKnocked(int pinsKnocked)
        {
            _match.AddFrame(pinsKnocked, null);
            Assert.AreEqual(pinsKnocked, _match.Score);
        }

        [Test]
        public void Score_AfterSingleRollWithStrike_Is30()
        {
            _match.AddFrame(10, null);
            Assert.AreEqual(30, _match.Score);
        }

        [TestCase(3, 6)]
        [TestCase(4, 5)]
        [TestCase(9, 0)]
        public void Score_AfterOpenFrame_IsCorrect(int roll1PinsKnocked, int? roll2PinsKnocked)
        {
            _match.AddFrame(roll1PinsKnocked, roll2PinsKnocked);
            Assert.AreEqual(roll1PinsKnocked + roll2PinsKnocked, _match.Score);
        }

        [TestCase(3, 7)]
        [TestCase(5, 5)]
        [TestCase(9, 1)]
        public void Score_AfterSpare_IsCorrect(int roll1PinsKnocked, int? roll2PinsKnocked)
        {
            _match.AddFrame(roll1PinsKnocked, roll2PinsKnocked);
            Assert.AreEqual(roll1PinsKnocked + 10, _match.Score);
        }

        [Test]
        public void Score_AtStartOfGame_Is0()
        {
            Assert.AreEqual(0, _match.Score);
        }

        [Test]
        public void AddGame_GivenMoreThan10Frames_ThrowsInvalidOperationException()
        {
            Assert.Catch(typeof(InvalidOperationException), () =>
            {
                _match.AddGame(new (int, int?)[11] { (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), });
            });
        }

        [Test]
        public void AddGame_GivenLessThan10Frames_ThrowsInvalidOperationException()
        {
            Assert.Catch(typeof(InvalidOperationException), () =>
            {
                _match.AddGame(new (int, int?)[9] { (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), });
            });
        }

        [Test]
        public void Score_AfterGameOf1s_Is20()
        {
            var twoKnockedInTwoRolls = (1, 1);
            _match.AddGame(new (int, int?)[10] { twoKnockedInTwoRolls, twoKnockedInTwoRolls, twoKnockedInTwoRolls, twoKnockedInTwoRolls, twoKnockedInTwoRolls, twoKnockedInTwoRolls, twoKnockedInTwoRolls, twoKnockedInTwoRolls, twoKnockedInTwoRolls, twoKnockedInTwoRolls, });

            Assert.AreEqual(20, _match.Score);
        }

        [Test]
        public void Score_AfterGameOfStrikes_Is300()
        {
            (int, int?) strike = (10, null);
            _match.AddGame(new (int, int?)[10] { strike, strike, strike, strike, strike, strike, strike, strike, strike, strike, });

            Assert.AreEqual(300, _match.Score);
        }
    }
}
