// /***************************************************************************
//  * BitstreamErrors.cs
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

namespace MP3Sharp.Decode
{
    /// <summary>
    ///     This interface describes all error codes that can be thrown
    ///     in <code>BistreamException</code>s.
    /// </summary>
    internal struct BitstreamErrors_Fields
    {
        public static readonly int UNKNOWN_ERROR;
        public static readonly int UNKNOWN_SAMPLE_RATE;
        public static readonly int STREAM_ERROR;
        public static readonly int UNEXPECTED_EOF;
        public static readonly int STREAM_EOF;
        public static readonly int BITSTREAM_LAST = 0x1ff;

        static BitstreamErrors_Fields()
        {
            UNKNOWN_ERROR = JavaLayerErrors_Fields.BITSTREAM_ERROR + 0;
            UNKNOWN_SAMPLE_RATE = JavaLayerErrors_Fields.BITSTREAM_ERROR + 1;
            STREAM_ERROR = JavaLayerErrors_Fields.BITSTREAM_ERROR + 2;
            UNEXPECTED_EOF = JavaLayerErrors_Fields.BITSTREAM_ERROR + 3;
            STREAM_EOF = JavaLayerErrors_Fields.BITSTREAM_ERROR + 4;
        }
    }

    internal interface BitstreamErrors : JavaLayerErrors
    {
    }
}