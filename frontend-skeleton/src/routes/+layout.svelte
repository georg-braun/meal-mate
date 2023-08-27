<script lang="ts">
	// The ordering of these imports is critical to your app working properly
	import '@skeletonlabs/skeleton/themes/theme-skeleton.css';
	// If you have source.organizeImports set to true in VSCode, then it will auto change this ordering
	import '@skeletonlabs/skeleton/styles/skeleton.css';
	// Most of your app wide CSS should be put in this file
	import '../app.postcss';
	import { AppShell, AppBar, TabAnchor, TabGroup } from '@skeletonlabs/skeleton';
	import { page } from '$app/stores';
	import IconList from '~icons/mdi/format-list-bulleted-square';
	import IconSquareDotted from '~icons/mdi/dots-square';
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
				<IconList class="mx-auto" />
				<span class="iconify" data-icon="material-symbols:checklist-rounded" data-inline="false" />
			</svelte:fragment>
			<span>Einkaufsliste</span>
		</TabAnchor>
		<TabAnchor href="/template" selected={$page.url.pathname === '/template'}>
			<svelte:fragment slot="lead">
				<IconSquareDotted class="mx-auto" />
			</svelte:fragment>
			<span>Vorlagen</span>
		</TabAnchor>
		<!-- ... -->
	</TabGroup>
</AppShell>
