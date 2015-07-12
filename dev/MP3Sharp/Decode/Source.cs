// /***************************************************************************
//  * Source.cs
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
    ///     Work in progress.
    ///     Class to describe a seekable data source.
    /// </summary>
    internal struct Source_Fields
    {
        public static readonly long LENGTH_UNKNOWN = -1;
    }

    internal interface Source
    {
        bool Seekable { get; }

        int read(sbyte[] b, int offs, int len);

        bool willReadBlock();

        long length();

        long tell();

        long seek(long pos);
    }
}