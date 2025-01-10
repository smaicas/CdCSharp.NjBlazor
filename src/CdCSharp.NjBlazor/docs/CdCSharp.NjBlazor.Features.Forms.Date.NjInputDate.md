# NjInputDate

*Namespace:* CdCSharp.NjBlazor.Features.Forms.Date
*Assembly:* CdCSharp.NjBlazor
*Source:* NjInputDate.cs


---

**Property:** `Variant` (Public)
*Property Type:* `NjInputDateVariant`
*Default:* `NjInputDateVariant.Flat`
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
## Inherited from NjInputDateBase

**Summary:**

    Base component to create a date selector.
    
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor()`


    Constructs an instance of [T:CdCSharp.NjBlazor.Features.Forms.Date.NjInputDateBase`1]



**Property:** `DisplayFormat` (Public)

Gets or sets the display format for the parameter.

*Property Type:* `String`
*Nullable:* True
*Attributes:* [ParameterAttribute]


**Property:** `ParsingErrorMessage` (Public)

Gets or sets the parsing error message.

*Property Type:* `String`
*Default:* `string.Empty`
*Nullable:* False
*Attributes:* [ParameterAttribute]


**Property:** `Type` (Public)

Gets or sets the type of input for the date.

*Property Type:* `InputDateType`
*Default:* `InputDateType.Date`
*Nullable:* False
*Attributes:* [ParameterAttribute]


**Property:** `DomJs` (Protected)

Gets or sets the DOM JavaScript Interop service.

*Property Type:* `IDOMJsInterop`
*Default:* `default!`
*Nullable:* False
*Attributes:* [InjectAttribute]


**Method:** `FormatValueAsString`
*Method Signature:* `String FormatValueAsString(TValue value)`

Formats a nullable value as a string.



**Method:** `OnAfterRender`
*Method Signature:* `Void OnAfterRender(Boolean firstRender)`


    Method called after the component has been rendered.
    



**Method:** `OnChangeAsync`
*Method Signature:* `Task OnChangeAsync(ChangeEventArgs args)`


    Updates the current value as a string based on the provided change event arguments.
    



**Method:** `OnParametersSet`
*Method Signature:* `Void OnParametersSet()`


    Sets the parameters based on the specified input type.
    



**Method:** `OnFocusAsync`
*Method Signature:* `Task OnFocusAsync(FocusEventArgs focusEventArgs)`


    Asynchronously handles the focus event.
    



**Method:** `ShowPicker`
*Method Signature:* `Task ShowPicker()`


    Shows a picker asynchronously.
    



**Method:** `TryParseValueFromString`
*Method Signature:* `Boolean TryParseValueFromString(String value, TValue result, String validationErrorMessage)`


    Tries to parse a value from a string representation.
    



**Method:** `GetFormatTextFormat`
*Method Signature:* `String GetFormatTextFormat()`

Gets the formatted text based on the input date type.



**Method:** `GetDefaultText`
*Method Signature:* `String GetDefaultText()`


    Get the default text based on the input type.
    


---
## Inherited from NjInputBase

**Summary:**

    Base class for input components that handle user input of type TValue.
    
---

**Property:** `InputReference` (Protected)


    Gets or sets the associated [T:Microsoft.AspNetCore.Components.ElementReference].
    May be  if accessed before the component is rendered.

*Property Type:* `Nullable`
*Nullable:* True
*Attributes:* [DisallowNullAttribute]


**Property:** `InputReferenceId` (Protected)

Gets or sets the input reference ID.

*Property Type:* `String`
*Nullable:* True
*Attributes:* 


**Property:** `Class` (Public)

Gets or sets the CSS class for the component.

*Property Type:* `String`
*Default:* `string.Empty`
*Nullable:* False
*Attributes:* [ParameterAttribute]


**Property:** `Disabled` (Public)

Gets or sets a value indicating whether the component is disabled.

*Property Type:* `Boolean`
*Nullable:* False
*Attributes:* [ParameterAttribute]


**Property:** `FieldIdentifier` (Public)

Gets the field identifier.

*Property Type:* `FieldIdentifier`
*Nullable:* False
*Attributes:* 


**Method:** `AsClass`
*Method Signature:* `String AsClass( classes)`


    Concatenates non-empty strings in the 'classes' array to form a single string.
    



**Property:** `FormControl` (Public)

Gets or sets a value indicating whether the control is a form control.

*Property Type:* `Boolean`
*Nullable:* False
*Attributes:* [ParameterAttribute]


**Property:** `ReadOnly` (Public)

Gets or sets a value indicating whether the control is in read-only mode.

*Property Type:* `Boolean`
*Nullable:* False
*Attributes:* [ParameterAttribute]


**Property:** `EmptyClass` (Protected)


    Gets the CSS class for an empty class based on the IsEmpty property.
    

*Property Type:* `String`
*Nullable:* False
*Attributes:* 


**Property:** `FocusClass` (Protected)


    Gets the CSS class for focusing based on the focus state.
    

*Property Type:* `String`
*Nullable:* False
*Attributes:* 


