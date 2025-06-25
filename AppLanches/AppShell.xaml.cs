using AppLanches.Validations;
using AppLanches.Services;
using AppLanches.Validations;
using AppLanches.Pages;
namespace AppLanches
{
    public partial class AppShell : Shell
    {
        private readonly ApiService _apiService;
        private readonly IValidator _validator;

        public AppShell(ApiService apiService, IValidator validator)
        {
            InitializeComponent();
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _validator = validator;
            ConfigureShell();
        }

        private void ConfigureShell()
        {
            var homePage = new HomePage(_apiService, _validator);
            var carrinhoPage = new ShoppingCartPage();
            var favoritosPage = new FavouritesPage();
            var perfilPage = new ProfilePage();
            //var productsListPage = new ProductsListPage();

            Items.Add(new TabBar
            {
                Items =
            {
                new ShellContent { Title = "Home",Icon = "home",Content = homePage  },
                new ShellContent { Title = "ShoppingCart", Icon = "cart",Content = carrinhoPage },
                new ShellContent { Title = "Favourites",Icon = "heart",Content = favoritosPage },
                new ShellContent { Title = "Profile",Icon = "profile",Content = perfilPage }
            }
            });
        }
    }
}
