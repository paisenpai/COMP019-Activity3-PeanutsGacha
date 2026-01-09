using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PeanutsGacha.Models;

namespace PeanutsGacha.Controllers;

public sealed class HomeController : Controller
{
	private static readonly IReadOnlyList<Prize> PrizePool = new List<Prize>
	{
		new("Snoopy", "/assets/snoopy.jpg"),
		new("Joe Cool", "/assets/snoopy_joecool.jpg"),
		new("Flying Ace", "/assets/snoopy_flyingace.jpg"),
		new("Olaf", "/assets/olaf.jpg"),
		new("Spike", "/assets/spike.jpg"),
		new("Belle", "/assets/belle.jpg"),
		new("Marbles", "/assets/marbles.jpg"),
		new("Andy", "/assets/andy.jpg"),
		new("Woodstock", "/assets/woodstock.jpg"),
	};

	[HttpGet]
	public IActionResult Index()
	{
		return View(new GachaViewModel { Prizes = PrizePool });
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Roll()
	{
		// Weighted roll:
		// - Joe Cool: 1%
		// - Flying Ace: 1%
		// - Remaining 98% split evenly among the other 7 prizes (14% each)
		var roll = Random.Shared.Next(100); // 0..99

		Prize selectedPrize;
		if (roll == 0)
		{
			selectedPrize = PrizePool[1]; // Joe Cool
		}
		else if (roll == 1)
		{
			selectedPrize = PrizePool[2]; // Flying Ace
		}
		else
		{
			var commonIndex = (roll - 2) / 14; // 0..6
			selectedPrize = commonIndex switch
			{
				0 => PrizePool[0], // Snoopy
				1 => PrizePool[3], // Olaf
				2 => PrizePool[4], // Spike
				3 => PrizePool[5], // Belle
				4 => PrizePool[6], // Marbles
				5 => PrizePool[7], // Andy
				_ => PrizePool[8], // Woodstock
			};
		}

		return View("Index", new GachaViewModel
		{
			Prizes = PrizePool,
			SelectedPrize = selectedPrize,
		});
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
