export function Load(): void {
    window['ThemeJs'] = new ThemeModule.ThemeClass();
}

module ThemeModule {
    const storagePreferenceKey: string = 'nj-darkmode';

    export class ThemeClass {
        constructor() {
        }

        public async InitializeAsync()
            : Promise<void> {
            let localStorageIsDarkMode: boolean | null = await this.LocalStorageIsDarkMode();

            if (localStorageIsDarkMode !== null) {
                await this.SetDarkMode(localStorageIsDarkMode);
                return;
            }

            let userPreferenceIsDarkMode: boolean | null = await this.UserPreferenceIsDarkMode();

            if (userPreferenceIsDarkMode !== null) {
                await this.SetDarkMode(userPreferenceIsDarkMode);
                return;
            }

            await await this.SetDarkMode(false);
        }

        public async SetDarkMode(isDark: boolean): Promise<void> {
            document.documentElement.classList.remove('dark');
            document.documentElement.classList.remove('light');
            document.documentElement.classList.add(isDark ? 'dark' : 'light');
            localStorage.setItem(storagePreferenceKey, isDark.toString());
        }

        public async ToggleDarkMode(): Promise<boolean> {
            let darkMode = !(await this.IsDarkMode());
            this.SetDarkMode(darkMode);
            return darkMode;
        }

        public async IsDarkMode(): Promise<boolean> {
            let isDark = await this.LocalStorageIsDarkMode();
            return await this.LocalStorageIsDarkMode();
        }

        private async LocalStorageIsDarkMode(): Promise<boolean | null> {
            const localStoragePreference: string = localStorage.getItem(storagePreferenceKey);
            if (!localStoragePreference) return null;
            const isDark: boolean = localStoragePreference === 'true';
            return isDark;
        }
        private async UserPreferenceIsDarkMode(): Promise<boolean | null> {
            const userPrefersDark: boolean = window.matchMedia
                && window.matchMedia('(prefers-color-scheme: dark)').matches;
            return userPrefersDark;
        }
    }
}

Load();