**Property:** `IsEmpty` (Protected)

Determines if the current value is empty.

*Property Type:* `Boolean`
*Nullable:* False
*Attributes:* 


**Method:** `OnAfterRender`
*Method Signature:* `Void OnAfterRender(Boolean firstRender)`


    Method called after the component has been rendered.
    



**Method:** `OnFocusAsync`
*Method Signature:* `Task OnFocusAsync(FocusEventArgs focusEventArgs)`


    Handles the focus event asynchronously.
    



**Method:** `OnFocusOutAsync`
*Method Signature:* `Task OnFocusOutAsync(FocusEventArgs focusEventArgs)`


    Handles the asynchronous event when focus moves out from the element.
    



**Method:** `OnInputAsync`
*Method Signature:* `Task OnInputAsync(ChangeEventArgs changeEventArgs)`


    Handles input asynchronously.
    



**Method:** `TryParseValueFromString`
*Method Signature:* `Boolean TryParseValueFromString(String value, TValue result, String validationErrorMessage)`


    Tries to parse a value from a string representation.
    



**Method:** `.ctor`
*Method Signature:* `Void .ctor()`

---
## Inherited from InputBase

**Summary:**

            A base class for form input components. This base class automatically
            integrates with an [T:Microsoft.AspNetCore.Components.Forms.EditContext], which must be supplied
            as a cascading parameter.
            
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor()`


            Constructs an instance of [T:Microsoft.AspNetCore.Components.Forms.InputBase`1].
            



**Method:** `FormatValueAsString`
*Method Signature:* `String FormatValueAsString(TValue value)`


            Formats the value as a string. Derived classes can override this to determine the formatting used for [P:Microsoft.AspNetCore.Components.Forms.InputBase`1.CurrentValueAsString].
            



**Method:** `TryParseValueFromString`
*Method Signature:* `Boolean TryParseValueFromString(String value, TValue result, String validationErrorMessage)`


            Parses a string to create an instance of . Derived classes can override this to change how
            [P:Microsoft.AspNetCore.Components.Forms.InputBase`1.CurrentValueAsString] interprets incoming values.
            



**Method:** `SetParametersAsync`
*Method Signature:* `Task SetParametersAsync(ParameterView parameters)`




**Method:** `Dispose`
*Method Signature:* `Void Dispose(Boolean disposing)`




**Property:** `AdditionalAttributes` (Public)


            Gets or sets a collection of additional attributes that will be applied to the created element.
            

*Property Type:* `IReadOnlyDictionary`
*Nullable:* True
*Attributes:* [NullableAttribute], [ParameterAttribute]


**Property:** `Value` (Public)


            Gets or sets the value of the input. This should be used with two-way binding.
            

*Property Type:* `TValue`
*Nullable:* True
*Attributes:* [ParameterAttribute]


**Property:** `ValueChanged` (Public)


            Gets or sets a callback that updates the bound value.
            

*Property Type:* `EventCallback`
*Nullable:* False
*Attributes:* [NullableAttribute], [ParameterAttribute]


**Property:** `ValueExpression` (Public)


            Gets or sets an expression that identifies the bound value.
            

*Property Type:* `Expression`
*Nullable:* True
*Attributes:* [NullableAttribute], [ParameterAttribute]


**Property:** `DisplayName` (Public)


            Gets or sets the display name for this field.
            This value is used when generating error messages when the input value fails to parse correctly.

*Property Type:* `String`
*Nullable:* True
*Attributes:* [ParameterAttribute]


**Property:** `EditContext` (Protected)


            Gets the associated [T:Microsoft.AspNetCore.Components.Forms.EditContext].
            This property is uninitialized if the input does not have a parent [T:Microsoft.AspNetCore.Components.Forms.EditForm].
            

*Property Type:* `EditContext`
*Nullable:* False
*Attributes:* [NullableAttribute]


**Property:** `FieldIdentifier` (ProtectedOrInternal)


            Gets the [P:Microsoft.AspNetCore.Components.Forms.InputBase`1.FieldIdentifier] for the bound value.
            

*Property Type:* `FieldIdentifier`
*Nullable:* False
*Attributes:* 


**Property:** `CurrentValue` (Protected)


            Gets or sets the current value of the input.
            

*Property Type:* `TValue`
*Nullable:* True
*Attributes:* 


**Property:** `CurrentValueAsString` (Protected)


            Gets or sets the current value of the input, represented as a string.
            

*Property Type:* `String`
*Nullable:* True
*Attributes:* 


**Property:** `CssClass` (Protected)


            Gets a CSS class string that combines the class attribute and a string indicating
            the status of the field being edited (a combination of "modified", "valid", and "invalid").
            Derived components should typically use this value for the primary HTML element's 'class' attribute.
            

*Property Type:* `String`
*Nullable:* False
*Attributes:* [NullableAttribute]


**Property:** `NameAttributeValue` (Protected)


            Gets the value to be used for the input's "name" attribute.
            

*Property Type:* `String`
*Nullable:* False
*Attributes:* [NullableAttribute]

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

