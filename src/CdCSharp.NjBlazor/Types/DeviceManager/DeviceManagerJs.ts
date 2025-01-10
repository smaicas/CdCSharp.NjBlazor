export function Load(): void {
    window['DeviceManagerJs'] = new DeviceManagerJsModule.DeviceManagerJsClass();
}

export module DeviceManagerJsModule {
    export class DeviceManagerJsClass {
        constructor() {
        }

        public async GetWindowWidth(): Promise<number> {
            return window.innerWidth;
        }

        resizeCallbackEvents = new WeakMap<any, EventListener>();
        public async AddResizeCallback(dotnet: any, callbackName: string) {

            if (!this.resizeCallbackEvents.get(dotnet)) {
                let resizeTimer;
                const sendResizeCallback = (e) => {
                    clearTimeout(resizeTimer);
                    resizeTimer = setTimeout(function () {
                        dotnet.invokeMethodAsync(callbackName, window.innerWidth);
                    }, 500);
                }
                window.addEventListener("resize", sendResizeCallback);
                this.resizeCallbackEvents.set(dotnet, sendResizeCallback);
            }
        }
    }
}

Load();