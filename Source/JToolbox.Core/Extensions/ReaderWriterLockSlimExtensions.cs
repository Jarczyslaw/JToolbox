using System;
using System.Threading;
using System.Threading.Tasks;

namespace JToolbox.Core.Extensions
{
    public static class ReaderWriterLockSlimExtensions
    {
        public static void AsReader(this ReaderWriterLockSlim @lock, Action action)
        {
            @lock.EnterReadLock();
            try
            {
                action();
            }
            finally
            {
                @lock.ExitReadLock();
            }
        }

        public static T AsReader<T>(this ReaderWriterLockSlim @lock, Func<T> func)
        {
            @lock.EnterReadLock();
            try
            {
                return func();
            }
            finally
            {
                @lock.ExitReadLock();
            }
        }

        public static async Task AsReaderAsync(this ReaderWriterLockSlim @lock, Func<Task> func)
        {
            @lock.EnterReadLock();
            try
            {
                await func();
            }
            finally
            {
                @lock.ExitReadLock();
            }
        }

        public static async Task<T> AsReaderAsync<T>(this ReaderWriterLockSlim @lock, Func<Task<T>> func)
        {
            @lock.EnterReadLock();
            try
            {
                return await func();
            }
            finally
            {
                @lock.ExitReadLock();
            }
        }

        public static void AsWriter(this ReaderWriterLockSlim @lock, Action action)
        {
            @lock.EnterWriteLock();
            try
            {
                action();
            }
            finally
            {
                @lock.ExitWriteLock();
            }
        }

        public static T AsWriter<T>(this ReaderWriterLockSlim @lock, Func<T> func)
        {
            @lock.EnterWriteLock();
            try
            {
                return func();
            }
            finally
            {
                @lock.ExitWriteLock();
            }
        }

        public static async Task AsWriterAsync(this ReaderWriterLockSlim @lock, Func<Task> func)
        {
            @lock.EnterWriteLock();
            try
            {
                await func();
            }
            finally
            {
                @lock.ExitWriteLock();
            }
        }

        public static async Task<T> AsWriterAsync<T>(this ReaderWriterLockSlim @lock, Func<Task<T>> func)
        {
            @lock.EnterWriteLock();
            try
            {
                return await func();
            }
            finally
            {
                @lock.ExitWriteLock();
            }
        }
    }
}