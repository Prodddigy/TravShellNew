using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
namespace RazorComponents;

public class CustomCssClassProvider<ProviderType> : ComponentBase where ProviderType : FieldCssClassProvider, new()
{
    [CascadingParameter]
    EditContext? CurrentEditContext { get; set; }
    public ProviderType Provider { get; set; } = new ProviderType();
    protected override void OnInitialized()
    {
        if (CurrentEditContext == null)
        {
            throw new InvalidOperationException($"{nameof(CustomCssClassProvider <ProviderType>)} wymaga kaskadowego parametru typu {nameof(EditContext)}. Możesz na przykład wewnątrz EditForm użyć {nameof(CustomCssClassProvider<ProviderType>)}.");
        }
        CurrentEditContext.SetFieldCssClassProvider
         (Provider);
    }
}
