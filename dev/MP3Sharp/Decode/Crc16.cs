// /***************************************************************************
//  * Crc16.cs
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

using MP3Sharp.Support;

/*
* 02/12/99 : Java Conversion by E.B , ebsp@iname.com, JavaLayer
*
*-----------------------------------------------------------------------
*  @(#) crc.h 1.5, last edit: 6/15/94 16:55:32
*  @(#) Copyright (C) 1993, 1994 Tobias Bading (bading@cs.tu-berlin.de)
*  @(#) Berlin University of Technology
*
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
*-----------------------------------------------------------------------
*/

namespace MP3Sharp.Decode
{
    /// <summary>
    ///     16-Bit CRC checksum
    /// </summary>
    internal sealed class Crc16
    {
        private static readonly short polynomial;
        private short crc;

        static Crc16()
        {
            polynomial = (short) SupportClass.Identity(0x8005);
        }

        /// <summary>
        ///     Dummy Constructor
        /// </summary>
        public Crc16()
        {
            crc = (short) SupportClass.Identity(0xFFFF);
        }

        /// <summary>
        ///     Feed a bitstring to the crc calculation (0 < length <= 32).
        /// </summary>
        public void add_bits(int bitstring, int length)
        {
            int bitmask = 1 << (length - 1);
            do
                if (((crc & 0x8000) == 0) ^ ((bitstring & bitmask) == 0))
                {
                    crc <<= 1;
                    crc ^= polynomial;
                }
                else
                    crc <<= 1; while ((bitmask = SupportClass.URShift(bitmask, 1)) != 0);
        }

        /// <summary>
        ///     Return the calculated checksum.
        ///     Erase it for next calls to add_bits().
        /// </summary>
        public short checksum()
        {
            short sum = crc;
            crc = (short) SupportClass.Identity(0xFFFF);
            return sum;
        }
    }
}