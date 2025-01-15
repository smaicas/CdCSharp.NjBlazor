export function Load(): void {
    window['ColorPickerJs'] = new ColorPickerJsModule.ColorPickerJsClass();
}

export module ColorPickerJsModule {
    export class ColorPickerJsClass {
        constructor() {
        }

        public async RemoveRelativeBoundPosition(elementRef: HTMLElement, clientX: number, clientY: number): Promise<number[]> {
            if (!elementRef.getBoundingClientRect) { return; }

            const rect = elementRef.getBoundingClientRect();

            let x = Math.min(rect.width, Math.max(0, clientX - rect.left));
            let y = Math.min(rect.height, Math.max(0, clientY - rect.top))
            return [x, y]
        }

        public async RefreshHandlerPosition(elementRef: HTMLElement, handlerRef: HTMLCanvasElement, clientX: number, clientY: number) {
            if (!elementRef.getBoundingClientRect) { return; }

            const rect = elementRef.getBoundingClientRect();
            if (clientX) {
                handlerRef.style.left = Math.min(rect.width, Math.max(0, clientX)) + 'px';
            }
            if (clientY) {
                handlerRef.style.top = Math.min(rect.height, Math.max(0, clientY)) + 'px';
            }
        }
    }
}

Load();