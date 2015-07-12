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

#region usings

using System;
using MP3Sharp.Support;

#endregion

/*
* 12/12/99		Initial version.	mdm@techie.com
/*-----------------------------------------------------------------------
*  This program is free software; you can redistribute it and/or modify
*  it under the terms of the GNU General Public License as published by
*  the Free Software Foundation; either version 2 of the License, or
*  (at your option) any later version.
*
*  This program is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*  GNU General Public License for more details.
*
*  You should have received a copy of the GNU General Public License
*  along with this program; if not, write to the Free Software
*  Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
*----------------------------------------------------------------------
*/

namespace MP3Sharp
{
    /// <summary>
    ///     The Mp3SharpException is the base class for all API-level
    ///     exceptions thrown by JavaLayer. To facilitate conversion and
    ///     common handling of exceptions from other domains, the class
    ///     can delegate some functionality to a contained Throwable instance.
    ///     <p>
    /// </summary>
    /// <author>
    ///     MDM
    /// </author>
    public class Mp3SharpException : Exception
    {
        private readonly Exception exception;

        public Mp3SharpException()
        {
        }

        public Mp3SharpException(string msg) : base(msg)
        {
        }

        public Mp3SharpException(string msg, Exception t) : base(msg)
        {
            exception = t;
        }

        public virtual Exception Exception
        {
            get { return exception; }
        }

        //UPGRADE_TODO: The equivalent of method 'java.lang.Throwable.printStackTrace' is not an override method. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1143"'
        public void printStackTrace()
        {
            SupportClass.WriteStackTrace(this, Console.Error);
        }

        //UPGRADE_TODO: The equivalent of method 'java.lang.Throwable.printStackTrace' is not an override method. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1143"'
        public void printStackTrace(System.IO.StreamWriter ps)
        {
            if (exception == null)
            {
                SupportClass.WriteStackTrace((Exception) this, ps);
            }
            else
            {
                SupportClass.WriteStackTrace(exception, Console.Error);
            }
        }
    }
}