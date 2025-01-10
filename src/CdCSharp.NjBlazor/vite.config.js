import { defineConfig } from 'vite';
import path from 'path';
import glob from 'glob';

// Obtener todos los archivos TypeScript de entrada
const inputFiles = glob.sync('./Types/**/[A-Z]*.ts').reduce((entries, path) => {
    const name = path.split('/').slice(-2)[0];
    entries[name] = path;
    return entries;
}, {});

export default defineConfig({
    resolve: {
        alias: {
            '@types': path.resolve(__dirname, 'Types')
        }
    },
    build: {
        outDir: path.resolve(__dirname, 'wwwroot'),
        emptyOutDir: false,
        rollupOptions: {
            input: inputFiles,
            output: {
                dir: path.resolve(__dirname, 'wwwroot/js'),
                entryFileNames: '[name].min.js', // Añadido .min para indicar que está minificado
                format: 'es',
                manualChunks: undefined
            }
        },
        target: 'esnext',
        sourcemap: true,
        minify: 'terser', // Usar terser para minificación
        terserOptions: {
            compress: {
                drop_console: true, // Elimina console.log
                drop_debugger: true // Elimina declaraciones debugger
            },
            format: {
                comments: false // Elimina comentarios
            }
        }
    }
});

//import { defineConfig } from 'vite';
//import path from 'path';

//export default defineConfig({
//    resolve: {
//        alias: {
//            '@types': path.resolve(__dirname, 'Types') // Ruta hacia la carpeta Types
//        }
//    },
//    build: {
//        outDir: path.resolve(__dirname, 'wwwroot'), // Directorio de salida en wwwroot
//        outFile: 'main.js',
//        rollupOptions: {
//            input: './Types/main.ts', // Specify the entry point for your project
//            output: {
//                entryFileNames: '[name].js', // Preserve entry file names
//                chunkFileNames: '[name].js', // Preserve chunk file names
//                assetFileNames: '[name][extname]', // Preserve asset file names
//                dir: path.resolve(__dirname, 'wwwroot/js') // Specify the full path of the output file
//            }
//        },
//    },
//    optimizeDeps: {
//        include: ['@types/**/*.ts'] // Incluir los archivos TypeScript dentro de la carpeta Types
//    }
//});