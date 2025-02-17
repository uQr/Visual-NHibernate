//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------


// These interfaces serve as an extension to the BCL's SymbolStore interfaces.
namespace Microsoft.Samples.Debugging.CorSymbolStore
{
	using System;
	using System.Diagnostics.SymbolStore;
	using System.Runtime.InteropServices;
	using System.Runtime.InteropServices.ComTypes;

	[
		ComImport,
		Guid("AA544d42-28CB-11d3-bd22-0000f80849bd"),
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
		ComVisible(false)
	]
	internal interface ISymUnmanagedBinder
	{
		// These methods will often return error HRs in common cases.
		// If there are no symbols for the given target, a failing hr is returned.
		// This is pretty common.
		//
		// Using PreserveSig and manually handling error cases provides a big performance win.
		// Far fewer exceptions will be thrown and caught.
		// Exceptions should be reserved for truely "exceptional" cases.
		[PreserveSig]
		int GetReaderForFile(IntPtr importer,
								  [MarshalAs(UnmanagedType.LPWStr)] String filename,
								  [MarshalAs(UnmanagedType.LPWStr)] String SearchPath,
								  [MarshalAs(UnmanagedType.Interface)] out ISymUnmanagedReader retVal);

		[PreserveSig]
		int GetReaderFromStream(IntPtr importer,
										IStream stream,
										[MarshalAs(UnmanagedType.Interface)] out ISymUnmanagedReader retVal);
	}

	[
		ComImport,
		Guid("ACCEE350-89AF-4ccb-8B40-1C2C4C6F9434"),
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
		ComVisible(false)
	]
	internal interface ISymUnmanagedBinder2 : ISymUnmanagedBinder
	{
		// ISymUnmanagedBinder methods (need to define the base interface methods also, per COM interop requirements)
		[PreserveSig]
		new int GetReaderForFile(IntPtr importer,
								  [MarshalAs(UnmanagedType.LPWStr)] String filename,
								  [MarshalAs(UnmanagedType.LPWStr)] String SearchPath,
								  [MarshalAs(UnmanagedType.Interface)] out ISymUnmanagedReader retVal);

		[PreserveSig]
		new int GetReaderFromStream(IntPtr importer,
										IStream stream,
										[MarshalAs(UnmanagedType.Interface)] out ISymUnmanagedReader retVal);

		// ISymUnmanagedBinder2 methods 
		[PreserveSig]
		int GetReaderForFile2(IntPtr importer,
								  [MarshalAs(UnmanagedType.LPWStr)] String fileName,
								  [MarshalAs(UnmanagedType.LPWStr)] String searchPath,
								  int searchPolicy,
								  [MarshalAs(UnmanagedType.Interface)] out ISymUnmanagedReader pRetVal);
	}

	[
		ComImport,
		Guid("28AD3D43-B601-4d26-8A1B-25F9165AF9D7"),
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
		ComVisible(false)
	]
	internal interface ISymUnmanagedBinder3 : ISymUnmanagedBinder2
	{
		// ISymUnmanagedBinder methods (need to define the base interface methods also, per COM interop requirements)
		[PreserveSig]
		new int GetReaderForFile(IntPtr importer,
								  [MarshalAs(UnmanagedType.LPWStr)] String filename,
								  [MarshalAs(UnmanagedType.LPWStr)] String SearchPath,
								  [MarshalAs(UnmanagedType.Interface)] out ISymUnmanagedReader retVal);

		[PreserveSig]
		new int GetReaderFromStream(IntPtr importer,
										IStream stream,
										[MarshalAs(UnmanagedType.Interface)] out ISymUnmanagedReader retVal);

		// ISymUnmanagedBinder2 methods (need to define the base interface methods also, per COM interop requirements)
		[PreserveSig]
		new int GetReaderForFile2(IntPtr importer,
				   [MarshalAs(UnmanagedType.LPWStr)] String fileName,
				   [MarshalAs(UnmanagedType.LPWStr)] String searchPath,
				   int searchPolicy,
				   [MarshalAs(UnmanagedType.Interface)] out ISymUnmanagedReader pRetVal);

		// ISymUnmanagedBinder3 methods 
		[PreserveSig]
		int GetReaderFromCallback(IntPtr importer,
								   [MarshalAs(UnmanagedType.LPWStr)] String fileName,
								   [MarshalAs(UnmanagedType.LPWStr)] String searchPath,
								   int searchPolicy,
								   IntPtr callback,
								   [MarshalAs(UnmanagedType.Interface)] out ISymUnmanagedReader pRetVal);
	}


