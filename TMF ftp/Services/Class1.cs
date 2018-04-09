using System;
using System.ComponentModel;

namespace TMF_ftp.Services
{
    public class Class1 : IDisposable
    {
        private IntPtr handle;

        private Component component = new Component();

        private bool disposed = false;

        public Class1(IntPtr handle)
        {
            this.handle = handle;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {

                if (disposing)
                {

                    component.Dispose();
                }

                CloseHandle(handle);
                handle = IntPtr.Zero;

                disposed = true;

            }
        }
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);
        
        ~Class1()
        {
            Dispose(false);
        }
    }

    
}
