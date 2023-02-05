<script lang="ts">
    import { itemRepositoryStore, itemStore } from "../data/stores";
    import ShoppingListEntry from "./ShoppingListEntry.svelte";
    import { ShoppingList } from "../domain/shopping-list/ShoppingList";
    import { createEntryOnList, updateShoppingList } from "../communication/commands";
    import App from "../../App.svelte";
    import { sortByCategory } from "../utilities/array";

    let itemRepository = $itemRepositoryStore;

    export let shoppingList: ShoppingList;
    let entriesSortedbyCategory: ShoppingListEntry[] = [];
    $:  {
        console.log("SORT")
        let temp: [] = []
        shoppingList.items.forEach(item => temp.push({...item, category: itemRepository.getCategoryTitleOfShoppingListEntry(shoppingList.id, item.id)}));
        
        entriesSortedbyCategory = sortByCategory(temp);
        
    }
    let newEntryItemId: string;
    let newEntryQualifier: string;

    function handleRemoveEntryEvent(data: {detail: {id: string}}){
        const id = data.detail.id;
        if (!!id){
            const listCopy = structuredClone(shoppingList);
            ShoppingList.removeEntry(listCopy,id)
            updateShoppingList(listCopy)
        }
    }

</script>

{shoppingList.title}

<div class="entries">
    {#if !!shoppingList.items}
        {#each entriesSortedbyCategory as entry (entry.id)}
            <ShoppingListEntry listId={shoppingList.id} entry={entry} on:remove-entry={handleRemoveEntryEvent} />
        {/each}
    {/if}
</div>

<div>
    <div>
        <select bind:value={newEntryItemId}>
            {#each $itemStore as item}
                <option value={item.id}>
                    {item.title}
                </option>
            {/each}
        </select>
        <input bind:value={newEntryQualifier} />
        <button
            on:click={async () =>

                await createEntryOnList(
                    shoppingList,
                    newEntryItemId,
                    newEntryQualifier
                )}>Hinzufügen</button
        >
    </div>
</div>

<style>
</style>
