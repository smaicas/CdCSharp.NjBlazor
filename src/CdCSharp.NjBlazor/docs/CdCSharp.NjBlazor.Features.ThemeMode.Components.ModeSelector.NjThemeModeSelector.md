# NjThemeModeSelector

*Namespace:* CdCSharp.NjBlazor.Features.ThemeMode.Components.ModeSelector
*Assembly:* CdCSharp.NjBlazor
*Source:* NjThemeModeSelector.cs



    Represents a theme mode selector for the NjThemeModeSelectorBase class.
    De-multiplexer for NjThemeModeSelectorVariant
    
---

**Property:** `Variant` (Public)
*Property Type:* `NjThemeModeSelectorVariant`
*Default:* `NjThemeModeSelectorVariant.Flat`
*Nullable:* False
*Attributes:* [ParameterAttribute]


**Method:** `BuildRenderTree`
*Method Signature:* `Void BuildRenderTree(RenderTreeBuilder builder)`


**Method:** `OnInitialized`
*Method Signature:* `Void OnInitialized()`


**Method:** `OnParametersSet`
*Method Signature:* `Void OnParametersSet()`


**Method:** `OnAfterRender`
*Method Signature:* `Void OnAfterRender(Boolean firstRender)`


**Method:** `OnInitializedAsync`
*Method Signature:* `Task OnInitializedAsync()`


**Method:** `OnParametersSetAsync`
*Method Signature:* `Task OnParametersSetAsync()`


**Method:** `OnAfterRenderAsync`
*Method Signature:* `Task OnAfterRenderAsync(Boolean firstRender)`


**Method:** `.ctor`
*Method Signature:* `Void .ctor()`

---
## Inherited from NjThemeModeSelectorBase

**Summary:**

    Base class for a theme mode selector component.
    
---

**Property:** `IconSize` (Public)


    Gets or sets the size of the SVG icon.
    

*Property Type:* `NjSvgIconSize`
*Default:* `NjSvgIconSize.Medium`
*Nullable:* False
*Attributes:* [ParameterAttribute]


**Property:** `ThemeJs` (Public)

Gets or sets the JavaScript interop service for theming.

*Property Type:* `IThemeJsInterop`
*Nullable:* True
*Attributes:* [InjectAttribute]


**Property:** `DarkClass` (Protected)

Gets the CSS class for dark mode based on the IsDarkMode property.

*Property Type:* `String`
*Nullable:* False
*Attributes:* 


**Method:** `ToggleDarkMode`
*Method Signature:* `Task ToggleDarkMode()`


    Toggles the dark mode using the provided ThemeJs object.
    



**Method:** `.ctor`
*Method Signature:* `Void .ctor()`

---
## Inherited from NjComponentBase

**Summary:**

    Base class for Nj components that provides common functionality.
    
---

**Property:** `AdditionalAttributes` (Public)


    Gets or sets a collection of additional attributes that will be applied to the created element.
    

*Property Type:* `IReadOnlyDictionary`
*Nullable:* True
*Attributes:* [ParameterAttribute]


**Property:** `Class` (Public)


    Gets the css class from the additional attributes.
    

*Property Type:* `String`
*Nullable:* False
*Attributes:* 


**Method:** `AsClass`
*Method Signature:* `String AsClass( classes)`


    Concatenates an array of strings into a single string, separated by a space.
    Ignores empty strings.
    



**Method:** `DebounceEvent`
*Method Signature:* `Action DebounceEvent(Action action, TimeSpan interval)`


    Creates a debounced action for an event handler.
    



**Method:** `ThrottleEvent`
*Method Signature:* `Action ThrottleEvent(Action action, TimeSpan interval)`


    Throttles an event action to limit the rate at which it is invoked.
    



**Method:** `.ctor`
*Method Signature:* `Void .ctor()`

---
## Inherited from ComponentBase

