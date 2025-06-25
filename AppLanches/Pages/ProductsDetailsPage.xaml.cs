using AppLanches.Services;
using AppLanches.Validations;
using AppLanches.Models;
namespace AppLanches.Pages;

public partial class ProductsDetailsPage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly IValidator _validator;
    private int _produtoId;
    private bool _loginPageDisplayed = false;

    public ProductsDetailsPage(int produtoId,
                                string produtoNome,
                                ApiService apiService,
                                IValidator validator)
    {
        InitializeComponent();
        _apiService = apiService;
        _validator = validator;
        _produtoId = produtoId;
        Title = produtoNome ?? "Detalhe do Produto";
    }

    // M�todo chamado quando a p�gina aparece
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GetProdutoDetalhes(_produtoId);
    }

    private async Task<Product?> GetProdutoDetalhes(int produtoId)
    {
        var (produtoDetalhe, errorMessage) = await _apiService.GetProdutoDetalhe(produtoId);

        if (errorMessage == "Unauthorized" && !_loginPageDisplayed)
        {
            await DisplayLoginPage();
            return null;
        }

        // Verificar se houve algum erro na obten��o das produtos
        if (produtoDetalhe == null)
        {
            // Lidar com o erro, exibir mensagem ou logar
            await DisplayAlert("Erro", errorMessage ?? "N�o foi poss�vel obter o produto.", "OK");
            return null;
        }

        if (produtoDetalhe != null)
        {
            // Atualizar as propriedades dos controles com os dados do produto
            ImagemProduto.Source = produtoDetalhe.ImagePath;
            LblProdutoNome.Text = produtoDetalhe.Name;
            LblProdutoPreco.Text = produtoDetalhe.Price.ToString();
            LblProdutoDescricao.Text = produtoDetalhe.Detail;
            LblPrecoTotal.Text = produtoDetalhe.Price.ToString();
        }
        else
        {
            await DisplayAlert("Erro", errorMessage ?? "N�o foi poss�vel obter os detalhes do produto.", "OK");
            return null;
        }
        return produtoDetalhe;
    }

    //m�todos que ser�o implementados mais adiante no curso 
    //private void ImagemBtnFavorito_Clicked(object sender, EventArgs e) { }
    //private void BtnAdiciona_Clicked(object sender, EventArgs e) { }
    //private void BtnRemove_Clicked(object sender, EventArgs e) { }
    //private void BtnIncluirNoCarrinho_Clicked(object sender, EventArgs e) { }

    private async Task DisplayLoginPage()
    {
        _loginPageDisplayed = true;

        await Navigation.PushAsync(new LoginPage(_apiService, _validator));
    }

    private void ImagemBtnFavorito_Clicked(object sender, EventArgs e)
    {

    }

    private void BtnRemove_Clicked(object sender, EventArgs e)
    {

    }

    private void BtnAdiciona_Clicked(object sender, EventArgs e)
    {

    }

    private void BtnIncluirNoCarrinho_Clicked(object sender, EventArgs e)
    {

    }
}
