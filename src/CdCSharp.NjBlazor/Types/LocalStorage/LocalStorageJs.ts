/**
 * Function to load and initialize the LocalStorage module in the window object.
 */
export function Load(): void {
    window['LocalStorageJs'] = new LocalStorageJsModule.LocalStorageJsClass();
}

/**
 * Module containing LocalStorage functionality.
 */
export module LocalStorageJsModule {
    /**
     * Class that handles LocalStorage operations.
     */
    export class LocalStorageJsClass {
        /**
         * Initializes a new instance of the LocalStorageJsClass.
         */
        constructor() {
        }

        /**
         * Sets an item in the localStorage with the specified key and value.
         * @param key The key to store the value under.
         * @param value The value to store.
         * @returns A promise that resolves when the operation is complete.
         */
        public async SetItemAsync(key: string, value: string): Promise<void> {
            try {
                localStorage.setItem(key, value);
            } catch (error) {
                console.error('Error setting localStorage item:', error);
                throw error;
            }
        }

        /**
         * Gets an item from localStorage by key.
         * @param key The key of the item to retrieve.
         * @returns A promise that resolves with the value, or null if not found.
         */
        public async GetItemAsync(key: string): Promise<string | null> {
            try {
                return localStorage.getItem(key);
            } catch (error) {
                console.error('Error getting localStorage item:', error);
                throw error;
            }
        }

        /**
         * Removes an item from localStorage by key.
         * @param key The key of the item to remove.
         * @returns A promise that resolves when the operation is complete.
         */
        public async RemoveItemAsync(key: string): Promise<void> {
            try {
                localStorage.removeItem(key);
            } catch (error) {
                console.error('Error removing localStorage item:', error);
                throw error;
            }
        }

        /**
         * Clears all items from localStorage.
         * @returns A promise that resolves when the operation is complete.
         */
        public async ClearAsync(): Promise<void> {
            try {
                localStorage.clear();
            } catch (error) {
                console.error('Error clearing localStorage:', error);
                throw error;
            }
        }

        /**
         * Gets all keys that start with a specific prefix.
         * @param prefix The prefix to search for.
         * @returns A promise that resolves with an array of matching keys.
         */
        public async GetKeysByPrefixAsync(prefix: string): Promise<string[]> {
            try {
                const keys: string[] = [];
                for (let i = 0; i < localStorage.length; i++) {
                    const key = localStorage.key(i);
                    if (key && key.startsWith(prefix)) {
                        keys.push(key);
                    }
                }
                return keys;
            } catch (error) {
                console.error('Error getting keys by prefix:', error);
                throw error;
            }
        }

        /**
         * Clears all items from localStorage that start with a specific prefix.
         * @param prefix The prefix of the keys to clear.
         * @returns A promise that resolves when the operation is complete.
         */
        public async ClearByPrefixAsync(prefix: string): Promise<void> {
            try {
                const keys = await this.GetKeysByPrefixAsync(prefix);
                for (const key of keys) {
                    localStorage.removeItem(key);
                }
            } catch (error) {
                console.error('Error clearing items by prefix:', error);
                throw error;
            }
        }
    }
}

/**
 * Initialize the module when loaded.
 */
Load();