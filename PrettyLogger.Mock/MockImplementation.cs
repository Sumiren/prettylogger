﻿using System.Linq;
using NUnit.Framework;

namespace PrettyLogger.Mock
{
    public class MockImplementation : InmemoryImplementation
    {
        public string LastLogged => Logs.Last();

        public void AssertLoggedOnlyOnce(string expected)
        {
            Assert.That(Logs.Count,  Is.EqualTo(1));
            Assert.That(Logs.Last(), Is.EqualTo(expected));
        }

        public void AssertNotLogged()
        {
            Assert.That(Logs.Count, Is.Zero);
        }
    }
}