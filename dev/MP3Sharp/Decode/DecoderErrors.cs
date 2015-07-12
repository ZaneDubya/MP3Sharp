// /***************************************************************************
//  *   DecoderErrors.cs
//  *   Copyright (c) 2015 Zane Wagner, Robert Burke,
//  *   the JavaZoom team, and others.
//  * 
//  *   All rights reserved. This program and the accompanying materials
//  *   are made available under the terms of the GNU Lesser General Public License
//  *   (LGPL) version 2.1 which accompanies this distribution, and is available at
//  *   http://www.gnu.org/licenses/lgpl-2.1.html
//  *
//  *   This library is distributed in the hope that it will be useful,
//  *   but WITHOUT ANY WARRANTY; without even the implied warranty of
//  *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//  *   Lesser General Public License for more details.
//  *
//  ***************************************************************************/

namespace MP3Sharp.Decode
{
    /// <summary>
    ///     This interface provides constants describing the error
    ///     codes used by the Decoder to indicate errors.
    /// </summary>
    internal struct DecoderErrors_Fields
    {
        public static readonly int UNKNOWN_ERROR;
        public static readonly int UNSUPPORTED_LAYER;

        static DecoderErrors_Fields()
        {
            UNKNOWN_ERROR = JavaLayerErrors_Fields.DECODER_ERROR + 0;
            UNSUPPORTED_LAYER = JavaLayerErrors_Fields.DECODER_ERROR + 1;
        }
    }

    internal interface DecoderErrors : JavaLayerErrors
    {
    }
}