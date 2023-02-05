<script lang="ts">
    import { createShoppingList } from "../communication/commands";
    import { currentUser } from "../communication/pocketbase";
    import { itemRepositoryStore, shoppingListStore } from "../data/stores";
    import ShoppingList from "./ShoppingList.svelte";

    let newShoppingListName: string;
</script>

<h1>Einkaufslisten</h1>
{#each $shoppingListStore as shoppingList}
    <ShoppingList {shoppingList} />
{/each}

<div>
    <input bind:value={newShoppingListName} />
    <button
        on:click={async () => {
            const userId = $currentUser.id;
            await createShoppingList(
                newShoppingListName,
                userId
            );
        }}>Erstellen</button
    >
</div>

<style>
</style>
