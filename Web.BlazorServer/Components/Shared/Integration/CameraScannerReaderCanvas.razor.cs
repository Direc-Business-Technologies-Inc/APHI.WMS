using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Web.BlazorServer.Components.Shared.Integration;

public partial class CameraScannerReaderCanvas
{
    [Parameter] required public string ScannedData { get; set; } = string.Empty;
    [Parameter] public EventCallback<string> ScannedDataChanged { get; set; }
    [Parameter] public EventCallback<string> UpdateScannedDataEvent { get; set; }
    [Parameter] public EventCallback<int> UpdateStateEvent { get; set; }

    [Inject] IJSRuntime JSRuntime { get; set; }
    IJSObjectReference _js;


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _js = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "../scripts/CustomScripts/MobileScanner.js");

            try
            {
                await _js.InvokeVoidAsync("setdotnetInstance", DotNetObjectReference.Create(this));
                await _js.InvokeVoidAsync("initializeScanner");
                StateHasChanged();

                await _js.InvokeVoidAsync("startScan");
            }
            catch (Exception)
            {
                await UpdateState(0);
            }
        }
    }

    [JSInvokable]
    public async Task UpdateState(int state)
    {
        await UpdateStateEvent.InvokeAsync(state);
    }

    [JSInvokable]
    public async Task UpdateScannedData(string data)
    {
        await UpdateScannedDataEvent.InvokeAsync(data);
    }
}
