<script lang="ts">
	// The ordering of these imports is critical to your app working properly
	import '@skeletonlabs/skeleton/themes/theme-skeleton.css';
	// If you have source.organizeImports set to true in VSCode, then it will auto change this ordering
	import '@skeletonlabs/skeleton/styles/skeleton.css';
	// Most of your app wide CSS should be put in this file
	import '../app.postcss';
	import { AppShell, AppBar, TabAnchor, TabGroup } from '@skeletonlabs/skeleton';
	import { page } from '$app/stores';
</script>

<!-- App Shell -->
<AppShell>
	<svelte:fragment slot="header">
		<AppBar>
			<h2 class="h2">MealMate</h2>
		</AppBar>
	</svelte:fragment>
	<!-- Page Route Content -->
	<slot />
	<!-- Navigation -->
	<TabGroup
		justify="justify-center"
		active="variant-filled-primary"
		hover="hover:variant-soft-primary"
		flex="flex-1 lg:flex-none"
		rounded=""
		border=""
		class="bg-surface-100-800-token w-full sticky bottom-0"
	>
		<TabAnchor href="/list" selected={$page.url.pathname === '/list'}>
			<svelte:fragment slot="lead">
				<svg
					xmlns="http://www.w3.org/2000/svg"
					width="1em"
					height="1em"
					viewBox="0 0 24 24"
					{...$$props}
					><path
						fill="currentColor"
						d="m5.525 16.175l3.55-3.55q.3-.3.7-.288t.7.313q.275.3.275.7t-.275.7L6.25 18.3q-.3.3-.7.3t-.7-.3L2.7 16.15q-.275-.275-.275-.7t.275-.7q.275-.275.7-.275t.7.275l1.425 1.425Zm0-8l3.55-3.55q.3-.3.7-.288t.7.313q.275.3.275.7t-.275.7L6.25 10.3q-.3.3-.7.3t-.7-.3L2.7 8.15q-.275-.275-.275-.7t.275-.7q.275-.275.7-.275t.7.275l1.425 1.425ZM14 17q-.425 0-.713-.288T13 16q0-.425.288-.713T14 15h7q.425 0 .713.288T22 16q0 .425-.288.713T21 17h-7Zm0-8q-.425 0-.713-.288T13 8q0-.425.288-.713T14 7h7q.425 0 .713.288T22 8q0 .425-.288.713T21 9h-7Z"
					/></svg
				>
				<span class="iconify" data-icon="material-symbols:checklist-rounded" data-inline="false" />
			</svelte:fragment>
			<span>Einkaufsliste</span>
		</TabAnchor>
		<TabAnchor href="/template" selected={$page.url.pathname === '/template'}>
			<svelte:fragment slot="lead">
				<svg
					xmlns="http://www.w3.org/2000/svg"
					width="1em"
					height="1em"
					viewBox="0 0 48 48"
					{...$$props}
					><g fill="currentColor"
						><path
							d="M5 28a1 1 0 1 0 0 2h3a8 8 0 0 0 8 8h16a8 8 0 0 0 8-8h3a1 1 0 1 0 0-2H5Z"
						/><path
							fill-rule="evenodd"
							d="M23.105 10.053L24 10.5l.895.447l-.002.003l-.012.025l-.05.11a8.972 8.972 0 0 0-.6 1.861c-.236 1.179-.149 2.222.476 2.847c1.375 1.375 1.288 3.332 1.024 4.653a10.96 10.96 0 0 1-.806 2.438l-.02.043l-.007.013l-.002.005l-.001.001L24 22.5l-.895-.447l.002-.003l.012-.025l.05-.11a8.972 8.972 0 0 0 .6-1.861c.236-1.179.149-2.222-.476-2.847c-1.375-1.375-1.288-3.332-1.024-4.653a10.96 10.96 0 0 1 .806-2.438l.02-.043l.007-.013l.002-.004l.001-.002Zm1.79.893Zm-9.725 1.495l.83.56c.829.559.83.558.83.558v-.001v.001l-.013.02a6.128 6.128 0 0 0-.246.43a5.79 5.79 0 0 0-.45 1.127c-.24.892-.154 1.596.474 2.06c1.622 1.202 1.536 2.999 1.214 4.19a7.783 7.783 0 0 1-.605 1.528a8.037 8.037 0 0 1-.363.627l-.008.012l-.002.004l-.001.001s-.001.002-.83-.558s-.83-.559-.83-.559l.013-.019l.054-.086a6.82 6.82 0 0 0 .192-.344c.155-.298.333-.698.45-1.128c.24-.892.154-1.595-.474-2.06c-1.622-1.202-1.536-2.998-1.214-4.19c.165-.612.409-1.15.605-1.528a8.108 8.108 0 0 1 .363-.626l.007-.012l.003-.005l.002-.002Zm17 0l.83.56c.829.559.83.558.83.558v-.001v.001l-.013.02a6.128 6.128 0 0 0-.246.43a5.79 5.79 0 0 0-.45 1.127c-.24.892-.154 1.596.474 2.06c1.622 1.202 1.536 2.999 1.214 4.19a7.783 7.783 0 0 1-.605 1.528a8.037 8.037 0 0 1-.362.627l-.008.012l-.003.004l-.001.001s-.001.002-.83-.558s-.83-.559-.83-.559l.013-.019l.054-.086a6.82 6.82 0 0 0 .192-.344c.155-.298.333-.698.45-1.128c.24-.892.154-1.595-.474-2.06c-1.622-1.202-1.536-2.998-1.214-4.19c.165-.612.409-1.15.605-1.528a8.108 8.108 0 0 1 .362-.626l.008-.012l.003-.005l.002-.002Z"
							clip-rule="evenodd"
						/></g
					></svg
				>
			</svelte:fragment>
			<span>Vorlagen</span>
		</TabAnchor>
		<!-- ... -->
	</TabGroup>
</AppShell>
