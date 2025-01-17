# RenderHtmlStringExtensions

*Namespace:* Nj.Blazor.Markdown
*Assembly:* CdCSharp.NjBlazor
*Source:* RenderHtmlStringExtensions.cs



    Contains extension methods for rendering HTML strings.
    
---

**Method:** `ProcessInlineItems`
*Method Signature:* `String ProcessInlineItems(String line)`


    Processes inline items in a given line of text.
    



**Method:** `RenderBlockQuote`
*Method Signature:* `RenderFragment RenderBlockQuote(String line)`


    Renders a blockquote element with the provided line as content.
    



**Method:** `RenderCodeBlock`
*Method Signature:* `RenderFragment RenderCodeBlock(IEnumerable lines, String language)`


    Renders a code block with syntax highlighting based on the specified language.
    



**Method:** `RenderHeader`
*Method Signature:* `RenderFragment RenderHeader(String line)`


    Renders a header component for a given line of text.
    



**Method:** `RenderParagraph`
*Method Signature:* `RenderFragment RenderParagraph(IEnumerable lines)`


    Renders a collection of strings as a paragraph in a Blazor component.
    



**Method:** `RenderUnorderedList`
*Method Signature:* `RenderFragment RenderUnorderedList(IEnumerable lines)`


    Renders an unordered list based on the provided lines.
    


