export function Load(): void {
    window['DraggableJs'] = new DraggableJsModule.DraggableJsClass();
}

export module DraggableJsModule {
    export class DraggableJsClass {
        constructor() {
        }

        mouseMoveHandlers = new WeakMap<Element, EventListener>();

        public async EnableMouseMove(elementRef: HTMLElement, dotNetRef: any): Promise<void> {
            if (this.mouseMoveHandlers.get(elementRef)) return;

            const mouseMoveHandler = (event: MouseEvent) => {
                dotNetRef.invokeMethodAsync('NotifyMouseMove', event.clientX, event.clientY);
            };

            elementRef.addEventListener('mousemove', mouseMoveHandler);

            this.mouseMoveHandlers.set(elementRef, mouseMoveHandler);
        }

        public async DisableMouseMove(elementRef: HTMLElement): Promise<void> {
            const mouseMoveHandler = this.mouseMoveHandlers.get(elementRef);
            if (mouseMoveHandler) {
                elementRef.removeEventListener('mousemove', mouseMoveHandler);
                this.mouseMoveHandlers.delete(elementRef);
            }
        }
    }
}

Load();