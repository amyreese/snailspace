using System;
using System.Collections.Generic;
using System.Text;

namespace SnailsPace.Objects
{
	class Score : IComparable<Score>
	{
		public int score;
		public String name;

		public Score(int score, String name)
		{
			this.score = score;
			this.name = name;
		}

		public int CompareTo(Score otherScore)
		{
			if (score > otherScore.score)
			{
				return 1;
			}
			else if (score < otherScore.score)
			{
				return -1;
			}
			else
			{
				return 0;
			}
		}
	}
}