using System;
using System.Collections.Generic;
using System.Text;

namespace SnailsPace.Objects
{
	class Score : IComparable<Score>
	{
		public int score;
		public String name;

		/// <summary>
		/// Create a Score object with a score and a name.
		/// </summary>
		/// <param name="score">The player's score.</param>
		/// <param name="name">The player's name.</param>
		public Score(int score, String name)
		{
			this.score = score;
			this.name = name;
		}

		/// <summary>
		/// Compare this Score to another Score.
		/// </summary>
		/// <param name="otherScore">The other Score.</param>
		/// <returns></returns>
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