**Summary:**

            Optional base class for components. Alternatively, components may
            implement [T:Microsoft.AspNetCore.Components.IComponent] directly.
            
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor()`


            Constructs an instance of [T:Microsoft.AspNetCore.Components.ComponentBase].
            



**Method:** `BuildRenderTree`
*Method Signature:* `Void BuildRenderTree(RenderTreeBuilder builder)`


            Renders the component to the supplied [T:Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder].
            



**Method:** `OnInitialized`
*Method Signature:* `Void OnInitialized()`


            Method invoked when the component is ready to start, having received its
            initial parameters from its parent in the render tree.
            



**Method:** `OnInitializedAsync`
*Method Signature:* `Task OnInitializedAsync()`


             Method invoked when the component is ready to start, having received its
             initial parameters from its parent in the render tree.
            
             Override this method if you will perform an asynchronous operation and
             want the component to refresh when that operation is completed.
             



**Method:** `OnParametersSet`
*Method Signature:* `Void OnParametersSet()`


            Method invoked when the component has received parameters from its parent in
            the render tree, and the incoming values have been assigned to properties.
            



**Method:** `OnParametersSetAsync`
*Method Signature:* `Task OnParametersSetAsync()`


            Method invoked when the component has received parameters from its parent in
            the render tree, and the incoming values have been assigned to properties.
            



**Method:** `StateHasChanged`
*Method Signature:* `Void StateHasChanged()`


            Notifies the component that its state has changed. When applicable, this will
            cause the component to be re-rendered.
            



**Method:** `ShouldRender`
*Method Signature:* `Boolean ShouldRender()`


            Returns a flag to indicate whether the component should render.
            



**Method:** `OnAfterRender`
*Method Signature:* `Void OnAfterRender(Boolean firstRender)`


             Method invoked after each time the component has rendered interactively and the UI has finished
             updating (for example, after elements have been added to the browser DOM). Any [T:Microsoft.AspNetCore.Components.ElementReference]
             fields will be populated by the time this runs.
            
             This method is not invoked during prerendering or server-side rendering, because those processes
             are not attached to any live browser DOM and are already complete before the DOM is updated.
             



**Method:** `OnAfterRenderAsync`
*Method Signature:* `Task OnAfterRenderAsync(Boolean firstRender)`


             Method invoked after each time the component has been rendered interactively and the UI has finished
             updating (for example, after elements have been added to the browser DOM). Any [T:Microsoft.AspNetCore.Components.ElementReference]
             fields will be populated by the time this runs.
            
             This method is not invoked during prerendering or server-side rendering, because those processes
             are not attached to any live browser DOM and are already complete before the DOM is updated.
            
             Note that the component does not automatically re-render after the completion of any returned [T:System.Threading.Tasks.Task],
             because that would cause an infinite render loop.
             



**Method:** `InvokeAsync`
*Method Signature:* `Task InvokeAsync(Action workItem)`


            Executes the supplied work item on the associated renderer's
            synchronization context.
            



**Method:** `InvokeAsync`
*Method Signature:* `Task InvokeAsync(Func workItem)`


            Executes the supplied work item on the associated renderer's
            synchronization context.
            



**Method:** `DispatchExceptionAsync`
*Method Signature:* `Task DispatchExceptionAsync(Exception exception)`


             Treats the supplied  as being thrown by this component. This will cause the
             enclosing ErrorBoundary to transition into a failed state. If there is no enclosing ErrorBoundary,
             it will be regarded as an exception from the enclosing renderer.
            
             This is useful if an exception occurs outside the component lifecycle methods, but you wish to treat it
             the same as an exception from a component lifecycle method.
             



**Method:** `SetParametersAsync`
*Method Signature:* `Task SetParametersAsync(ParameterView parameters)`


            Sets parameters supplied by the component's parent in the render tree.
            



**Property:** `RendererInfo` (Protected)


            Gets the [T:Microsoft.AspNetCore.Components.RendererInfo] the component is running on.
            

*Property Type:* `RendererInfo`
*Nullable:* False
*Attributes:* 


**Property:** `Assets` (Protected)


            Gets the [T:Microsoft.AspNetCore.Components.ResourceAssetCollection] for the application.
            

*Property Type:* `ResourceAssetCollection`
*Nullable:* False
*Attributes:* 


**Property:** `AssignedRenderMode` (Protected)


            Gets the [T:Microsoft.AspNetCore.Components.IComponentRenderMode] assigned to this component.
            

*Property Type:* `IComponentRenderMode`
*Nullable:* True
*Attributes:* [NullableAttribute]

