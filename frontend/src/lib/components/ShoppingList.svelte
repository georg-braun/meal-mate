<script lang="ts">
  import { onDestroy } from "svelte";

  import apiClient from "../communication/api-client";
  import type { GetCategoriesDetailsQueryItem } from "../communication/dtos/GetCategoriesDetailsQuery";
  import type { ShoppingListQueryResponse } from "../communication/dtos/ShoppingListQueryResponse";
  import {
    startListeningToShoppingListChanges,
    startSignalR,
    stopListeningToShoppingListChanges,
    stopSignalR,
  } from "../communication/mealMateHub";
  import { categoriesWithItemsStore, shoppingListStore } from "../store";
  import ShoppingListEntry from "./ShoppingListEntry.svelte";

  let isListeningToChanges = false;

  $: shoppingList = $shoppingListStore;

  async function startSignal(shoppingListId: string) {
    await startSignalR();
    await startListeningToShoppingListChanges(shoppingListId).then(
      (isConnected) => (isListeningToChanges = isConnected)
    );
  }

  async function stopSignal(shoppingListId: string) {
    if (shoppingList != undefined && !!shoppingListId)
      await stopListeningToShoppingListChanges(shoppingListId).then(
        (isConnected) => (isListeningToChanges = isConnected)
      );
    await stopSignalR();
  }

  $: {
    if (!!id) {
      startSignal(id);
    }
  }

  onDestroy(async () => {
    if (!!id){
      isListeningToChanges = await stopListeningToShoppingListChanges(id);
    }
    else {
      isListeningToChanges = false;
    }
    
  });

  $: {
    console.log(`Current shopping list is ${id}.`);
    getInitialShoppingList(id);
    apiClient.refreshCategoriesDetailsStoreAsync();
  }

  let qualifier: string;
  let selectedNewEntry: GetCategoriesDetailsQueryItem;

  async function getInitialShoppingList(id: string) {
    const shoppingListResponse = await apiClient.getShoppingListAsync(id);
    shoppingListStore.set(shoppingListResponse);
  }

  async function createEntryAsync() {
    if (selectedNewEntry == undefined) return;

    await apiClient.createEntryAsync(
      selectedNewEntry.id,
      shoppingList.id,
      qualifier
    );
  }

  export let id: string;
</script>

{#if !!shoppingList}
  <h1>Einkaufsliste</h1>

  <h2 title={shoppingList.id}>{shoppingList.name}</h2>
  Live-Updates:
  <div
    class="connection-status {isListeningToChanges
      ? 'connection-status--connected'
      : 'connection-status--disconnected'}"
  />

  {#each shoppingList.entries as entry (entry.id)}
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

<style>
  .connection-status {
    background-color: greenyellow;
    width: 10px;
    height: 10px;
    border-radius: 5px;
  }

  .connection-status--connected {
    background-color: greenyellow;
  }

  .connection-status--disconnected {
    background-color: orangered;
  }
</style>
