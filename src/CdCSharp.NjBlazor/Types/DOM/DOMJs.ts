export function Load(): void {
    window['DOMJs'] = new DOMModule.DOMJsClass();
}

export module DOMModule {
    export class DOMJsClass {
        constructor() {
        }

        public async SetCssVariable(variableName: string, value: string): Promise<void> {
            document.documentElement.style.setProperty(variableName, value);
        };

        public async GetCssVariable(variableName: string): Promise<string> {
            const rootStyles = getComputedStyle(document.documentElement);
            return rootStyles.getPropertyValue(variableName);
        }

        public async SaveLocalStorage(key: string, value: string): Promise<void> {
            localStorage.setItem(key, value);
        }

        public async GetLocalStorage(key: string): Promise<string> {
            return localStorage.getItem(key);
        }

        public async SelectText(element: HTMLDivElement): Promise<void> {
            const range = document.createRange();
            range.selectNodeContents(element);
            const selection = window.getSelection();
            selection.removeAllRanges();
            selection.addRange(range);
        }

        public async GetCoordsRelative(
            relativeToElement: HTMLElement,
            positioning: string): Promise<Array<number>> {
            let result = [0, 0, 0, 0];

            const height = relativeToElement.getBoundingClientRect().height;
            const width = relativeToElement.getBoundingClientRect().width;

            switch (positioning) {
                case "center":
                    return [0, 0, 0, 0];

                case "top":
                    return [0, 0, height, 0];

                case "right":
                    return [0, 0, 0, width];

                case "bottom":
                    return [height, 0, 0, 0];

                case "left":
                    return [0, width, 0, 0];

                case "topleft":
                    return [0, width, height, 0];

                case "topright":
                    return [0, 0, height, width];

                case "bottomleft":
                    return [height, width, 0, 0];

                case "bottomright":
                    return [height, 0, 0, width];
            }

            return result;
        }

        public async GetElementBounds(
            elementQuery: string): Promise<Array<number>> {
            const element = document.querySelector(elementQuery);

            if (!element) return [0, 0];

            const rect = element.getBoundingClientRect();

            const height = rect.height;
            const width = rect.width;

            return [width, height];
        }

        public async SetCalendarDatepickerValue(calendarInputRef: HTMLInputElement, value: string): Promise<void> {
            if (!calendarInputRef) return;

            calendarInputRef.value = value;
        }

        public async AddShowPickerEventHandler(clickElement: HTMLElement, inputCalendarElement: HTMLInputElement): Promise<void> {
            if (clickElement.dataset.showPickerListenerAttached === "true") {
                return;
            }
            clickElement.dataset.showPickerListenerAttached = "true";
            clickElement.addEventListener("click", () => {
                if (inputCalendarElement.showPicker) {
                    inputCalendarElement.showPicker();
                }
            });
        }

        textPatternClickEvents = new WeakMap<HTMLSpanElement, EventListener>();
        textPatternInputEvents = new WeakMap<HTMLSpanElement, EventListener>();
        textPatternBlurEvents = new WeakMap<HTMLSpanElement, EventListener>();
        public async TextPatternAddDynamic(containerBox: HTMLDivElement, elements: Array<ElementPattern>): Promise<void> {
            containerBox.innerHTML = '';
            this.textPatternClickEvents = new WeakMap<HTMLSpanElement, EventListener>();
            this.textPatternInputEvents = new WeakMap<HTMLSpanElement, EventListener>();
            this.textPatternBlurEvents = new WeakMap<HTMLSpanElement, EventListener>();

            for (let element of elements) {
                let span = document.createElement('span');
                span.innerText = element.value;

                if (element.isSeparator) {
                    containerBox.appendChild(span);
                    continue;
                }

                if (element.isEditable) {
                    span.setAttribute('contenteditable', 'true');
                    this.addTextPatternEvents(span, containerBox, element);
                }
                containerBox.appendChild(span);
            }
        }

