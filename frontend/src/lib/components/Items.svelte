<script lang="ts">
    import { createItem } from "../communication/commands";
    import {
        itemStore,
        itemRepositoryStore,
        categoryStore,
    } from "../data/stores";

    let newItemName: string;
    let newItemCategoryId: string;
    let itemRepository = $itemRepositoryStore;
</script>

<h1>Gegenstand</h1>
<div class="items">
    {#each $itemStore as item}
        <div class="items__item">
            <div>{item.title}</div>
            <div>{itemRepository.getCategoryTitle(item.categoryId)}</div>
        </div>
    {/each}
</div>

<div>
    <input bind:value={newItemName} />
    <select bind:value={newItemCategoryId}>
        {#each $categoryStore as category}
            <option value={category.id}>
                {category.title}
            </option>
        {/each}
    </select>
    <button
        on:click={async () =>
            await createItem(newItemName, newItemCategoryId)}
        >Hinzufügen</button
    >
</div>

<style>
    .items__item{
        display: grid;
        grid-template-columns: 1fr 1fr;
    }
</style>