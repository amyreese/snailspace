using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
namespace SnailsPace.Objects
{
	[Serializable]
	class HighScoreList
	{
		public Dictionary<String, List<Score>> pointsScores;

		public Dictionary<String, List<Score>> accuracyScores;

		public HighScoreList()
		{
			pointsScores = new Dictionary<String, List<Score>>();
			accuracyScores = new Dictionary<String, List<Score>>();
			pointsScores.Add("Test", new List<Score>());
			List<Score> list;
			pointsScores.TryGetValue("Test", out list);
			list.Add(new Score(123,"testName"));
			accuracyScores.Add("Test", new List<Score>());
			List<Score> list2;
			accuracyScores.TryGetValue("Test", out list2);
			list2.Add(new Score(321,"testName"));
		}

		public Boolean isHighAccuracy(String map, int accuracy)
		{
			List<Score> mapScores;
			accuracyScores.TryGetValue(map, out mapScores);
			if (mapScores == null)
			{
				accuracyScores.Add(map, new List<Score>());
			}
			accuracyScores.TryGetValue(map, out mapScores);

			List<Score>.Enumerator scoreEnumerator = mapScores.GetEnumerator();
			if (mapScores.Count < 5)
			{
				return true;
			}
			int scoreCount = 0;

			while (scoreEnumerator.MoveNext() && scoreCount < 5)
			{

				scoreCount++;
				if (accuracy > scoreEnumerator.Current.score)
				{

					return true;
				}
			}
			return false;
		}

		public Boolean isHighPoints(String map, int points)
		{
			List<Score> mapScores;
			pointsScores.TryGetValue(map, out mapScores);
			if (mapScores == null)
			{
				pointsScores.Add(map, new List<Score>());
			}
			pointsScores.TryGetValue(map, out mapScores);

			List<Score>.Enumerator scoreEnumerator = mapScores.GetEnumerator();
			if (mapScores.Count < 5)
			{
				return true;
			}
			int scoreCount = 0;
			while (scoreEnumerator.MoveNext() && scoreCount < 5)
			{
				scoreCount++;
				if (points > scoreEnumerator.Current.score)
				{
					return true;
				}
			}
			return false;
		}

		public void addPointsScore(String map, int points, String name)
		{
			if (isHighPoints(map, points))
			{
				List<Score> mapScores;
				pointsScores.TryGetValue(map, out mapScores);
				if (mapScores == null)
				{
					mapScores = new List<Score>();
					pointsScores.Add(map, mapScores);
				}
				mapScores.Add(new Score(points, name));
				mapScores.Sort();
				if (mapScores.Count > 5)
				{
					List<Score>.Enumerator mapScoreEnumerator = mapScores.GetEnumerator();
					mapScoreEnumerator.MoveNext();
					mapScores.Remove(mapScoreEnumerator.Current);
				}
			}
		}

		public void addAccuracyScore(String map, int accuracy, String name)
		{
			if (isHighAccuracy(map, accuracy))
			{
				List<Score> mapScores;
				accuracyScores.TryGetValue(map, out mapScores);
				if (mapScores == null)
				{
					mapScores = new List<Score>();
					accuracyScores.Add(map, mapScores);
				}
				mapScores.Add(new Score(accuracy, name));
				mapScores.Sort();
				if (mapScores.Count > 5)
				{
					List<Score>.Enumerator mapScoreEnumerator = mapScores.GetEnumerator();
					mapScoreEnumerator.MoveNext();
					mapScores.Remove(mapScoreEnumerator.Current);
				}

			}
		}
#if debug
		public void debugPrint()
		{
			Dictionary<String, List<Score>>.Enumerator mapEnumerator = pointsScores.GetEnumerator();
			while (mapEnumerator.MoveNext())
			{
				Debug.WriteLine("Map: " + mapEnumerator.Current.Key);
				List<Score> scores = mapEnumerator.Current.Value;
				scores.Sort();
				List<Score>.Enumerator pointsEnumerator = scores.GetEnumerator();
				while (pointsEnumerator.MoveNext())
				{
					Debug.WriteLine(pointsEnumerator.Current.score + " " + pointsEnumerator.Current.score);
				}
			}
			mapEnumerator = accuracyScores.GetEnumerator();

			while (mapEnumerator.MoveNext())
			{
				Debug.WriteLine("Map: " + mapEnumerator.Current.Key);
				List<Score> scores = mapEnumerator.Current.Value;
				scores.Sort();
				List<Score>.Enumerator accuracyEnumerator = scores.GetEnumerator();
				while (accuracyEnumerator.MoveNext())
				{
					Debug.WriteLine(accuracyEnumerator.Current.score + " " + accuracyEnumerator.Current.score);
				}
			}
		}
#endif
	}
}