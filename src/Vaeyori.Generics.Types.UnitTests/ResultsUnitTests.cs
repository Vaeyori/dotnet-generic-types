/*
*    Copyright (C) 2021 Joshua "ysovuka" Thompson
*
*    This program is free software: you can redistribute it and/or modify
*    it under the terms of the GNU Affero General Public License as published
*    by the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*
*    This program is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU Affero General Public License for more details.
*
*    You should have received a copy of the GNU Affero General Public License
*    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

namespace Vaeyori.Generics.Types.UnitTests
{
    using System;
    using Xunit;

    public class ResultsUnitTests
    {
        [Fact]
        public void ResultT1_Constructor_SuccessfullyCreatedWithOk()
        {
            var result = new Result<string>("test");

            Assert.NotNull(result.Match((x) => x, (x) => null));
        }

        [Fact]
        public void ResultT1_Constructor_SuccessfullyCreatedWithException()
        {
            var result = new Result<string>(new Exception());

            Assert.NotNull(result.Match((x) => null, (x) => x));
        }

        [Fact]
        public void ResultT1T2_Constructor_SuccessfullyCreatedWithOk()
        {
            var result = new Result<string, Exception>("test");

            Assert.NotNull(result.Match((x) => x, (x) => null));
        }

        [Fact]
        public void ResultT1T2_Constructor_SuccessfullyCreatedWithException()
        {
            var result = new Result<string, Exception>(new Exception());

            Assert.NotNull(result.Match((x) => null, (x) => x));
        }

        [Fact]
        public void ResultExtensionMethod_Ok_SuccessfullyCreatesResultT1WithOk()
        {
            Result<string> result = Result.Ok("test");

            Assert.NotNull(result.Match((x) => x, (x) => null));
        }

        [Fact]
        public void ResultExtensionMethod_Exception_SuccessfullyCreatesResultT1WithException()
        {
            Result<string> result = Result.Exception(new Exception());

            Assert.NotNull(result.Match((x) => null, (x) => x));
        }

        [Fact]
        public void ResultExtensionMethod_Ok_SuccessfullyCreatesResultT1T2WithOk()
        {
            Result<string, Exception> result = Result.Ok("test");

            Assert.NotNull(result.Match((x) => x, (x) => null));
        }

        [Fact]
        public void ResultExtensionMethod_Exception_SuccessfullyCreatesResultT1T2WithException()
        {
            Result<string, Exception> result = Result.Exception(new Exception());

            Assert.NotNull(result.Match((x) => null, (x) => x));
        }

        [Fact]
        public void ResultT1_Ok_SuccessfullyConvertedResultT1T2()
        {
            var resultT1T2 = new Result<string, Exception>("test");
            Result<string> result = resultT1T2;

            Assert.NotNull(result.Match((x) => x, (x) => null));
        }

        [Fact]
        public void ResultT1_Exception_SuccessfullyConvertedResultT1T2()
        {
            var resultT1T2 = new Result<string, Exception>(new Exception());
            Result<string> result = resultT1T2;

            Assert.NotNull(result.Match((x) => null, (x) => x));
        }
    }
}
