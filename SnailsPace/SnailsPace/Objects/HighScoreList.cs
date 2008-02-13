using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
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
		public Dictionary<String, SortedDictionary<int, String>> pointsScores;

		public Dictionary<String, SortedDictionary<int, String>> accuracyScores;

		public HighScoreList()
		{
			pointsScores = new Dictionary<String, SortedDictionary<int, String>>();
			accuracyScores = new Dictionary<String, SortedDictionary<int, String>>();
		}

		public Boolean isHighAccuracy(String map, int accuracy)
		{
			SortedDictionary<int, String> mapScores;
			accuracyScores.TryGetValue( map, out mapScores );
			if (mapScores == null)
			{
				pointsScores.Add( map, new SortedDictionary<int, String>() ); 
			}
			accuracyScores.TryGetValue(map, out mapScores);
			SortedDictionary<int, String>.Enumerator scoreEnumerator = mapScores.GetEnumerator();
			int scoreCount = 0;
			while (scoreEnumerator.MoveNext() && scoreCount < 5 )
			{
				if (scoreCount < 5)
				{
					scoreCount++;
					if (accuracy > scoreEnumerator.Current.Key)
					{
						return true;
					}
				}
				else
				{
					return false;
				}
			}
			return false;
		}

		public Boolean isHighPoints(String map, int points)
		{
			SortedDictionary<int, String> mapScores;
			pointsScores.TryGetValue( map, out mapScores );
			if (mapScores == null)
			{
				pointsScores.Add( map, new SortedDictionary<int, String>() ); 
			}
			pointsScores.TryGetValue(map, out mapScores);
			SortedDictionary<int, String>.Enumerator scoreEnumerator = mapScores.GetEnumerator();
			int scoreCount = 0;
			while (scoreEnumerator.MoveNext() )
			{
				scoreCount++;
				if (points > scoreEnumerator.Current.Key)
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
				SortedDictionary<int, String> mapScores;
				pointsScores.TryGetValue(map, out mapScores);
				mapScores.Add(points, name);
			}
		}

		public void addAccuracyScore(String map, int accuracy, String name)
		{
			if (isHighPoints(map, accuracy))
			{
				SortedDictionary<int, String> mapScores;
				accuracyScores.TryGetValue(map, out mapScores);
				mapScores.Add(accuracy, name);
			}
		}

		/*public void Save()
		{

			StorageDevice device = Guide.BeginShowStorageDeviceSelector();
			// Open a storage container
			StorageContainer container = device.OpenContainer("TestStorage");
			// Get the path of the save game
			string filename = Path.Combine(container.Path, "highscores.xml");

			// Open the file, creating it if necessary
			FileStream stream = File.Open(filename, FileMode.OpenOrCreate);

			// Convert the object to XML data and put it in the stream
			XmlSerializer serializer = new XmlSerializer(typeof(HighScoreList));
			serializer.Serialize(stream, this);

			// Close the file
			stream.Close();
		}

		public void Load()
		{


			StorageDevice device = Guide.BeginShowStorageDeviceSelector();
			// Open a storage container
			StorageContainer container = device.OpenContainer("TestStorage");

			// Get the path of the save game
			string filename = Path.Combine(container.Path, "highscores.xml");

			// Check to see if the save exists
			if (!File.Exists(filename))
			{
				return;
			}

			// Open the file
			FileStream stream = File.Open(filename, FileMode.OpenOrCreate,
				FileAccess.Read);

			// Read the data from the file
			XmlSerializer serializer = new XmlSerializer(typeof(HighScoreList));
			this = (HighScoreList)serializer.Deserialize(stream);

			// Close the file
			stream.Close();

			// Dispose the container
			container.Dispose();

		}*/

		public void debugPrint()
		{
			Dictionary<String, SortedDictionary< int, String >>.Enumerator mapEnumerator = pointsScores.GetEnumerator();

			while (mapEnumerator.MoveNext())
			{
				Debug.WriteLine("Map: " + mapEnumerator.Current);
				SortedDictionary<int, String> scores = mapEnumerator.Current.Value;
				SortedDictionary<int, String>.Enumerator pointsEnumerator = scores.GetEnumerator();
				while (pointsEnumerator.MoveNext())
				{
					Debug.WriteLine(pointsEnumerator.Current.Key + " " + pointsEnumerator.Current.Value);
				}
			}
			 mapEnumerator = accuracyScores.GetEnumerator();

			while (mapEnumerator.MoveNext())
			{
				Debug.WriteLine("Map: " + mapEnumerator.Current);
				SortedDictionary<int, String> scores = mapEnumerator.Current.Value;
				SortedDictionary<int, String>.Enumerator accuracyEnumerator = scores.GetEnumerator();
				while (accuracyEnumerator.MoveNext())
				{
					Debug.WriteLine(accuracyEnumerator.Current.Key + " " + accuracyEnumerator.Current.Value);
				}
			}
			Debug.WriteLine(pointsScores);
			Debug.WriteLine(accuracyScores);
		}
	}
}
