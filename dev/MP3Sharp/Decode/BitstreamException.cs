// /***************************************************************************
//  * BitstreamException.cs
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
using MP3Sharp.Decode;

namespace MP3Sharp
{
    /// <summary>
    ///     Instances of <code>BitstreamException</code> are thrown
    ///     when operations on a <code>Bitstream</code> fail.
    ///     <p>
    ///         The exception provides details of the exception condition
    ///         in two ways:
    ///         <ol>
    ///             <li>
    ///                 as an error-code describing the nature of the error
    ///             </li>
    ///             <br></br>
    ///             <li>
    ///                 as the <code>Throwable</code> instance, if any, that was thrown
    ///                 indicating that an exceptional condition has occurred.
    ///             </li>
    ///         </ol>
    ///     </p>
    ///     @since 0.0.6
    /// </summary>
    /// <author>
    ///     MDM	12/12/99
    /// </author>
    public class BitstreamException : Mp3SharpException, BitstreamErrors
    {
        private int errorcode;

        public BitstreamException(string msg, Exception t) : base(msg, t)
        {
            InitBlock();
        }

        public BitstreamException(int errorcode, Exception t) : this(getErrorString(errorcode), t)
        {
            InitBlock();
            this.errorcode = errorcode;
        }

        public virtual int ErrorCode
        {
            get { return errorcode; }
        }

        private void InitBlock()
        {
            errorcode = BitstreamErrors_Fields.UNKNOWN_ERROR;
        }

        public static string getErrorString(int errorcode)
        {
            // REVIEW: use resource bundle to map error codes
            // to locale-sensitive strings.

            return "Bitstream errorcode " + System.Convert.ToString(errorcode, 16);
        }
    }
}