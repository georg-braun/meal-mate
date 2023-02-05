<script lang="ts">
    import { itemRepositoryStore } from "../data/stores";
    import type { ShoppingListEntry } from "../domain/shopping-list/ShoppingListEntry";
    import { createEventDispatcher } from "svelte";

    export let listId: string;
    export let entry: ShoppingListEntry;

    const dispatch = createEventDispatcher();

    let itemRepository = $itemRepositoryStore;

    async function remove() {
        if (!!entry.id) {
            dispatch("remove-entry", { id: entry.id });
        }
    }
</script>

<div class="entry" on:click={remove} on:keydown={remove}>
    {itemRepository.getItemTitle(entry.itemId)}
    {#if entry.qualifier}
        ({entry.qualifier})
    {/if}
    {itemRepository.getCategoryTitleOfShoppingListEntry(listId, entry.id)}
</div>

<style>
    .entry {
        display: flex;
        align-items: center;
        justify-content: center;

        background-color: gray;
    }
</style>
