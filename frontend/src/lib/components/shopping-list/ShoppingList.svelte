<script lang="ts">
  import { onDestroy } from "svelte";

  import {
    startListeningToShoppingListChanges,
    startSignalR,
    stopListeningToShoppingListChanges,
    stopSignalR,
  } from "../../communication/hub/mealMateHub";
  import { shoppingListStore } from "../../store";
  import ShoppingListEntry from "./ShoppingListEntry.svelte";
  import apiClient from "../../communication/api/api-client";

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

  window.addEventListener("beforeunload", (event) => {
    if (!!id) {
      stopSignal(id);
    }
  });

  $: {
    if (!!id) {
      startSignal(id);
    }
  }

  onDestroy(async () => {
    await stopSignal(id);
  });

  $: {
    console.log(`Current shopping list is ${id}.`);
    getInitialShoppingList(id);
    apiClient.refreshItemsStoreAsync();
  }

  let newEntry: string;

  async function getInitialShoppingList(id: string) {
    const shoppingListResponse = await apiClient.getShoppingListAsync(id);
    shoppingListStore.set(shoppingListResponse);
  }

  async function createEntryAsync() {
    if (newEntry == "") return;

    await apiClient.createEntryWithFreeTextAsync(shoppingList.id, newEntry);
  }

  export let id: string;
</script>

{#if !!shoppingList}
  <div class="header">
    <div
      class="connection-status {isListeningToChanges
        ? 'connection-status--connected'
        : 'connection-status--disconnected'}"
    />
    <h2 title={shoppingList.id}>{shoppingList.name}</h2>
  </div>

  <div class="items">
    {#each shoppingList.entries as entry (entry.id)}
      <ShoppingListEntry shoppingListId={shoppingList.id} {entry} />
    {/each}
  </div>

  <!-- Placeholder so that the new entry isn't in the foreground of a list entry -->
  <div class="bottom-placeholder" />

  <!-- New entry menu that stick at the bottom -->
  <div class="new-entry">
    <div>
      <input class="new-entry__input" bind:value={newEntry} />
      <button
        class="new-entry__add"
        on:click={async () => await createEntryAsync()}>+</button
      >
    </div>
  </div>
{:else}
  Lade Liste ({id}) ...
{/if}

<style>
  .header {
    display: flex;
    align-items: center;
    justify-content: center;
  }
  .connection-status {
    display: block;
    background-color: greenyellow;
    width: 10px;
    height: 10px;
    border-radius: 5px;
    margin-right: 5px;
  }

  .connection-status--connected {
    background-color: greenyellow;
  }

  .connection-status--disconnected {
    background-color: orangered;
  }

  .new-entry {
    position: fixed;
    width: 100%;
    bottom: 0px;
    left: 0px;

    margin: 20px 0px;

    display: flex;
    flex-wrap: wrap;
    justify-content: center;

    text-align: center;
    padding: 5px;
  }

  .new-entry__input {
    height: 30px;

    width: 60vw;
    text-align: center;
    font-size: x-large;
    border: solid;
    border-width: 1px 0px 1px 1px;
    border-radius: 15px 0px 0px 15px;
    flex-shrink: 1;
  }

  .new-entry__add {
    height: 34px;
    padding: 0px 20px;
    font-size: x-large;

    border: solid;
    border-width: 1px 1px 1px 0px;
    border-radius: 0px 15px 15px 0px;

    background-color: greenyellow;
  }

  .bottom-placeholder {
    height: 70px;
    background-color: transparent;
  }

  .items {
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    gap: 10px;
  }
</style>
