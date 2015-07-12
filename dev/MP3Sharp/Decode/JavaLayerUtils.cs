// /***************************************************************************
//  * JavaLayerUtils.cs
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
    ///     The JavaLayerUtils class is not strictly part of the JavaLayer API.
    ///     It serves to provide useful methods and system-wide hooks.
    /// </summary>
    /// <author>
    ///     MDM
    /// </author>
    internal class JavaLayerUtils
    {
        private static JavaLayerHook hook = null;

        /// <summary>
        ///     Sets the system-wide JavaLayer hook.
        /// </summary>
        public static JavaLayerHook Hook
        {
            get
            {
                lock (typeof (JavaLayerUtils))
                {
                    return hook;
                }
            }

            set
            {
                lock (typeof (JavaLayerUtils))
                {
                    hook = value;
                }
            }
        }
    }
}