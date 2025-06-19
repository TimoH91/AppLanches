using AppLanches.Validations;
using AppLanches.Services;
namespace AppLanches.Pages;

public partial class InscriptionPage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly IValidator _validator;

    public InscriptionPage(ApiService apiService, IValidator validator)
    {
        InitializeComponent();
        _apiService = apiService;
        _validator = validator;
    }

    private async void BtnSignup_ClickedAsync(object sender, EventArgs e)
    {
        if (await _validator.Validar(EntNome.Text, EntEmail.Text, EntPhone.Text, EntPassword.Text))
        {

            var response = await _apiService.RegisterUser(EntNome.Text, EntEmail.Text,
                                                          EntPhone.Text, EntPassword.Text);

            if (!response.HasError)
            {
                await DisplayAlert("Aviso", "Sua conta foi criada com sucesso !!", "OK");
                await Navigation.PushAsync(new LoginPage(_apiService, _validator));
            }
            else
            {
                await DisplayAlert("Erro", "Algo deu errado!!!", "Cancelar");
            }
        }
        else
        {
            string mensagemErro = "";
            mensagemErro += _validator.NameError != null ? $"\n- {_validator.NameError}" : "";
            mensagemErro += _validator.EmailError != null ? $"\n- {_validator.EmailError}" : "";
            mensagemErro += _validator.TelephoneError != null ? $"\n- {_validator.TelephoneError}" : "";
            mensagemErro += _validator.PasswordError != null ? $"\n- {_validator.PasswordError}" : "";

            await DisplayAlert("Erro", mensagemErro, "OK");
        }
    }

    private async void TapLogin_TappedAsync(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new LoginPage(_apiService, _validator));
    }
}