// /***************************************************************************
//  * JavaLayerException.cs
//  * Copyright (c) 2015 the authors.
//  * 
//  * All rights reserved. This program and the accompanying materials
//  * are made available under the terms of the GNU Lesser General Public License
//  * (LGPL) version 3 which accompanies this distribution, and is available at
//  * https://www.gnu.org/licenses/lgpl-3.0.en.html
//  *
//  * This library is distributed in the hope that it will be useful,
//  * but WITHOUT ANY WARRANTY; without even the implied warranty of
//  * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//  * Lesser General Public License for more details.
//  *
//  ***************************************************************************/

using System;
using MP3Sharp.Support;

namespace MP3Sharp
{
    /// <summary>
    ///     MP3SharpException is the base class for all API-level
    ///     exceptions thrown by JavaLayer. To facilitate conversion and
    ///     common handling of exceptions from other domains, the class
    ///     can delegate some functionality to a contained Throwable instance.
    /// </summary>
    public class MP3SharpException : Exception
    {
        private readonly Exception exception;

        public MP3SharpException()
        {
        }

        public MP3SharpException(string msg) : base(msg)
        {
        }

        public MP3SharpException(string msg, Exception t) : base(msg)
        {
            exception = t;
        }

        public virtual Exception Exception
        {
            get { return exception; }
        }

        public void PrintStackTrace()
        {
            SupportClass.WriteStackTrace(this, Console.Error);
        }

        public void PrintStackTrace(System.IO.StreamWriter ps)
        {
            if (exception == null)
            {
                SupportClass.WriteStackTrace(this, ps);
            }
            else
            {
                SupportClass.WriteStackTrace(exception, Console.Error);
            }
        }
    }
}