export function Load(): void {
    window['LocalizationJs'] = new LocalizationModule.LocalizationJsClass();
}

export module LocalizationModule {
    export class LocalizationJsClass {
        public async Get(): Promise<string> {
            return window.localStorage['nj-culture'];
        };

        public async Set(value: any): Promise<void> {
            window.localStorage['nj-culture'] = value
        }
    }
}

Load();