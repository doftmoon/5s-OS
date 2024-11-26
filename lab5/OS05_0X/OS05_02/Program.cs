using System.Diagnostics;

class Program
{
	const int ThreadCount = 10; // Количество потоков
	const int ThreadLifeTime = 10; // Время работы каждого потока в секундах
	const int ObservationTime = 30; // Время наблюдения в секундах
	static int[,] Matrix = new int[ThreadCount, ObservationTime];
	static DateTime StartTime = DateTime.Now;
	static object lockObject = new object(); // Объект для синхронизации

	static void WorkThread(object o)
	{
		int id = (int)o;
		for (int i = 0; i < ThreadLifeTime * 20; i++)
		{
			DateTime CurrentTime = DateTime.Now;
			int ElapsedSeconds = (int)Math.Round(CurrentTime.Subtract(StartTime).TotalSeconds - 0.49);
			if (ElapsedSeconds >= ObservationTime) break; // Останавливаемся, если превысили время наблюдения

			if (ElapsedSeconds >= 0 && ElapsedSeconds < ObservationTime)
			{
				Matrix[id, ElapsedSeconds] += 50;
			}

			Thread.Sleep(50); // Симулируем работу потока
		}
	}

	static void Main(string[] args)
	{
		Process.GetCurrentProcess().ProcessorAffinity = (System.IntPtr)15;
		Thread[] threads = new Thread[ThreadCount];
		for (int i = 0; i < ThreadCount; ++i)
		{
			object threadId = i;
			threads[i] = new Thread(WorkThread);
			switch (i % 3)
			{
				case 0:
					threads[i].Priority = ThreadPriority.Lowest;
					break;
				case 3:
					threads[i].Priority = ThreadPriority.Lowest;
					break;
				case 6:
					threads[i].Priority = ThreadPriority.Lowest;
					break;
				case 2:
					threads[i].Priority = ThreadPriority.Highest;
					break;
				case 5:
					threads[i].Priority = ThreadPriority.Highest;
					break;
				case 8:
					threads[i].Priority = ThreadPriority.Highest;
					break;
			}

			threads[i].Start(threadId);
		}

		Console.WriteLine("A student Artem is waiting for the threads to finish");
		for (int i = 0; i < ThreadCount; ++i)
		{
			threads[i].Join();
		}

		// Вывод таблицы с информацией о работе потоков
		Console.WriteLine("Time ");
		for (int s = 0; s < ObservationTime; s++)
		{
			Console.Write("{0,3}:  ", s);
			for (int th = 0; th < ThreadCount; th++)
			{
				Console.Write(" {0,5}", Matrix[th, s]);
			}
			Console.WriteLine();
		}
	}
}
