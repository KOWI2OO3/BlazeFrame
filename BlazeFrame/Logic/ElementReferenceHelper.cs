using BlazeFrame.Element;
using Microsoft.AspNetCore.Components;

namespace BlazeFrame.Logic;

public static class ElementReferenceHelper
{
    public static async Task<BoundingClientRect> GetBoundingClientRect(this ElementReference element) => 
        await JSInvoker.INSTANCE.InvokeAsync<BoundingClientRect>(element, "getBoundingClientRect");
    
    public static async Task<ParentElement> GetParentElement(this ElementReference element) => 
        await JSInvoker.INSTANCE.InvokeAsync<ParentElement>(null, "getParentSize", element);

    public static async Task<T> GetAttribute<T>(this ElementReference element, string attribute) => 
        await JSInvoker.INSTANCE.GetPropertyAsync<T>(element, attribute);

    public static async Task SetAttribute<T>(this ElementReference element, string attribute, T value, bool attemptBatched = false) {
        if(!attemptBatched || !JSInvoker.INSTANCE.SetPropertyBatched(element, attribute, value))
            await JSInvoker.INSTANCE.SetPropertyAsync(element, attribute, value);
    }
    
    internal static async void SetAttributeProperty<T>(this ElementReference element, string attribute, T value, bool attemptBatched = false) => 
        await SetAttribute(element, attribute, value, attemptBatched);
}
