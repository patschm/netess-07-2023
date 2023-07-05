using System;
using System.IO;

namespace Vullis;
public class Unmanaged : IDisposable
{
    private static bool _isOpen = false;
    private FileStream _file = null;

    public void Open()
    {
        System.Console.WriteLine("Trying to open....");
        if (!_isOpen)
        {
            System.Console.WriteLine("Open...");
            _isOpen = true;
            _file = File.OpenWrite("bla.txt");
            return;
        }
        System.Console.WriteLine("Helaas! Is in gebruik");
    }
    public void Close()
    {
        System.Console.WriteLine("Closing....");
        _isOpen = false;
    }

    private void Dispose(bool fromDispose)
    {
        Close();
        if (fromDispose)
        {
            _file?.Dispose();
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);

    }

    ~Unmanaged()
    {
        Dispose(false);
    }

}
