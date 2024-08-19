using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkThread
{
	public class Shop
	{
		private object _lock = new();
		public int Count { get; set; } = 0;

		private static Mutex _mutex = new Mutex();

		private static Semaphore _semaphore = new Semaphore(1, 1);

		public void ShowThread(int amount)
		{
			Console.WriteLine($"Группа {Thread.CurrentThread.ManagedThreadId} совершила {amount} покупок. Всего покупок: {Count}");
		}

		// Метод для увеличения счетчика покупок с использованием lock
		//Оператор lock определяет блок кода, внутри которого весь код блокируется и становится недоступным для других потоков до завершения работы текущего потока. Остальный потоки помещаются в очередь ожидания и ждут, пока текущий поток не освободит данный блок кода.
		public void MethodSynchronizationLock(int amount)
		{
			lock (_lock)
			{
				Count += amount;
				ShowThread(amount);
			}
		}

		// Метод для увеличения счетчика покупок с использованием Monitor
		public void MethodSynchronizationMonitor(int amount)
		{
			Monitor.Enter(_lock);//принимает объект блокировки
			try
			{
				Count += amount;
				ShowThread(amount);
			}
			finally
			{
				Monitor.Exit(_lock); //происходит освобождение объекта locker, если блокировка осуществлена успешно, и он становится доступным для других потоков
			}
		}

		// Метод для увеличения счетчика покупок с использованием Mutex	
		/*Mutex предназначен для синхронизации между процессами, а не потоками в рамках одного процесса.*/
		public void MethodSynchronizationMutex(int amount)
		{
			_mutex.WaitOne();
			try
			{
				Count += amount;
				ShowThread(amount);
			}
			finally
			{
				_mutex.ReleaseMutex();
			}
		}

		// Метод для увеличения счетчика покупок с использованием Semaphore
		//Семафоры позволяют ограничить количество потоков, которые имеют доступ к определенным ресурсам.
		public void MethodSynchronizationSemaphore(int amount)
		{
			_semaphore.WaitOne(); //ожидает получения свободного места в семафоре
			try
			{
				Count += amount;
				ShowThread(amount);
			}
			finally
			{
				_semaphore.Release();//освобождает место в семафоре
			}
		}
	}
}
