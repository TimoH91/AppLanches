using Microsoft.Maui.Controls;

namespace AppLanches.Pages;

public partial class EmptyShoppingCartPage : ContentPage
{
    public EmptyShoppingCartPage()
    {
        InitializeComponent();
    }

    private async void BtnRetornar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}