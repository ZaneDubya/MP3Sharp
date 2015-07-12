// /***************************************************************************
//  * Control.cs
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
    /// </summary>
    internal interface Control
    {
        bool Playing { get; }

        bool RandomAccess { get; }

        /// <summary>
        ///     Retrieves the current position.
        /// </summary>
        /// <summary>
        /// </summary>
        double Position { get; set; }

        /// <summary>
        ///     Starts playback of the media presented by this control.
        /// </summary>
        void start();

        /// <summary>
        ///     Stops playback of the media presented by this control.
        /// </summary>
        void stop();

        void pause();
    }
}