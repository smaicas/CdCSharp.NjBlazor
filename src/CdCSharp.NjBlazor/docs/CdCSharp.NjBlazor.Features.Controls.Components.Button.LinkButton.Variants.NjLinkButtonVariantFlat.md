# NjLinkButtonVariantFlat

*Namespace:* CdCSharp.NjBlazor.Features.Controls.Components.Button.LinkButton.Variants
*Assembly:* CdCSharp.NjBlazor
*Source:* Features_Controls_Components_Button_LinkButton_Variants_NjLinkButtonVariantFlat_razor.g.cs


---

**Method:** `BuildRenderTree`
*Method Signature:* `Void BuildRenderTree(RenderTreeBuilder __builder)`


**Method:** `.ctor`
*Method Signature:* `Void .ctor()`

---
## Inherited from NjLinkButtonBase

**Summary:**

    Base class for a link button component in the NjControl library.
    
---

**Property:** `BackgroundColor` (Public)

Gets or sets the background color in CSS format.

*Property Type:* `CssColor`
*Nullable:* True
*Attributes:* [ParameterAttribute]


**Property:** `Color` (Public)

Gets or sets the color value in CSS format.

*Property Type:* `CssColor`
*Nullable:* True
*Attributes:* [ParameterAttribute]


**Property:** `PostAdornment` (Public)

Gets or sets the post-adornment string.

*Property Type:* `String`
*Nullable:* True
*Attributes:* [ParameterAttribute]


**Property:** `PostAdornmentColor` (Public)

Gets or sets the color for the post-adornment.

*Property Type:* `CssColor`
*Nullable:* True
*Attributes:* [ParameterAttribute]


**Property:** `PreAdornment` (Public)

Gets or sets the string to be displayed before the main content.

*Property Type:* `String`
*Nullable:* True
*Attributes:* [ParameterAttribute]


**Property:** `PreAdornmentColor` (Public)

Gets or sets the color of the pre-adornment.

*Property Type:* `CssColor`
*Nullable:* True
*Attributes:* [ParameterAttribute]


**Property:** `Padding` (Public)

Gets or sets the padding value.

*Property Type:* `Int32`
*Nullable:* False
*Attributes:* [ParameterAttribute]


**Property:** `Text` (Public)

Gets or sets the text value with a specified transformation.

*Property Type:* `String`
*Nullable:* False
*Attributes:* [ParameterAttribute]


**Property:** `Href` (Public)

Gets or sets the href attribute value.

*Property Type:* `String`
*Default:* `"#"`
*Nullable:* False
*Attributes:* [ParameterAttribute]


**Property:** `Target` (Public)

Gets or sets the target for the link button.

*Property Type:* `NjLinkButtonTarget`
*Default:* `NjLinkButtonTarget.Self`
*Nullable:* False
*Attributes:* [ParameterAttribute]


**Property:** `TextTransform` (Public)

Gets or sets the function used to transform text.

*Property Type:* `Func`
*Default:* `(s) => s.ToUpper()`
*Nullable:* False
*Attributes:* [ParameterAttribute]


**Property:** `LinkTarget` (Protected)

Gets the target attribute value for a link based on the specified target type.

*Property Type:* `String`
*Nullable:* False
*Attributes:* 


**Property:** `FocusClass` (Protected)

Gets the CSS class based on the focus state.

*Property Type:* `String`
*Nullable:* False
*Attributes:* 


**Property:** `InlineStyle` (Protected)

Gets the inline style for the element.

*Property Type:* `String`
*Nullable:* False
*Attributes:* 


**Method:** `OnFocusAsync`
*Method Signature:* `Task OnFocusAsync(FocusEventArgs focusEventArgs)`


    Handles the focus event asynchronously.
    



**Method:** `OnFocusOutAsync`
*Method Signature:* `Task OnFocusOutAsync(FocusEventArgs focusEventArgs)`


    Handles the asynchronous event when focus moves out from the element.
    



**Method:** `.ctor`
*Method Signature:* `Void .ctor()`

---
## Inherited from NjControlComponentBase

**Summary:**

    Base class for control components in the Nj framework.
    
---

**Property:** `Disabled` (Public)

Gets or sets a value indicating whether the component is disabled.

*Property Type:* `Boolean`
*Nullable:* False
*Attributes:* [ParameterAttribute]


**Property:** `FormControl` (Public)

Gets or sets a value indicating whether the control is a form control.

*Property Type:* `Boolean`
*Nullable:* False
*Attributes:* [ParameterAttribute]


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

