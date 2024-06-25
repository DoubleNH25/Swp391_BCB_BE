<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseObject
{
    public class SlotIncludeTransaction
    {
        public int? TransactionId { get; set; }
        public List<ChatInfos>? ChatInfos { get; set; }
    }
    public class SlotReturnInfo
    {
        public string? Date { get; set; }
        public List<int>? SlotIds { get; set; }
        public string? Message { get; set; }
    }
    public class ChatInfos
    {
        public int? RoomId { get; set; }
        public string? PlayDate { get; set; }
=======
﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Runtime.CompilerServices;
using System.Text;
using Internal.Runtime.CompilerServices;

namespace System
{
    public partial class String
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern string FastAllocateString(int length);

        // Set extra byte for odd-sized strings that came from interop as BSTR.
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern void SetTrailByte(byte data);
        // Try to retrieve the extra byte - returns false if not present.
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern bool TryGetTrailByte(out byte data);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern string Intern();
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern string? IsInterned();

        public static string Intern(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return str.Intern();
        }

        public static string? IsInterned(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return str.IsInterned();
        }

        // Copies the source String (byte buffer) to the destination IntPtr memory allocated with len bytes.
        // Used by ilmarshalers.cpp
        internal static unsafe void InternalCopy(string src, IntPtr dest, int len)
        {
            if (len != 0)
            {
                Buffer.Memmove(ref *(byte*)dest, ref Unsafe.As<char, byte>(ref src.GetRawStringData()), (nuint)len);
            }
        }

        internal unsafe int GetBytesFromEncoding(byte* pbNativeBuffer, int cbNativeBuffer, Encoding encoding)
        {
            // encoding == Encoding.UTF8
            fixed (char* pwzChar = &_firstChar)
            {
                return encoding.GetBytes(pwzChar, Length, pbNativeBuffer, cbNativeBuffer);
            }
        }
>>>>>>> main
    }
}