	public class SymbolBinder : ISymbolBinder1, ISymbolBinder2
	{
		ISymUnmanagedBinder m_binder;

		public SymbolBinder()
		{
			Guid CLSID_CorSymBinder = new Guid("0A29FF9E-7F9C-4437-8B11-F424491E3931");
			m_binder = (ISymUnmanagedBinder3)Activator.CreateInstance(Type.GetTypeFromCLSID(CLSID_CorSymBinder));
		}

		public ISymbolReader GetReader(IntPtr importer, String filename,
										  String searchPath)
		{
			ISymUnmanagedReader reader = null;
			int hr = m_binder.GetReaderForFile(importer, filename, searchPath, out reader);
			if (IsFailingResultNormal(hr))
			{
				return null;
			}
			Marshal.ThrowExceptionForHR(hr);
			return new SymReader(reader);
		}

		public ISymbolReader GetReaderForFile(Object importer, String filename,
										   String searchPath)
		{
			ISymUnmanagedReader reader = null;
			IntPtr uImporter = IntPtr.Zero;
			try
			{
				uImporter = Marshal.GetIUnknownForObject(importer);
				int hr = m_binder.GetReaderForFile(uImporter, filename, searchPath, out reader);
				if (IsFailingResultNormal(hr))
				{
					return null;
				}
				Marshal.ThrowExceptionForHR(hr);
			}
			finally
			{
				if (uImporter != IntPtr.Zero)
					Marshal.Release(uImporter);
			}
			return new SymReader(reader);
		}

		public ISymbolReader GetReaderForFile(Object importer, String fileName,
										   String searchPath, SymSearchPolicies searchPolicy)
		{
			ISymUnmanagedReader symReader = null;
			IntPtr uImporter = IntPtr.Zero;
			try
			{
				uImporter = Marshal.GetIUnknownForObject(importer);
				int hr = ((ISymUnmanagedBinder2)m_binder).GetReaderForFile2(uImporter, fileName, searchPath, (int)searchPolicy, out symReader);
				if (IsFailingResultNormal(hr))
				{
					return null;
				}
				Marshal.ThrowExceptionForHR(hr);
			}
			finally
			{
				if (uImporter != IntPtr.Zero)
					Marshal.Release(uImporter);
			}
			return new SymReader(symReader);
		}

		public ISymbolReader GetReaderForFile(Object importer, String fileName,
										   String searchPath, SymSearchPolicies searchPolicy,
										   IntPtr callback)
		{
			ISymUnmanagedReader reader = null;
			IntPtr uImporter = IntPtr.Zero;
			try
			{
				uImporter = Marshal.GetIUnknownForObject(importer);
				int hr = ((ISymUnmanagedBinder3)m_binder).GetReaderFromCallback(uImporter, fileName, searchPath, (int)searchPolicy, callback, out reader);
				if (IsFailingResultNormal(hr))
				{
					return null;
				}
				Marshal.ThrowExceptionForHR(hr);
			}
			finally
			{
				if (uImporter != IntPtr.Zero)
					Marshal.Release(uImporter);
			}
			return new SymReader(reader);
		}

		public ISymbolReader GetReaderFromStream(Object importer, IStream stream)
		{
			ISymUnmanagedReader reader = null;
			IntPtr uImporter = IntPtr.Zero;
			try
			{
				uImporter = Marshal.GetIUnknownForObject(importer);
				int hr = ((ISymUnmanagedBinder2)m_binder).GetReaderFromStream(uImporter, stream, out reader);
				if (IsFailingResultNormal(hr))
				{
					return null;
				}
				Marshal.ThrowExceptionForHR(hr);
			}
			finally
			{
				if (uImporter != IntPtr.Zero)
					Marshal.Release(uImporter);
			}
			return new SymReader(reader);
		}

		private static bool IsFailingResultNormal(int hr)
		{
			// If a pdb is not found, that's a pretty common thing.
			if (hr == unchecked((int)0x806D0005))   // E_PDB_NOT_FOUND
			{
				return true;
			}
			// Other fairly common things may happen here, but we don't want to hide
			// this from the programmer.
			// You may get 0x806D0014 if the pdb is there, but just old (mismatched)
			// Or if you ask for the symbol information on something that's not an assembly.
			// If that may happen for your application, wrap calls to GetReaderForFile in 
			// try-catch(COMException) blocks and use the error code in the COMException to report error.
			return false;
		}
	}
}
