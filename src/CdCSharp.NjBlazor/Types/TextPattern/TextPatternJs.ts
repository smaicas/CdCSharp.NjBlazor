export function Load(): void {
    window['TextPatternJs'] = new TextPatternModule.TextPatternJsClass();
}

export module TextPatternModule {
    export class TextPatternJsClass {
        constructor() {
        }

        textPatternClickEvents = new WeakMap<HTMLSpanElement, EventListener>();
        textPatternInputEvents = new WeakMap<HTMLSpanElement, EventListener>();
        textPatternBlurEvents = new WeakMap<HTMLSpanElement, EventListener>();
        public async TextPatternAddDynamic(
            containerBox: HTMLDivElement,
            elements: Array<ElementPattern>,
            dotnet: any,
            dotnetNotifyChangeTextCallback: string,
            dotnetValidatePartialCallback: string): Promise<void> {
            if (!containerBox.appendChild) { return; }

            containerBox.innerHTML = '';
            this.textPatternClickEvents = new WeakMap<HTMLSpanElement, EventListener>();
            this.textPatternInputEvents = new WeakMap<HTMLSpanElement, EventListener>();
            this.textPatternBlurEvents = new WeakMap<HTMLSpanElement, EventListener>();

            for (let index: number = 0; index < elements.length; index++) {
                let element: ElementPattern = elements[index];
                let span: HTMLSpanElement = document.createElement('span');

                span.innerText = element.value;
                if (element.isSeparator) {
                    containerBox.appendChild(span);
                    continue;
                }

                if (element.isEditable) {
                    span.setAttribute('contenteditable', 'true');
                    this.addTextPatternEvents(span, index, containerBox, element, dotnet, dotnetNotifyChangeTextCallback, dotnetValidatePartialCallback);
                }
                containerBox.appendChild(span);
            }
        }

        private addTextPatternEvents = (
            span: HTMLSpanElement,
            index: number,
            containerBox: HTMLDivElement,
            elementPattern: ElementPattern,
            dotnet: any,
            dotnetNotifyChangeTextCallback: string,
            dotnetValidatePartialCallback: string) => {
            const selectTextOnClick = () => this.selectTextOnClick(span);
            if (!this.textPatternClickEvents.get(span)) {
                span.addEventListener("click", selectTextOnClick);
                this.textPatternClickEvents.set(span, selectTextOnClick);
            }

            const goNextOrPrevent = () =>
                this.goNextOrPrevent(
                    span,
                    index,
                    containerBox,
                    elementPattern,
                    dotnet,
                    dotnetNotifyChangeTextCallback,
                    dotnetValidatePartialCallback);
            if (!this.textPatternInputEvents.get(span)) {
                span.addEventListener("input", goNextOrPrevent);
                this.textPatternInputEvents.set(span, goNextOrPrevent);
            }

            const setDefaultValueNotLength = () => this.setDefaultValueNotLength(span, elementPattern);
            if (!this.textPatternBlurEvents.get(span)) {
                span.addEventListener("blur", setDefaultValueNotLength);
                this.textPatternBlurEvents.set(span, setDefaultValueNotLength);
            }
        }
        private selectTextOnClick = (span: HTMLSpanElement) => {
            const range = document.createRange();
            range.selectNodeContents(span);
            const selection = window.getSelection();
            selection.removeAllRanges();
            selection.addRange(range);
        };

