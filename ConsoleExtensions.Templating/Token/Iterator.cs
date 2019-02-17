namespace ConsoleExtensions.Templating.Token
{
	using System.Linq;

	internal class Iterator
	{
		public Iterator(string source)
		{
			this.Chars = source.ToCharArray();
			this.Length = this.Chars.Length;
		}

		public char[] Chars { get; }

		public char Current => this.Chars[this.Index];

		public bool EOL => this.Index >= this.Length;

		public int Index { get; private set; }

		public int Length { get; }

		public char Next => this.Index == this.Length - 1 ? '\0' : this.Chars[this.Index + 1];

		public int Start { get; private set; }

		public string GetExternal()
		{
			var result = new string(this.Chars, this.Start, this.Index - this.Start);
			return result;
		}

		public string GetInternal()
		{
			var result = new string(this.Chars, this.Start + 1, this.Index - this.Start - 1);
			this.Index++;
			return result;
		}

		public void Iterate(int count = 1)
		{
			this.Index += count;
		}

		public void IterateUntil(params char[] stopChars)
		{
			while (!this.EOL && !stopChars.Contains(this.Chars[this.Index]))
			{
				this.Index++;
			}
		}

		public void ResetStart()
		{
			this.Start = this.Index;
		}
	}
}