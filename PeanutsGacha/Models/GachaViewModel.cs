namespace PeanutsGacha.Models;

public sealed class GachaViewModel
{
	public IReadOnlyList<Prize> Prizes { get; init; } = Array.Empty<Prize>();
	public Prize? SelectedPrize { get; init; }
}
