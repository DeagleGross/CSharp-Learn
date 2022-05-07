using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class BasicCalculator2
	{
		[TestMethod]
		public void Solve()
		{
			var s = "3+2*2";
			var result = Calculate(s);

			result.Should().Be(7);
		}

		[TestMethod]
		public void Solve2()
		{
			var s = " 3+5 / 2 ";
			var result = Calculate(s);

			result.Should().Be(5);
		}

		[TestMethod]
		public void Solve3()
		{
			var s = "3+2*2+2";
			var result = Calculate(s);

			result.Should().Be(9);
		}

		[TestMethod]
		public void Solve4()
		{
			var s = "3/2";
			var result = Calculate(s);

			result.Should().Be(1);
		}

		[TestMethod]
		public void Solve5()
		{
			var s = "0-2147483647";
			var result = Calculate(s);

			result.Should().Be(-2147483647);
		}

		[TestMethod]
		public void Solve6()
		{
			var s = "1 - 1 + 1";
			var result = Calculate(s);

			result.Should().Be(1);
		}

		public int Calculate(string s)
		{
			var expression = s.Trim();

			Stack<string> evaluationStack = new();

			int position = 0;

			var token = GetNextToken(expression, ref position);

			while (token.tokenType != TokenType.End)
			{
				if (token.tokenType == TokenType.Number)
				{
					evaluationStack.Push(token.token);
					token = GetNextToken(expression, ref position);
					continue;
				}

				if (token.tokenType == TokenType.Operation)
				{
					if (token.token == "+"
						|| token.token == "-")
					{
						evaluationStack.Push(token.token);
						token = GetNextToken(expression, ref position);
						continue;
					}
					else
					{
						var operand1 = evaluationStack.Pop();
						var operand2 = GetNextToken(expression, ref position);

						var result = token.token switch
						{
							"*" => int.Parse(operand1) * int.Parse(operand2.token),
							"/" => int.Parse(operand1) / int.Parse(operand2.token),
							_ => throw new ArgumentOutOfRangeException()
						};

						evaluationStack.Push(result.ToString());

						token = GetNextToken(expression, ref position);
					}
				}
			}

			if (evaluationStack.Count == 1)
			{
				return int.Parse(evaluationStack.Pop());
			}

			var resultStack = new Stack<string>();
			while (evaluationStack.Count > 0)
			{
				resultStack.Push(evaluationStack.Pop());
			}

			var ret = 0;

			while (resultStack.Count > 0)
			{
				var operand1 = int.Parse(resultStack.Pop());
				var operation = resultStack.Pop();
				var operand2 = int.Parse(resultStack.Pop());

				var result = operation switch
				{
					"+" => operand1 + operand2,
					"-" => operand1 - operand2,
					_ => throw new ArgumentOutOfRangeException()
				};

				resultStack.Push(result.ToString());

				if (resultStack.Count == 1)
				{
					return int.Parse(resultStack.Pop());
				}
			}

			return ret;
		}

		private enum TokenType
		{
			Number,
			Operation,
			End
		}

		private (string token, TokenType tokenType) GetNextToken(string s, ref int position)
		{
			if (position >= s.Length)
			{
				return (string.Empty, TokenType.End);
			}

			var c = s[position];
			position++;

			while (c == ' ' && position < s.Length)
			{
				c = s[position];
				position++;
			}

			if (c == '*'
				|| c == '/'
				|| c == '+'
				|| c == '-')
			{
				return (c.ToString(), TokenType.Operation);
			}

			StringBuilder number = new();
			number.Append(c);
			
			while (position < s.Length && char.IsDigit(s[position]))
			{
				number.Append(s[position]);
				position++;
			}

			return (number.ToString(), TokenType.Number);
		}
	}
}
