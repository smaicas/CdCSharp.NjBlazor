
# Estructuras JS Interop en Blazor

El método mas básico para interoperar con JS desde un componente Blazor lo provee el framework directamente.

Podemos **ejecutar código JS desde un componente Blazor** haciendo uso directo de IJSRuntime

```csharp
@inject IJSRuntime JsRuntime
[...]
JsRuntime.InvokeAsync<ReturnClass>("JSFunctionName", params);
JsRuntime.InvokeVoidAsync("JSProcedureName", params);
```
siempre y cuando hayamos definido y cargado el Javascript correspondiente:
```javascript
window["JSFunctionName"] = function(params) { 
[...]
window["JSProcedureName"] = function(params) { 
[...]
```
Como especifica la [documentación](https://learn.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/call-javascript-from-dotnet?view=aspnetcore-8.0)

Podemos **ejecutar un método estático de un componente Blazor desde Javascript** haciendo uso del atributo `JsInvokable` en el método del componente con el que queremos interoperar.

```csharp
[JSInvokable] public static Task{<T>}  {.NET METHOD ID}() { ... }
```
para luego desde Javascript
```Javascript
DotNet.invokeMethodAsync('{ASSEMBLY NAME}', '{.NET METHOD ID}', {ARGUMENTS});
```
Tal cual se encuentra en la [documentación](https://learn.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/call-dotnet-from-javascript?view=aspnetcore-8.0)

Cuando queremos  **ejecutar un método de la instancia del componente** es necesaria una referencia al mismo en el lado del Javascript.
```csharp
private DotNetObjectReference<MyComponent>? objRef; 
protected override void OnInitialized() { 
objRef = DotNetObjectReference.Create(this); 
}
```
Dependiendo de si el método "`JSInvokable`" va a actuar como "callback" a una llamada previa de C# a Javascript o no, podemos elegir una estrategia para enviar la referencia del componente (objRef).
* En el caso de un callback es obvio que podemos enviar el objeto en la primera llamada.
```csharp
...
JsRuntime.InvokeVoidAsync("FirstCall", objRef);
...
```
```Javascript
window.FirstCall = (dotNetHelper) => { 
return dotNetHelper.invokeMethodAsync('InstanceCsharpMethod'); 
};
```
* En otro caso podemos hacer una función de inicialización en JS y llamarla durante `OnAfterRender`
```csharp
protected override async Task OnAfterRenderdAsync(bool firstRender) { 
 JsRuntime.InvokeVoidAsync("Initialize", objRef);
}
```
```Javascript
function Load() {
  window["MyModule"] = new MyModule.MyJsClass();
}
var MyModule;
((MyModule2) => {
  class MyJsClass{
    async Initialize(dotnetRef) {
      this._dotNetRef = dotnetRef;
    }
    async CallBlazorComponentInstanceMethod(){
	    dotNetHelper.invokeMethodAsync('InstanceCsharpMethod');
	}
  }
  MyModule2.MyJsClass = MyJsClass;
})(MyModule || (MyModule = {}));
Load();
```
Ahora bien, existen otros mecanismos para interoperar con JS que, bajo mi punto de vista, generan una estructura del proyecto mas coherente.
En primer lugar es recomendable el uso de typescript (link a publicación Typescript)

Comenzaremos definiendo una interfaz que vamos a usar para interoperar con JS.
```csharp
public interface IThemeJsInterop
{
    /// <summary>
    /// Initializes Theme feature
    /// </summary>
    /// <param name="dotnetReference"></param>
    /// <returns></returns>
    Task InitializeAsync(DotNetObjectReference<NjThemeManager> dotnetReference);

    /// <summary>
    /// Toggles dark mode state. Returns current dark mode state.
    /// </summary>
    /// <returns></returns>
    Task ToggleDarkMode();
}
```
y la clase que la implementa
```csharp
public class ThemeJsInterop(IJSRuntime jsRuntime): IThemeJsInterop, IAsyncDisposable
{
	private readonly TaskCompletionSource<bool> _isModuleTaskLoaded = new(false);
	private readonly IJSRuntime _jsRuntime;
	private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

	public ThemeJsInterop(IJSRuntime jsRuntime)
	{
	    _jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
	    _moduleTask = new Lazy<Task<IJSObjectReference>>(() =>  
	    _jsRuntime.InvokeAsync<IJSObjectReference>("import", "_content/{{Namespace}}/moduleJs.js").AsTask());
	    _isModuleTaskLoaded.SetResult(true);
	}
	
    public async Task InitializeAsync(DotNetObjectReference<NjThemeManager> dotnetReference)
    {
        await _isModuleTaskLoaded.Task;
        await _moduleTask.Value;
        await _jsRuntime.InvokeVoidAsync("ThemeJs.InitializeAsync",
            dotnetReference);
    }

    public async Task ToggleDarkMode()
    {
        await _isModuleTaskLoaded.Task;
        await _moduleTask.Value;
        await _jsRuntime.InvokeVoidAsync("ThemeJs.ToggleDarkMode");
    }
    
    public async ValueTask DisposeAsync()
	{
	    if (_moduleTask.IsValueCreated)
	    {
	        IJSObjectReference module = await _moduleTask.Value;
	        await module.DisposeAsync();
	    }
	}
}
```
De esta manera podemos conseguir un diseño modular que directamente interopera con las definiciones que hagamos en Typescript. 
En el ejemplo:
```Javascript
export function Load(): void {
    window['ThemeJs'] = new ThemeModule.ThemeClass();
}

module ThemeModule {
    import DOMJsClass = DOMModule.DOMJsClass;
    const knownModeClasses: string[] = ['dark', 'light'];
    const storagePreferenceKey: string = 'darkModePreference';

    export class ThemeClass {       
        dotnet: any;
        public async InitializeAsync(
            dotnetReference: any)
            : Promise<void> {
            this.dotnet = dotnetReference;
            ...
        }

        public async ToggleDarkMode(): Promise<void> {
			...
        } 
    }
}

Load();
```
Faltaría inyectar la dependencia
```
...
service.AddTransient<IThemeJsInterop, ThemeJsInterop>();
...
```
Y utilizarla
```csharp 
@inject IThemeJsInterop JsThemeInterop
...
```
También a la hora de interoperar desde Javascript hacia C#, aunque hemos visto lo básico de la documentación, se puede hacer uso de un "Relay" que se encargue o facilite el procedimiento.

Comenzamos definiendo una interfaz de notificación al componente:
```csharp
internal interface ISomeBehaviorJsCallbacks
{
    Task NotifyChange(SomeClass paramClass);
}
```
Después definimos el propio "relay"
```csharp
internal sealed class MyComponentJsCallbacksRelay : IDisposable
{
    private readonly ISomeBehaviorJsCallbacks _callbacks;

    [DynamicDependency("NotifyChange")]
    public MyComponentJsCallbacksRelay(ISomeBehaviorJsCallbacks callbacks)
    {
        _callbacks = callbacks;
        DotNetReference = DotNetObjectReference.Create(this);
    }

    public IDisposable DotNetReference { get; }

    public void Dispose() => DotNetReference.Dispose();

    [JSInvokable]
    public Task NotifyChange(SomeClass paramClass) => _callbacks.NotifyChange(files);
}
```
E implementamos la interfaz en nuestro componente como callback. Definiendo el relay en `OnAfterRender`
```csharp
public class MyComponent : ComponentBase, ISomeBehaviorJsCallbacks 
{
    internal MyComponentJsCallbacksRelay _jsCallbacksRelay;
    protected override async Task OnAfterRenderAsync(bool firstRender)
	{
	    if (firstRender)
	    {
	        _jsCallbacksRelay = new MyComponentJsCallbacksRelay(this);
	    }
	}
	
	public async Task NotifyChange(SomeClass paramClass){
		...
		// JS return Callback - Do something with paramClass
	}
}
```
De este modo tenemos un objeto "relay" que se encarga de las notificaciones para un comportamiento específico. Heredable por otros componentes que pudieran requerirlo mas adelante.