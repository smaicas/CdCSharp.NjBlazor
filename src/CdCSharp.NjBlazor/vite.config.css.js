import { defineConfig } from 'vite';
import path from 'path';

export default defineConfig({
    build: {
        minify: 'terser',
        // outFileName: 'njblazor.css',
        // outFile: 'njblazor.css',
        outDir: 'wwwroot/css',
        assetsDir: '',
        rollupOptions: {
            input: './CssBundle/main.css',
        },
        output: {
            // Remove conflicting entryFileNames
            // chunkFileNames: '[name].css', // Preserve chunk file names (if needed)
            // assetFileNames: '[name][extname]', // Preserve asset file names (if needed)
            dir: path.resolve(__dirname, 'wwwroot/css'), // Specify output directory
        },
    },
});