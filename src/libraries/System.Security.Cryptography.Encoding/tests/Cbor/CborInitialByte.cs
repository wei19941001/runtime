﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

namespace System.Security.Cryptography.Encoding.Tests.Cbor
{
    internal enum CborMajorType : byte
    {
        UnsignedInteger = 0,
        NegativeInteger = 1,
        ByteString = 2,
        TextString = 3,
        Array = 4,
        Map = 5,
        Tag = 6,
        Special = 7,
    }

    internal enum CborAdditionalInfo : byte
    {
        UnsignedInteger8BitEncoding = 24,
        UnsignedInteger16BitEncoding = 25,
        UnsignedInteger32BitEncoding = 26,
        UnsignedInteger64BitEncoding = 27,
        IndefiniteLength = 31,
    }

    /// Represents the Cbor Data item initial byte structure
    internal readonly struct CborInitialByte
    {
        private const byte AdditionalInformationMask = 0b000_11111;
        public byte InitialByte { get; }

        internal CborInitialByte(CborMajorType majorType, CborAdditionalInfo additionalInfo)
        {
            Debug.Assert((byte)majorType < 8, "CBOR Major Type is out of range");
            Debug.Assert((byte)additionalInfo < 32, "CBOR initial byte additional info is out of range");

            InitialByte = (byte)(((byte)majorType << 5) | (byte)additionalInfo);
        }

        internal CborInitialByte(byte initialByte)
        {
            InitialByte = initialByte;
        }

        public CborMajorType MajorType => (CborMajorType)(InitialByte >> 5);
        public CborAdditionalInfo AdditionalInfo => (CborAdditionalInfo)(InitialByte & AdditionalInformationMask);
    }
}
