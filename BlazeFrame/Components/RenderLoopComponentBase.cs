using Microsoft.AspNetCore.Components;

namespace BlazeFrame.Components;

public abstract class RenderLoopComponentBase : ComponentBase, IDisposable
{
    public bool IsRunning { get; private set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && !IsRunning)
        {
            IsRunning = true;

            // Start the render loop after the first render
            await StartRenderLoop();
        }
    }

    protected async Task StartRenderLoop() 
    {
        while(IsRunning)
        {
            try{
                await Update();
            }catch (Exception) { }

            // Request a re-render
            await InvokeAsync(StateHasChanged);

            // Control the loop timing (e.g., 16ms for ~60FPS)
            await Task.Delay(16);
        }
    } 

    public void StopRenderLoop() => IsRunning = false; // Option to stop the loop

    protected abstract Task Update();

    public void Dispose() 
    {
        StopRenderLoop();
        GC.SuppressFinalize(this);
    }
}
