using System.Diagnostics;

namespace DictionaryResearch;

internal static class Program
{
	internal const int DictionaryLength = 500000;

	internal static void Main()
	{
		Helper.CheckBuckets(); // для 50к: 75431

		var defaultFilling = new Action(() =>
		{
			var count = Enumerable.Range(0, DictionaryLength).ToDictionary(i => i, i => "Value").Count;
		});
		
		var hardFilling = new Action(() =>
		{
			var count = Enumerable.Range(0, DictionaryLength).ToDictionary(i => 672827 * i, i => "Value").Count;
		});

		// TODO Сделать Action, меделнно заполняющий Dictionary;

		Console.WriteLine(Helper.MeasureTime(defaultFilling));
		Console.WriteLine(Helper.MeasureTime(hardFilling));

		// Какое время на вашем ПК для обоих Action : 47 и 2793
	}
}

internal static class Helper
{
	internal static void CheckBuckets()
	{
		var dictionary = Enumerable.Range(0, Program.DictionaryLength).ToDictionary(i => i, i => "Value");
		var count = dictionary.Count;
		Console.WriteLine(); // debug button
	}

	internal static long MeasureTime(Action action)
	{
		var time = new Stopwatch();
		GC.Collect();
		GC.WaitForPendingFinalizers();
		time.Restart();
		action.Invoke();
		time.Stop();
		return time.ElapsedMilliseconds;
	}
}