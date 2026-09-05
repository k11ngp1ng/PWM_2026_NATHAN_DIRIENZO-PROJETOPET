using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VanderPet.Models;

public class ServicoExibicao : INotifyPropertyChanged
{
	public int Id { get; set; }
	public string Servico { get; set; } = string.Empty;
	public string Descricao { get; set; } = string.Empty;
	public decimal PrecoCalculado { get; set; }
	public int DuracaoMinutosFinal { get; set; }

	public string PrecoFormatado => $"{PrecoCalculado:C}";
	public string DuracaoFormatada => $"{DuracaoMinutosFinal} min";

	private bool _isSelected;
	public bool IsSelected
	{
		get => _isSelected;
		set
		{
			if (_isSelected != value)
			{
				_isSelected = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(BackgroundColor));
				OnPropertyChanged(nameof(BorderColor));
			}
		}
	}

	public string BackgroundColor => IsSelected ? "#E6F2EF" : "#FFFFFF";
	public string BorderColor => IsSelected ? "#3A8D7A" : "#E2E8F0";

	public event PropertyChangedEventHandler? PropertyChanged;
	protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}