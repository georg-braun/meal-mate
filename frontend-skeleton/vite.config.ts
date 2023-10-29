import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vitest/config';
import Icons from 'unplugin-icons/vite';
import path from 'path';


export default defineConfig({
	plugins: [
		sveltekit(),
		Icons({
			compiler: 'svelte',
		})
	],
	test: {
		include: ['src/**/*.{test,spec}.{js,ts}']
	},
	resolve: {
		alias: [
			{ find: '@src', replacement: path.resolve(__dirname, './src'), },
		]
	}
});
