namespace MyDay2._0.Views;

public partial class SignUp : ContentPage
{
	public SignUp()
	{
		InitializeComponent();
        BindingContext = new ViewModels.SignUpViewModel();
    }
}