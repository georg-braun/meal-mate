<script>
	import { goto } from '$app/navigation';
	import { createList } from './api/createList';

	let listName = '';
</script>

<div class="container mx-auto p-8 space-y-8">
	<label class="label">
		<span>Name</span>
		<input class="input" type="text" placeholder="Meine neue Liste" bind:value={listName} />
	</label>

	<button
		type="button"
		class="btn variant-filled"
		on:click={async () => {
			let createdList = await createList({ Name: listName });
			console.log(createdList)
			goto(`/list/${createdList.id}`);
		}}>Erstellen</button
	>
</div>
