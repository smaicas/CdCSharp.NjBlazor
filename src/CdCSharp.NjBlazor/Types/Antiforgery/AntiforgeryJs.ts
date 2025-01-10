export function Load(): void {
    window['AntiforgeryJs'] = new AntiforgeryModule.AntiforgeryJsClass();
}

export module AntiforgeryModule {
    export class AntiforgeryJsClass {
        constructor() {
        }

        public async GetAntiForgeryToken() {
            var elements = document.getElementsByName('__RequestVerificationToken');
            if (elements.length > 0) {
                //return elements[0].value
                return elements[0].nodeValue
            }

            console.warn('no anti forgery token found!');
            return null;
        }

        public async GetNonce(): Promise<string> {
            var elements = document.getElementsByName('__Nonce');
            if (elements.length > 0) {
                return elements[0].attributes["value"].nodeValue
            }

            console.warn('no nonce token found!');
            return null;
        }
    }
}

Load();