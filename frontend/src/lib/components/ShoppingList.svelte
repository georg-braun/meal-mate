<script lang="ts">
  import App from "../../App.svelte";
  import apiClient from "../communication/api-client";
  import type { GetCategoriesDetailsQueryItem } from "../communication/dtos/GetCategoriesDetailsQuery";
  import type { ShoppingListQueryResponse } from "../communication/dtos/ShoppingListQueryResponse";
  import { categoriesWithItemsStore } from "../store";
  import ShoppingListEntry from "./ShoppingListEntry.svelte";

  $: {
    refreshShoppingList(id);
    apiClient.refreshCategoriesDetailsStoreAsync();
  }

  let shoppingList: ShoppingListQueryResponse;
  let qualifier: string;
  let selectedNewEntry: GetCategoriesDetailsQueryItem;

  async function refreshShoppingList(id: string) {
    shoppingList = await apiClient.getShoppingListAsync(id);
  }

  async function createEntryAsync() {
    if (selectedNewEntry == undefined) return;

    await apiClient.createEntryAsync(selectedNewEntry.id, id, qualifier);
  }

  export let id: string;
</script>

{#if !!shoppingList}
  <h1>Einkaufsliste</h1>
  <h2 title={shoppingList.id}>{shoppingList.name}</h2>

  {#each shoppingList.entries as entry (entry.entryId)}
    <div>
      <ShoppingListEntry shoppingListId={shoppingList.id} {entry} />
    </div>
  {/each}

  <input bind:value={qualifier} />
  <select bind:value={selectedNewEntry}>
    {#each $categoriesWithItemsStore as categoryWithItems (categoryWithItems.id)}
      <optgroup label={categoryWithItems.name}>
        {#each categoryWithItems.items as item (item.id)}
          <option value={item}>{item.name}</option>
        {/each}
      </optgroup>
    {/each}
  </select>
  <button on:click={async () => await createEntryAsync()}>Hinzufügen</button>
{:else}
  Lade Liste ({id}) ...
{/if}