        private goNextOrPrevent = (
            span: HTMLSpanElement,
            index: number,
            containerBox: HTMLDivElement,
            elementPattern: ElementPattern,
            dotnet: any,
            dotnetCallback: string,
            dotnetValidatePartialCallback: string) => {
            console.log(containerBox.innerHTML);

            if (span.innerText.length == 0) {
                span.innerText = elementPattern.defaultValue;
                if (dotnet) {
                    dotnet.invokeMethodAsync(dotnetCallback, containerBox.innerText);
                }
                return;
            }

            if (span.innerText.length <= elementPattern.length) {
                let cursor = this.getCursorPositionWithinSpan(span);
                let flattenedPattern = elementPattern.pattern
                    .replace(/[\\\(\)\^\$]/g, "");
                let text = span.innerText;
                let splittedPattern = flattenedPattern.substring(0, text.length).split('');

                let validText = '';
                for (let chIndex = 0; chIndex < text.length; chIndex++) {
                    let valid;
                    if (splittedPattern[chIndex] === 'w') {
                        valid = new RegExp('[a-zA-Z]', 'g').test(text[chIndex]);
                    }
                    else if (splittedPattern[chIndex] === 'd') {
                        valid = new RegExp('[0-9]', 'g').test(text[chIndex]);
                    }
                    if (valid) {
                        validText += text[chIndex];
                    } else {
                        cursor = cursor - 1;
                    }
                }
                span.innerText = validText;
                this.setCursorPositionWithinSpan(span, cursor);

                if (span.innerText.length >= elementPattern.length) {
                    const nextBlock = this.findNextEditableBlock(containerBox, span);
                    if (nextBlock) {
                        nextBlock.click();
                    }
                }
                if (dotnet) {
                    dotnet.invokeMethodAsync(dotnetCallback, containerBox.innerText);
                }

                if (span.innerText.length === elementPattern.length) {
                    dotnet.invokeMethodAsync(dotnetValidatePartialCallback, index, span.innerText).then(valid => {
                        if (!valid) {
                            span.innerText = elementPattern.defaultValue;
                        }
                    });
                }
            } else {
                span.innerText = span.innerText.substring(0, elementPattern.length);
                this.placeCaretAtEnd(span);
                if (dotnet) {
                    dotnet.invokeMethodAsync(dotnetCallback, containerBox.innerText);
                }
            }
        };

        private setDefaultValueNotLength = (span: HTMLSpanElement, elementPattern: ElementPattern) => {
            if (span.innerText.length != elementPattern.length) {
                span.innerText = elementPattern.defaultValue;
                return;
            }
        }

        private getCursorPositionWithinSpan = (spanElement: HTMLElement): number => {
            let cursorPosition = 0;

            if (window.getSelection) {
                let selection = window.getSelection();
                if (selection && selection.rangeCount > 0) {
                    let range = selection.getRangeAt(0);
                    let preSelectionRange = range.cloneRange();
                    preSelectionRange.selectNodeContents(spanElement);
                    preSelectionRange.setEnd(range.startContainer, range.startOffset);
                    cursorPosition = preSelectionRange.toString().length;
                }
            }

            return cursorPosition;
        }

        private setCursorPositionWithinSpan = (spanElement: HTMLElement, position: number): void => {
            let range = document.createRange();
            let selection = window.getSelection();

            if (selection) {
                range.setStart(spanElement.childNodes[0] || spanElement, position);
                range.collapse(true);
                selection.removeAllRanges();
                selection.addRange(range);
            }
        }
        private findNextEditableBlock = (containerBox: HTMLDivElement, current: HTMLSpanElement): HTMLElement | null => {
            let foundCurrent = false;
            for (const child of containerBox.children) {
                if (child === current) {
                    foundCurrent = true;
                } else if (foundCurrent && child instanceof HTMLSpanElement && child.contentEditable === "true") {
                    return child;
                }
            }
            return null;
        }

        private placeCaretAtEnd = (el: HTMLElement) => {
            const range = document.createRange();
            const selection = window.getSelection();

            range.selectNodeContents(el);
            range.collapse(false);

            if (selection) {
                selection.removeAllRanges();
                selection.addRange(range);
            }
        }
    }
}

interface ElementPattern {
    pattern: string;
    value: string;
    length: number;
    defaultValue: string;
    isSeparator: boolean;
    isEditable: boolean;
}

Load();