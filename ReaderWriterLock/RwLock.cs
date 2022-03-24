using System;
using System.Threading;

namespace ReaderWriterLock;

public class RwLock : IRwLock
{
    private readonly object writeLock = new();
    private volatile uint readers;

    public void ReadLocked(Action action)
    {
        lock (writeLock)
            Interlocked.Increment(ref readers);

        try
        {
            action();
        }
        finally
        {
            Interlocked.Decrement(ref readers);
        }
    }

    public void WriteLocked(Action action)
    {
        lock (writeLock)
        {
            while (readers > 0)
            {
            }

            action();
        }
    }
}