<script lang="ts">
	import { onMount } from 'svelte';
	import { getList } from './api/getList';
	import type { List } from '@src/types/List';
	import { page } from '$app/stores';
	import { createEntry } from './api/createEntry';

	let id = $page.params.id;

	let list: List;
	let newEntryName: string;

	onMount(async () => {
		console.log($page);
		let response = await getList(id);
		list = {
			name: response.name,
			id: response.id,
			entries: response.entries ?? []
		};
	});
</script>

{#if list}
	{#each list.entries as entry (entry.id)}
		{entry.itemName}
	{/each}
{/if}

<label class="label">
	<input class="input" type="text" placeholder="Gegenstand" bind:value={newEntryName} />
</label>
<button
	type="button"
	class="btn variant-filled"
	on:click={async () => {
		await createEntry({ ShoppingListId: id, Name: newEntryName });
	}}>Hinzufügen</button
>
