// /***************************************************************************
//  * InputStreamSource.cs
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

namespace MP3Sharp.Decode
{
    /// <summary>
    ///     <i>Work In Progress.</i>
    ///     An instance of <code>InputStreamSource</code> implements a
    ///     <code>Source</code> that provides data from an
    ///     <code>InputStream
    /// </code>
    ///     . Seeking functionality is not supported.
    /// </summary>
    /// <author>
    ///     MDM
    /// </author>
    internal class InputStreamSource : Source
    {
        private readonly System.IO.Stream in_Renamed;

        public InputStreamSource(System.IO.Stream in_Renamed)
        {
            if (in_Renamed == null)
                throw new NullReferenceException("in");

            this.in_Renamed = in_Renamed;
        }

        public virtual bool Seekable
        {
            get { return false; }
        }

        public virtual int read(sbyte[] b, int offs, int len)
        {
            int read = SupportClass.ReadInput(in_Renamed, ref b, offs, len);
            return read;
        }

        public virtual bool willReadBlock()
        {
            return true;
            //boolean block = (in.available()==0);
            //return block;
        }

        public virtual long tell()
        {
            return -1;
        }

        public virtual long seek(long to)
        {
            return -1;
        }

        public virtual long length()
        {
            return -1;
        }
    }
}