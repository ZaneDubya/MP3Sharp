// /***************************************************************************
//  * DecoderException.cs
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

namespace MP3Sharp.Decode
{
    /// <summary>
    ///     The <code>DecoderException</code> represents the class of
    ///     errors that can occur when decoding MPEG audio.
    /// </summary>
    internal class DecoderException : Mp3SharpException, DecoderErrors
    {
        private int m_ErrorCode;

        public DecoderException(string msg, Exception t) : base(msg, t)
        {
            InitBlock();
        }

        public DecoderException(int errorcode, Exception t) : this(GetErrorString(errorcode), t)
        {
            InitBlock();
            m_ErrorCode = errorcode;
        }

        public virtual int ErrorCode
        {
            get { return m_ErrorCode; }
        }

        private void InitBlock()
        {
            m_ErrorCode = DecoderErrors_Fields.UNKNOWN_ERROR;
        }

        public static string GetErrorString(int errorcode)
        {
            // REVIEW: use resource file to map error codes
            // to locale-sensitive strings. 

            return "Decoder errorcode " + System.Convert.ToString(errorcode, 16);
        }
    }
}