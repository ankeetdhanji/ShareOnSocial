using System;
using System.Linq;
using FakeItEasy;

namespace ShareOnSocialsLib.UnitTests.TestHelpers
{
    /// <summary>
    /// Fake factory.
    /// </summary>
    public static class FakeFactory
    {
        /// <summary>
        /// Finds the required fake oject from the list supplied or creates it.
        /// </summary>
        /// <returns>The fake required</returns>
        /// <param name="fakes">Fakes.</param>
        /// <typeparam name="T">The fake required.</typeparam>
        public static T FindOrCreate<T>(params object[] fakes)
        {
            T fake = (T)fakes.FirstOrDefault(f => f is T);
            if (fake == null)
            {
                fake = A.Fake<T>();
            }

            return fake;
        }
    }
}
