using System;
using System.Collections.Generic;
using System.Text;

namespace VanderPet.Models
{
	public class Racas
	{
		public int Id { get; set; }
		public string Raca { get; set; } = string.Empty;
		public int EspecieId { get; set; } = 0;
	}
}
