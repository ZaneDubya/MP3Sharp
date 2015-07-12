// /***************************************************************************
//  * Decoder.cs
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
    ///     Encapsulates the details of decoding an MPEG audio frame.
    /// </summary>
    internal class Decoder : DecoderErrors
    {
        private static readonly Params DEFAULT_PARAMS = new Params();
        private readonly Params params_Renamed;
        private Equalizer equalizer;

        /// <summary>
        ///     Synthesis filter for the left channel.
        /// </summary>
        private SynthesisFilter filter1;

        /// <summary>
        ///     Sythesis filter for the right channel.
        /// </summary>
        private SynthesisFilter filter2;

        private bool initialized;
        private LayerIDecoder l1decoder;
        private LayerIIDecoder l2decoder;

        /// <summary>
        ///     The decoder used to decode layer III frames.
        /// </summary>
        private LayerIIIDecoder l3decoder;

        /// <summary>
        ///     The Bistream from which the MPEG audio frames are read.
        /// </summary>
        /// <summary>
        ///     The Obuffer instance that will receive the decoded
        ///     PCM samples.
        /// </summary>
        private Obuffer output;

        private int outputChannels;
        private int outputFrequency;

        /// <summary>
        ///     Creates a new <code>Decoder</code> instance with default
        ///     parameters.
        /// </summary>
        public Decoder() : this(null)
        {
            InitBlock();
        }

        /// <summary>
        ///     Creates a new <code>Decoder</code> instance with default
        ///     parameters.
        /// </summary>
        /// <param name="params	The">
        ///     <code>Params</code> instance that describes
        ///     the customizable aspects of the decoder.
        /// </param>
        public Decoder(Params params0)
        {
            InitBlock();
            if (params0 == null)
                params0 = DEFAULT_PARAMS;

            params_Renamed = params0;

            Equalizer eq = params_Renamed.InitialEqualizerSettings;
            if (eq != null)
            {
                equalizer.FromEqualizer = eq;
            }
        }

        public static Params DefaultParams
        {
            get { return (Params) DEFAULT_PARAMS.Clone(); // MemberwiseClone();
            }
        }

        public virtual Equalizer Equalizer
        {
            set
            {
                if (value == null)
                    value = Equalizer.PASS_THRU_EQ;

                equalizer.FromEqualizer = value;

                float[] factors = equalizer.BandFactors;
                if (filter1 != null)
                    filter1.EQ = factors;

                if (filter2 != null)
                    filter2.EQ = factors;
            }
        }

        /// <summary>
        ///     Changes the output buffer. This will take effect the next time
        ///     decodeFrame() is called.
        /// </summary>
        public virtual Obuffer OutputBuffer
        {
            set { output = value; }
        }

        /// <summary>
        ///     Retrieves the sample frequency of the PCM samples output
        ///     by this decoder. This typically corresponds to the sample
        ///     rate encoded in the MPEG audio stream.
        /// </summary>
        /// <param name="the">
        ///     sample rate (in Hz) of the samples written to the
        ///     output buffer when decoding.
        /// </param>
        public virtual int OutputFrequency
        {
            get { return outputFrequency; }
        }

        /// <summary>
        ///     Retrieves the number of channels of PCM samples output by
        ///     this decoder. This usually corresponds to the number of
        ///     channels in the MPEG audio stream, although it may differ.
        /// </summary>
        /// <returns>
        ///     The number of output channels in the decoded samples: 1
        ///     for mono, or 2 for stereo.
        /// </returns>
        public virtual int OutputChannels
        {
            get { return outputChannels; }
        }

        /// <summary>
        ///     Retrieves the maximum number of samples that will be written to
        ///     the output buffer when one frame is decoded. This can be used to
        ///     help calculate the size of other buffers whose size is based upon
        ///     the number of samples written to the output buffer. NB: this is
        ///     an upper bound and fewer samples may actually be written, depending
        ///     upon the sample rate and number of channels.
        /// </summary>
        /// <returns>
        ///     The maximum number of samples that are written to the
        ///     output buffer when decoding a single frame of MPEG audio.
        /// </returns>
        public virtual int OutputBlockSize
        {
            get { return Obuffer.OBUFFERSIZE; }
        }

        private void InitBlock()
        {
            equalizer = new Equalizer();
        }

        /// <summary>
        ///     Decodes one frame from an MPEG audio bitstream.
        /// </summary>
        /// <param name="header		The">
        ///     header describing the frame to decode.
        /// </param>
        /// <param name="bitstream		The">
        ///     bistream that provides the bits for te body of the frame.
        /// </param>
        /// <returns>
        ///     A SampleBuffer containing the decoded samples.
        /// </returns>
        public virtual Obuffer decodeFrame(Header header, Bitstream stream)
        {
            if (!initialized)
            {
                initialize(header);
            }

            int layer = header.layer();

            output.clear_buffer();

            IFrameDecoder decoder = retrieveDecoder(header, stream, layer);

            decoder.decodeFrame();

            output.write_buffer(1);

            return output;
        }

        protected internal virtual DecoderException newDecoderException(int errorcode)
        {
            return new DecoderException(errorcode, null);
        }

        protected internal virtual DecoderException newDecoderException(int errorcode, Exception throwable)
        {
            return new DecoderException(errorcode, throwable);
        }

        protected internal virtual IFrameDecoder retrieveDecoder(Header header, Bitstream stream, int layer)
        {
            IFrameDecoder decoder = null;

            // REVIEW: allow channel output selection type
            // (LEFT, RIGHT, BOTH, DOWNMIX)
            switch (layer)
            {
                case 3:
                    if (l3decoder == null)
                    {
                        l3decoder = new LayerIIIDecoder(stream, header, filter1, filter2, output,
                            (int) OutputChannelsEnum.BOTH_CHANNELS);
                    }

                    decoder = l3decoder;
                    break;

                case 2:
                    if (l2decoder == null)
                    {
                        l2decoder = new LayerIIDecoder();
                        l2decoder.create(stream, header, filter1, filter2, output,
                            (int) OutputChannelsEnum.BOTH_CHANNELS);
                    }
                    decoder = l2decoder;
                    break;

                case 1:
                    if (l1decoder == null)
                    {
                        l1decoder = new LayerIDecoder();
                        l1decoder.create(stream, header, filter1, filter2, output,
                            (int) OutputChannelsEnum.BOTH_CHANNELS);
                    }
                    decoder = l1decoder;
                    break;
            }

            if (decoder == null)
            {
                throw newDecoderException(DecoderErrors_Fields.UNSUPPORTED_LAYER, null);
            }

            return decoder;
        }

        private void initialize(Header header)
        {
            // REVIEW: allow customizable scale factor
            float scalefactor = 32700.0f;

            int mode = header.mode();
            int layer = header.layer();
            int channels = mode == Header.SINGLE_CHANNEL ? 1 : 2;

            // set up output buffer if not set up by client.
            if (output == null)
                output = new SampleBuffer(header.frequency(), channels);

            float[] factors = equalizer.BandFactors;
            //Console.WriteLine("NOT CREATING SYNTHESIS FILTERS");
            filter1 = new SynthesisFilter(0, scalefactor, factors);

            // REVIEW: allow mono output for stereo
            if (channels == 2)
                filter2 = new SynthesisFilter(1, scalefactor, factors);

            outputChannels = channels;
            outputFrequency = header.frequency();

            initialized = true;
        }

        /// <summary>
        ///     The <code>Params</code> class presents the customizable
        ///     aspects of the decoder.
        ///     <p>
        ///         Instances of this class are not thread safe.
        /// </summary>
        internal class Params : ICloneable
        {
            private Equalizer equalizer;
            private OutputChannels outputChannels;

            public virtual OutputChannels OutputChannels
            {
                get { return outputChannels; }

                set
                {
                    if (value == null)
                        throw new NullReferenceException("out");

                    outputChannels = value;
                }
            }

            /// <summary>
            ///     Retrieves the equalizer settings that the decoder's equalizer
            ///     will be initialized from.
            ///     <p>
            ///         The <code>Equalizer</code> instance returned
            ///         cannot be changed in real time to affect the
            ///         decoder output as it is used only to initialize the decoders
            ///         EQ settings. To affect the decoder's output in realtime,
            ///         use the Equalizer returned from the getEqualizer() method on
            ///         the decoder.
            /// </summary>
            /// <returns>
            ///     The <code>Equalizer</code> used to initialize the
            ///     EQ settings of the decoder.
            /// </returns>
            public virtual Equalizer InitialEqualizerSettings
            {
                get { return equalizer; }
            }

            //UPGRADE_TODO: The equivalent of method 'java.lang.Object.clone' is not an override method. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1143"'
            public object Clone()
            {
                try
                {
                    return MemberwiseClone();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(this + ": " + ex);
                }
            }

            private void InitBlock()
            {
                outputChannels = OutputChannels.BOTH;
                equalizer = new Equalizer();
            }
        }
    }
}