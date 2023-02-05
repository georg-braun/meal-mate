<script lang="ts">
    import { createCategory } from "../communication/commands";
    import { categoryStore, itemRepositoryStore } from "../data/stores";

    let newCategoryName: string;
    let itemRepository = $itemRepositoryStore;
</script>

<h1>Kategorie</h1>
{#each $categoryStore as category}
    <div>{category.title}</div>
{/each}

<div>
    <input bind:value={newCategoryName} />
    <button
        on:click={async () =>
            await createCategory(newCategoryName)}
        >Hinzufügen</button
    >
</div>
