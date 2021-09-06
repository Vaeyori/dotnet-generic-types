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

namespace Vaeyori.Generics.Types
{
    using System;

    public static class Result
    {
        public static DelayedOk<TOk> Ok<TOk>(TOk ok) =>
            new(ok);

        public static DelayedException<TException> Exception<TException>(TException exception) =>
            new(exception);
    }

    public readonly struct Result<TOk>
    {
        private readonly TOk _ok;
        private readonly Exception _exception;
        private readonly bool _isException;

        public Result(TOk ok) : this(ok, default, false) { }
        public Result(Exception exception) : this(default, exception, true) { }

        private Result(TOk ok, Exception exception, bool isException)
        {
            _ok = ok;
            _exception = exception;
            _isException = isException;
        }

        public T Match<T>(Func<TOk, T> ok, Func<Exception, T> exception) =>
            _isException ? exception(_exception) : ok(_ok);


        public static implicit operator Result<TOk>(DelayedOk<TOk> ok) =>
            new(ok.Value);

        public static implicit operator Result<TOk>(DelayedException<Exception> exception) =>
            new(exception.Value);

        public static implicit operator Result<TOk>(Result<TOk, Exception> result) =>
            result.Match<Result<TOk>>((x) => new(x), (x) => new(x));
    }

    public readonly struct Result<TOk, TException>
    {
        private readonly TOk _ok;
        private readonly TException _exception;
        private readonly bool _isException;

        public Result(TOk ok) : this(ok, default) { }
        public Result(TException exception) : this(default, exception) { }

        private Result(TOk ok, TException exception)
        {
            _ok = ok;
            _exception = exception;
            _isException = exception is object;
        }

        public T Match<T>(Func<TOk, T> ok, Func<TException, T> exception) =>
            _isException ? exception(_exception) : ok(_ok);

        public static implicit operator Result<TOk, TException>(DelayedOk<TOk> ok) =>
            new(ok.Value);

        public static implicit operator Result<TOk, TException>(DelayedException<TException> exception) =>
            new(exception.Value);
    }
}
