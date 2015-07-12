// /***************************************************************************
//  *   JavaLayerErrors.cs
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
    ///     Exception erorr codes for components of the JavaLayer API.
    /// </summary>
    internal struct JavaLayerErrors_Fields
    {
        public static readonly int BITSTREAM_ERROR = 0x100;
        public static readonly int DECODER_ERROR = 0x200;
    }

    internal interface JavaLayerErrors
    {
    }
}