using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace HomeWorkThread
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Создаем два потока
			/*var thread1 = new Thread(() =>
			{
				var words = new List<string>() { "П", "р", "и", "в", "е", "т" };
				foreach (string word in words)
				{
					Console.Write(word + " ");
					Thread.Sleep(200);				
				}
				
			});
			
			var thread2 = new Thread(() =>
			{
				var words = new List<string>() { "П", "р", "и", "в", "е", "т" };
				foreach (string word in words)
				{
					Console.Write(word + " ");
					Thread.Sleep(200);
				}
			});

			// Запускаем потоки
			thread1.Start();
			Thread.Sleep(400);
			Console.WriteLine();
			thread2.Start();
			
			// Дожидаемся завершения потоков
			thread1.Join();
			Console.WriteLine();
			thread2.Join();

			Console.WriteLine("\nПотоки завершены.");
			Console.WriteLine();*/

			var shop = new Shop();
			var random = new Random();
			var threads = new List<Thread>();

			for (int i = 0; i < 5; i++)
			{
				threads.Add(new Thread(() => // создаём нговый поток и добвляем его в список threads
				{
					int purchaseCount = random.Next(1, 10);

					shop.MethodSynchronizationLock(purchaseCount);  //способ синхронизации lock

					shop.MethodSynchronizationMonitor(purchaseCount); //способ синхронизации Monitor

					shop.MethodSynchronizationMutex(purchaseCount);  //способ синхронизации Mutex

					shop.MethodSynchronizationSemaphore(purchaseCount);  //способ синхронизации Semaphore
				}));
			}

			// Запускаем все потоки
			foreach (Thread thread in threads)
			{
				thread.Start();
			}

			// Ждем завершения всех потоков
			foreach (Thread thread in threads)
			{
				thread.Join();// блокирует выполнение вызвавшего его потока до тех пор, пока не завершится поток, для которого был вызван данный метод
			}

			Console.WriteLine($"Всего покупок: {shop.Count}");

		}

	}
	}