        private addTextPatternEvents = (span: HTMLSpanElement,
            containerBox: HTMLDivElement,
            elementPattern: ElementPattern) => {
            const selectTextOnClick = () => this.selectTextOnClick(span);
            if (!this.textPatternClickEvents.get(span)) {
                span.addEventListener("click", selectTextOnClick);
                this.textPatternClickEvents.set(span, selectTextOnClick);
            }

            const goNextOrPrevent = () => this.goNextOrPrevent(span, containerBox, elementPattern);
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

        private goNextOrPrevent = (span: HTMLSpanElement, containerBox: HTMLDivElement, elementPattern: ElementPattern) => {
            if (span.innerText.length == 0) {
                span.innerText = elementPattern.defaultValue;
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
            } else {
                span.innerText = span.innerText.substring(0, elementPattern.length);
                this.placeCaretAtEnd(span);
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

        public async FocusElement(querySelector: string, parentElement?: HTMLElement | null): Promise<void> {
            console.log("Focused")
            if (parentElement) {
                (parentElement.querySelector(querySelector) as HTMLElement)?.focus();
            } else {
                (document.querySelector(querySelector) as HTMLElement)?.focus();
            }
        }

        public async GetFocusedElementClass(): Promise<string> {
            const focusedElement = document.activeElement;
            return focusedElement.classList.toString()
        }

        inputFileHandlers = new WeakMap<Element, EventListener>();
        public async InputFileInitializeCallbacks(dotNetRef: any, elementRef: HTMLInputElement): Promise<void> {
            if (this.inputFileHandlers.get(elementRef)) return;

            const fileChangeHandler = (event: InputEvent) => {
                const files: FileList | null = elementRef.files;
                if (files) {
                    let sendFiles: IBrowserFile[] = [];
                    for (let i = 0; i < files.length; i++) {
                        let f = files.item(i);
                        sendFiles.push({
                            id: i,
                            name: f.name,
                            size: f.size,
                            contentType: f.type,
                            lastModified: new Date(f.lastModified).toISOString(),
                        });
                    }
                    dotNetRef.invokeMethodAsync("NotifyChange", sendFiles);
                };
            };

            elementRef.addEventListener('change', fileChangeHandler);

            this.inputFileHandlers.set(elementRef, fileChangeHandler);
        }

        public async ReadFileData(elementRef: HTMLInputElement, fileId: number): Promise<ArrayBuffer> {
            const file = elementRef.files.item(fileId);
            const fileData = await this.readFileStreamAsync(file);
            return fileData;
        }

        public async ReadImageData(elementRef: HTMLInputElement, fileId: number): Promise<string> {
            const file = elementRef.files.item(fileId);
            const fileData = await this.readFileDataAsync(file);
            return fileData;
        }

        private async readFileDataAsync(file: File): Promise<string> {
            return new Promise<string>((resolve, reject) => {
                const reader = new FileReader();
                reader.onload = () => {
                    resolve(reader.result as string);
                };
                reader.onerror = (error) => {
                    reject(error);
                };
                reader.readAsDataURL(file);
            });
        }

        private async readFileStreamAsync(file: File): Promise<ArrayBuffer> {
            return new Promise<ArrayBuffer>((resolve, reject) => {
                const reader = new FileReader();
                reader.onload = () => {
                    resolve(reader.result as ArrayBuffer);
                };
                reader.onerror = (error) => {
                    reject(error);
                };
                reader.readAsArrayBuffer(file);
            });
        }

        public async SetDisabled(element: HTMLInputElement, value: boolean): Promise<void> {
            element.disabled = value;
        }

        public async ScrollToClosest(querySelector: string, element?: HTMLElement | null): Promise<void> {
            let closest: HTMLElement | null = null;

            if (element) {
                closest = element.matches(querySelector) ? element : element.closest(querySelector);
            } else {
                closest = document.querySelector(querySelector) as HTMLElement | null;
            }

            if (closest) {
                closest.scrollIntoView({ behavior: 'smooth', block: 'start' });
            }
        }

        public async ScrollTop(parentElement?: HTMLElement | null, position?: number) {
            position ??= 0;
            if (parentElement) {
                parentElement.scrollTop = position;
            } else {
                document.querySelector("body").scrollTop = position;
            }
        }

        public async DownloadFile(filename: string,
            content: string,
            contentType: string = 'text/plain')
            : Promise<void> {
            const blob: Blob = new Blob([content], { type: contentType });
            const url: string = window.URL.createObjectURL(blob);
            const a: HTMLAnchorElement = document.createElement('a');
            a.href = url;
            a.download = filename;
            document.body.appendChild(a);
            a.click();
            window.URL.revokeObjectURL(url);
            document.body.removeChild(a);
        }
    }
}
interface ICssVariable {
    Name: string;
    Value: string;
    FullName: string;
    Prefix: string;
}
interface IBrowserFile {
    id: number;
    name: string;
    size: number;
    contentType: string;
    lastModified: string;
}

interface ElementPattern {
    pattern: string;
    value: string;
    length: number;
    defaultValue: string;
    isSeparator: boolean;
    isEditable: boolean;
}

export class CssVariable implements ICssVariable {
    Name: string;
    Value: string;
    FullName: string;
    Prefix: string;

    constructor(data: ICssVariable) {
        this.Name = data.Name;
        this.Value = data.Value;
        this.FullName = data.FullName;
        this.Prefix = data.Prefix;
    }

    static fromJsonArray(jsonArray: any[]): CssVariable[] {
        return jsonArray.map((jsonObject) => new CssVariable(jsonObject));
    }
}

Load();